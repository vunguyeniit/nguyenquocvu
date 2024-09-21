using System;
using System.Reflection.Emit;
using System.Security;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Repositories;


public interface IRoleRepository
{
    Task<(IEnumerable<Role>?, int)> FilterAsync(RoleFilter model);
    Task<Role> GetByIdAsync(long id);
    Task CreateAsync(RoleModel model, long createdBy);
    Task UpdateAsync(long id, RoleModel model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
    Task<bool> AssignRoleByUser(RoleUserModel model, long updatedBy);

    Task<IEnumerable<RoleModuleDto>> GetRoleByModule(string module, long userId);
}

public class RoleRepository : IRoleRepository
{
    private string Label = "Vai trò";
    private readonly IMapper _mapper;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<Nhom> _groupRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IRepository<Menu> _menuRepository;

    public RoleRepository(
        IMapper mapper,
        IRepository<Role> roleRepository,
        IRepository<Permission> permissionRepository,
        IRepository<Nhom> groupRepository,
        IRepository<User> userRepository,
        IRepository<Menu> menuRepository,
        IActivityLogRepository activityLogRepository
        )
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _groupRepository = groupRepository;
        _menuRepository = menuRepository;

    }

    public async Task CreateAsync(RoleModel model, long createdBy)
    {
        var query = _roleRepository.Select();

        var item = await query.FirstOrDefaultAsync(p =>
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        var newItem = _mapper.Map<Role>(model);
        newItem.CreatedBy = createdBy;
        newItem.Code = model.Code.ToLower();
        _roleRepository.Insert(newItem);

        await _roleRepository.SaveChangesAsync();

        //List<string>? permissions = model.Permission;
        //if(permissions.Count > 0)
        //{
        //    foreach(string permission in permissions)
        //    {
        //        var currentPermission = await _permissionRepository.Select(true)
        //                .SingleOrDefaultAsync(x =>x.Name == permission);
        //        if (currentPermission != null)
        //        {
        //            var rolePermission = new RoleHasPermission { Name = permission, PermissionId = currentPermission.Id, RoleId = newItem.Id, CreatedBy = createdBy };
        //            var rolePermissionMap = _mapper.Map<RoleHasPermission>(rolePermission);
        //            _roleHasPermissionRepository.Insert(rolePermissionMap);
        //            await _roleHasPermissionRepository.SaveChangesAsync();
        //        }
        //    }
        //}

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"Tạo mới vai trò {newItem.Code}",
            Params = JsonConvert.SerializeObject(newItem),
            Target = "Role",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id);

         _roleRepository.Delete(item);

        var roleHasPermissions = await _permissionRepository.Select().Where(x => x.RoleId == id).ToListAsync();

        foreach(Permission roleHasPermission in roleHasPermissions )
        {
            var rolePermission = await _permissionRepository.Select().SingleOrDefaultAsync(x => x.Id == roleHasPermission.Id);

            if(rolePermission != null)
            {
                _permissionRepository.Delete(rolePermission);
            }
        }

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"vai trò với mã #{item.Code} thành công.",
            Params = JsonConvert.SerializeObject(item),
            Target = "Role",
            TargetCode = item.Code,
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

    /// <summary>
    /// GetRoles -> lấy danh sách quyền 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Role>?, int)> FilterAsync(RoleFilter model)
    {
        var query = _roleRepository.Select().AsNoTracking();

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p => p.Code.ToLower().Contains(model.TuKhoa.ToLower()));
         }

        query = model.TrangThai.HasValue ? query.Where(p => p.Status == model.TrangThai) : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderBy(p=>p.Code)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .Include(x =>x.Permissions)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<IEnumerable<RoleModuleDto>> GetRoleByModule(string module, long userId)
    {
        List<RoleModuleDto> roles = new List<RoleModuleDto>();
        var query =  _roleRepository.Select().Where(p => p.Status == 1);

        if (!string.IsNullOrEmpty(module))
        {
            query = query.Where(p => p.Module.ToLower().Contains(module.ToLower()));
        }
        List<string> modules = await query.Select(p => p.Module).Distinct().ToListAsync();
        if (modules != null)
        {
            foreach (var m in modules)
            {
                var codes = await _roleRepository.Select().Where(p => p.Status == 1).Where(p => p.Module.ToLower()
                    .Contains(m.ToLower()))
                    .Select( p => new RoleDto()
                    {
                        Id = p.Id,
                        Code = p.Code
                    }
                    ).ToListAsync();

                var _menus = await _menuRepository.Select().Where(p => p.Module != null && p.Module.ToLower() == m.ToLower()).Distinct().ToListAsync();
                if (codes != null)
                {
                    RoleModuleDto roleDto = new RoleModuleDto
                    {
                        Module = m,
                        RoleDtos = codes,
                        Menus = _menus
                    };
                    
                    roles.Add(roleDto);
                }
            }
        }

        var log = new ActivityLogDto
        {
            Contents = $"truy xuất danh sách quyền với phân hệ #{Label ?? "Rỗng" } thành công.",
            Params = module ?? "",
            Target = "Role",
            TargetCode = module,
            UserId = userId
        };
        await _activityLogRepository.SaveLogAsync(log, userId, LogMode.Get);

        return roles;
    }

    public async Task<Role> GetByIdAsync(long id)
    {
        var query = _roleRepository.Select(false).AsNoTracking()
            .Include(x => x.Permissions);

        Role? item = await query.SingleOrDefaultAsync(p => p.Id == id);
        return item == null ? throw new ArgumentException($"Không tìm thấy {Label}!") : item;
    }

    public async Task UpdateAsync(long id, RoleModel model, long updatedBy)
    {
        Role item = await GetByIdAsync(id);

        var isExist = await _roleRepository
            .Select(true)
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Code.ToLower().ToLower() == model.Code.ToLower());


        if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");

        var newRoleModel = new RoleModel { Code = item.Code, Description = model.Description, Status = model.Status, Module = model.Module };

        _mapper.Map(newRoleModel, item);

        await _roleRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"{Label} với mã #{item.Code}",
            Params = JsonConvert.SerializeObject(item),
            Target = "Role",
            TargetCode = item.Code
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task<bool> AssignRoleByUser(RoleUserModel model, long updatedBy)
    {
        User? user = null;
        Nhom? group = null;

        if (model.UserId > 0)
        {
            user = await _userRepository.Select().FirstOrDefaultAsync(p => p.Id == model.UserId);

            if (user == null) throw new ArgumentException($"Tài khoản không tìm thấy");
        } else if (model.GroupId > 0)
        {
            group = await _groupRepository.Select().FirstOrDefaultAsync(p => p.Id == model.GroupId);

            if (group == null) throw new ArgumentException($"Nhóm không tìm thấy");
        }
        var query = _roleRepository
        .Select(true)
        .Include(x => x.Permissions);

        string pList = "";
        if(model.RoleIds != null)
        {
            foreach (var rid in model.RoleIds)
            {
                Role? role = _roleRepository.Select().AsNoTracking().Where(p => p.Id == rid).FirstOrDefault();
                if (role != null) {
                    Permission? permission = _permissionRepository.Select().AsNoTracking().Where(p => p.UserId == model.UserId).Where(p => p.Id == rid).FirstOrDefault();
                    if (permission == null)
                    {
                        permission = new Permission
                        {
                            RoleId = rid,
                            UserId = model.UserId,
                            GroupId = model.GroupId,
                        };
                        _permissionRepository.Insert(permission);
                        pList += role.Code + ",";
                    }
                }
            }

            List<Permission> notApplies = await _permissionRepository.Select().AsNoTracking().Where(p => p.UserId == model.UserId).Where(p => !model.RoleIds.Contains(p.RoleId)).ToListAsync();
            foreach (var nid in notApplies)
            {
                _permissionRepository.Delete(nid);
            }
        }
        await _permissionRepository.SaveChangesAsync();

       if(user != null)
        {
            var log = new ActivityLogDto
            {
                Contents = $"vai trò cho {user.Fullname} với danh sách mã quyền: {pList}",
                Params = JsonConvert.SerializeObject(model.RoleIds),
                Target = "Permission.User",
                TargetCode = user.Code
            };

            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
        else if (group != null)
        {
            var log = new ActivityLogDto
            {
                Contents = $"vai trò cho nhóm {group.Name} với danh sách mã quyền: {pList}",
                Params = JsonConvert.SerializeObject(model.RoleIds),
                Target = "Permission.Group",
                TargetCode = group.Code
            };

            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
        return true;
    }
}