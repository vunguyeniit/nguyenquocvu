using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace SoKHCNVTAPI.Repositories;

public interface IGroupRepository
{
    Task<(IEnumerable<Nhom>?, int)> PagingAsync(PaginationDto model);
    Task<(IEnumerable<Nhom>?, int)> Filter(NhomFilterDto model);
    Task<NhomDto?> GetByIdAsync(long id);
    Task<Nhom?> Create(NhomDto model, long createdBy);
    Task Update(long id, NhomDto patch, long updatedBy);
    Task Delete(long id, long d);
}

public class GroupRepository : IGroupRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IRepository<Nhom> _groupRepository;
    private readonly IRepository<User> _userRepository;

    public GroupRepository(DataContext context, IRepository<Nhom>  groupRepository, IActivityLogRepository activityLogRepository, IRepository<User> userRepository, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _activityLogRepository = activityLogRepository;
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public async Task<(IEnumerable<Nhom>?, int)> PagingAsync(PaginationDto model)
    {
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var query = _groupRepository.Select().AsNoTracking();
            
        var items = await query.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }


    public async Task<(IEnumerable<Nhom>?, int)> Filter(NhomFilterDto model)
    {
        var query = _groupRepository
           .Select();

        query = !string.IsNullOrEmpty(model.Name)
            ? query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Description)
            ? query.Where(p => p.Description.ToLower().Contains(model.Description.ToLower()))
            : query;

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p =>
                p.Name.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Description.ToLower().Contains(model.Keyword.ToLower()));      
          
        }

        // status
        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    
}

    public async Task<NhomDto?> GetByIdAsync(long id)
    {
        var item = await _groupRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            var nhom = _mapper.Map<NhomDto>(item);
            nhom.Users = await _userRepository.Select().AsNoTracking().Where(p => p.GroupId == id && p.IsLocked == false).Select( p => new User
            {
                Id = p.Id,
                Code = p.Code,
                NationalId = p.NationalId,
                Fullname = p.Fullname,
                Address = p.Address,
                Status = p.Status,
                Password = ""

            }).ToListAsync();

            return nhom;
        }
        return null;
    }

    public async Task<Nhom?> Create(NhomDto model, long createdBy)
    {
        var item = await _groupRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == model.Name);

        if (item != null) throw new ArgumentException("Nhóm đã tồn tại!");

        var newGroup = _mapper.Map<Nhom>(model);
        _groupRepository.Insert(newGroup);
        await _groupRepository.SaveChangesAsync();
        if (model.userIds != null)
        {
            foreach (var userId in model.userIds)
            {
                var user = _userRepository.Select().Where(p => p.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    user.GroupId = newGroup.Id;
                    user.UpdatedAt = Utils.getCurrentDate();
                    _userRepository.Update(user);
                }
            }
            await _userRepository.SaveChangesAsync();
        }
        var log = new ActivityLogDto
        {
            Contents = $"nhóm với mã #{newGroup.Code} tên: {newGroup.Name} thành công.",
            Params = newGroup.Code.ToString() ?? "",
            Target = "Group",
            TargetCode = newGroup.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newGroup;
    }

    public async Task Update(long id, NhomDto model, long updatedBy)
    {
        var item = await _groupRepository.Select()
            .SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy nhóm!");

        // Mapping
        var recovery = _mapper.Map(model, item);
        recovery.UpdatedAt = Utils.getCurrentDate();

        // Update group
        _groupRepository.Update(recovery);
        await _groupRepository.SaveChangesAsync();

        // Update users in the group
        if (model.userIds != null && model.userIds.Any())
        {
            // Remove current group association for users not in the new list
            var usersInGroup = await _userRepository.Select()
                .Where(u => u.GroupId == recovery.Id)
                .ToListAsync();

            foreach (var user in usersInGroup)
            {
                if (!model.userIds.Contains(user.Id))
                {
                    user.GroupId = 0;
                    _userRepository.Update(user);
                }
            }

            await _userRepository.SaveChangesAsync();

            // Set new group association for users in the new list
            foreach (var userId in model.userIds)
            {
                var user = await _userRepository.Select()
                    .SingleOrDefaultAsync(p => p.Id == userId);

                if (user != null)
                {
                    user.GroupId = recovery.Id;
                    user.UpdatedAt = Utils.getCurrentDate();
                    _userRepository.Update(user);
                }
            }

            await _userRepository.SaveChangesAsync();
        }
        // Log activity
        var log = new ActivityLogDto
        {
            Contents = $"Cập nhật nhóm với mã #{item.Code}, tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "Group",
            TargetCode = item.Code.ToString(),
            UserId = updatedBy
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    

    public async Task Delete(long id, long deletedBy)
    {
        var group = await _context.Set<Nhom>().AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        if (group == null) throw new ArgumentException("Không tìm thấy nhóm!");

        _groupRepository.Delete(group);
        await _groupRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nhóm với mã #{group.Code} tên: {group.Name} thành công.",
            Params = group.Code ?? "",
            Target = "Group",
            TargetCode = group.Code,
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}