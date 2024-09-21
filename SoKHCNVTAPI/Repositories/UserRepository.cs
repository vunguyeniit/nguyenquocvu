using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoKHCNVTAPI.Repositories;

public interface IUserRepository
{
    Task<(IEnumerable<UserResponse>?, int)> PagingAsync(PaginationDto model);

    Task<(IEnumerable<UserResponse>?, int)> FilterUser(UserFilterDto model);
    Task<(IEnumerable<UserResponse>?, int)> Filter(UserFilterDto model);
    Task<UserResponse?> GetByIdAsync(long id);
    Task Create(UserDto model, long createdBy);
    Task Update(long id, UserUpdateDto model, long updatedBy);
    Task UpdatePassword(long id, UserUpdatePasswordDto model, long updatedBy);
   
    Task Delete(long id);

    //Task<NhomResponse?> GetNhom(long id);
    //Task<(IEnumerable<NhomResponse>?, int)> GetNhoms(NhomFilter model);
    //Task CapNhatNhom(long id, NhomDto model, long updatedBy);
    //Task TaoNhom(NhomDto model, long createdBy);

    Task<List<GiaoDien>> GetGiaoDiens(GiaoDienFilter model);
    Task<GiaoDien> CreateUpdateGiaoDien(GiaoDienModel model, long createdBy);
    Task<GiaoDien> GetGiaoDien(string Ma, long userId);
}

public class UserRepository : IUserRepository
{
    //private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;
   private readonly IRepository<Nhom> _nhomRepository;
    private readonly IRepository<GiaoDien> _giaoDienRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public UserRepository(IRepository<User> userRepository, IRepository<GiaoDien> giaoDienRepository, IRepository<Nhom> nhomRepository, 
        IMapper mapper, IActivityLogRepository activityLogRepository)
    {
        _nhomRepository = nhomRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _giaoDienRepository = giaoDienRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<UserResponse>?, int)> PagingAsync(PaginationDto model)
    {
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var query = _userRepository.Select().AsNoTracking();

        var items = await query
            .OrderBy(p =>p.Code)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        var itemMapped = _mapper.Map<IEnumerable<UserResponse>>(items);
        return (itemMapped, records);
    }

    public async Task<(IEnumerable<UserResponse>?, int)> FilterUser(UserFilterDto model)
    {
        var paging = new PaginationDto(model.PageNumber, model.PageSize);

        var query = _userRepository.Select().AsQueryable().AsNoTracking();
        //check Role SA Hide

        if (model.FullName != null)
        {
            query = query.Where(p => p.Fullname != null && p.Fullname.Contains(model.FullName));
        }

        if (model.Email != null)
        {
            query = query.Where(p => p.Email != null && p.Email.Contains(model.Email));
        }

        if (model.Phone != null)
        {
            query = query.Where(p => p.Phone != null && p.Phone.Contains(model.Phone));
        }

        if (model.Code != null)
        {
            query = query.Where(p => p.Code != null && p.Code.Contains(model.Code));
        }
        if (model.Address != null)
        {
            query = query.Where(p => p.Address != null && p.Address.Contains(model.Address));
        }

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;


        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.UpdatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.UpdatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày

                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.UpdatedAt.HasValue && p.UpdatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.UpdatedAt + "' is not valid for NgayCapNhat.");
            }
        }

        //sort by 
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "code":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Code) : query.OrderBy(p => p.Code);
                    break;
                case "fullname":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Fullname) : query.OrderBy(p => p.Fullname);
                    break;

                case "email":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Email) : query.OrderBy(p => p.Email);
                    break;
                case "address":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Address) : query.OrderBy(p => p.Address);
                    break;

                case "phone":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Phone) : query.OrderBy(p => p.Phone);
                    break;

                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;

                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }


        if (model.GroupId > 0)
        {
            query = query.Where(p => p.GroupId == model.GroupId);
        }

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p =>
                    p.Code.ToLower().Contains(model.Keyword.ToLower())
                || p.Fullname.ToLower().Contains(model.Keyword.ToLower())
                || p.Email.ToLower().Contains(model.Keyword.ToLower())
                || p.Phone.ToLower().Contains(model.Keyword.ToLower())
                || p.Address.ToLower().Contains(model.Keyword.ToLower())

            );
        }

        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
            .Take(paging.PageSize)            
            .ToListAsync();
        var records = await query.CountAsync();
        //var itemMapped = _mapper.Map<IEnumerable<UserResponse>>(items);
        var itemMapped =  items.Select(user => new UserResponse
        {
            Id = user.Id,
            NationalId = user.NationalId,
            FullName = user.Fullname,
            Address = user.Address,
            Province = user.Province,
            District = user.District,
            Ward = user.Ward,
            Position = user.Position,
            Status = (short)user.Status,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,      
            Remark = user.Remark,
            Email = user.Email,
            Code = user.Code,
            Phone = user.Phone,
            
            // Không bao gồm thuộc tính Role
        }).ToList();
        return (itemMapped, records);
    }

    public async Task<(IEnumerable<UserResponse>?, int)> Filter(UserFilterDto model)
    {
        var paging = new PaginationDto(model.PageNumber, model.PageSize);

        var query = _userRepository.Select().AsQueryable().AsNoTracking();

        //check Role SA Hide

        query = query.Where(p => p.Role != "SA");
       

        if (model.FullName != null)
        {
            query = query.Where(p => p.Fullname!= null && p.Fullname.Contains(model.FullName));
        }

        if (model.Email != null)
        {
            query = query.Where(p => p.Email != null && p.Email.Contains(model.Email));
        }
        
        if (model.Phone != null)
        {
            query = query.Where(p => p.Phone != null && p.Phone.Contains(model.Phone));
        }

        if (model.Code != null)
        {
            query = query.Where(p => p.Code != null && p.Code.Contains(model.Code));
        }
        if (model.Address != null)
        {
            query = query.Where(p => p.Address != null && p.Address.Contains(model.Address));
        }

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;


        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.UpdatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.UpdatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày

                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.UpdatedAt.HasValue && p.UpdatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.UpdatedAt + "' is not valid for NgayCapNhat.");
            }
        }

        //sort by 
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "code":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Code) : query.OrderBy(p => p.Code);
                    break;
                case "fullname":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Fullname) : query.OrderBy(p => p.Fullname);
                    break;

                case "email":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Email) : query.OrderBy(p => p.Email);
                    break;
                case "address":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Address) : query.OrderBy(p => p.Address);
                    break;

                case "phone":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Phone) : query.OrderBy(p => p.Phone);
                    break;

                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;

                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }



        if (model.GroupId > 0)
        {
            query = query.Where(p => p.GroupId == model.GroupId);
        }

        if (!string.IsNullOrEmpty(model.Keyword))
        {
            query = query.Where(p =>
                    p.Code.ToLower().Contains(model.Keyword.ToLower())
                || p.Fullname.ToLower().Contains(model.Keyword.ToLower())
                || p.Email.ToLower().Contains(model.Keyword.ToLower())
                || p.Phone.ToLower().Contains(model.Keyword.ToLower())
                || p.Address.ToLower().Contains(model.Keyword.ToLower())
                
            );
        }

        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((paging.PageNumber - 1) * paging.PageSize)
            .Take(paging.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        var itemMapped = _mapper.Map<IEnumerable<UserResponse>>(items);
        return (itemMapped, records);
    }

    public async Task<UserResponse?> GetByIdAsync(long id)
    {
        var item = await _userRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
        //if (item.Role == "SA")
        //{
        //    return null;
        //}

        var itemMapped = _mapper.Map<UserResponse>(item);

        itemMapped.Nhom = await _nhomRepository.Select().AsNoTracking().SingleOrDefaultAsync(nhom => nhom.Id == id);
        return itemMapped;
    }

    public async Task Create(UserDto model, long createdBy)
    {
        var isEmail = Utils.IsValidEmail(model.Email);

        if (isEmail)
        {
            var user = await _userRepository.Select().AsNoTracking() .FirstOrDefaultAsync(p => p.Email == model.Email);
            if (user != null) throw new ArgumentException("Email đã được sử dụng!");
        }

        var isPhone = Utils.IsValidPhoneNumber(model.Phone);
        if (isPhone)
        {
            model.Phone = Utils.FormatPhoneNumber(model.Phone!);
            var user = await _userRepository.Select().AsNoTracking().FirstOrDefaultAsync(p => p.Phone == model.Phone);
            if (user != null) throw new ArgumentException("Số điện thoại đã được sử dụng!");
        }

        var newUser = _mapper.Map<User>(model);
        newUser.Password = PasswordBuilder.HashBCrypt(model.Password ?? "12345678");
        newUser.CreatedBy = createdBy;

        _userRepository.Insert(newUser);

        await _userRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"tài khoản {newUser.Code} thành công.",
            Params = newUser.Code,
            Target = "User",
            TargetCode = newUser.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task Update(long id, UserUpdateDto model, long updatedBy)
    {
        
        var user = await _userRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (user == null) throw new ArgumentException("Không tìm thấy người dùng!");

        //Validate
        var validRoles = new List<string> { "user", "admin", "sa" };

        // Kiểm tra vai trò mới người dùng nhập vào có hợp lệ không
        if (!validRoles.Contains(model.Role?.ToLower())) // So sánh không phân biệt hoa thường
        {
            throw new ArgumentException("Vui lòng kiểm tra vai trò có thể là 'user', 'admin', hoặc 'sa'.");
        }


        if (GetRoleLevel(model.Role) > GetRoleLevel(user.Role))
        {
            throw new ArgumentException("Bạn không thể thay đổi vai trò cao hơn vai trò của chính mình!");
        }

        var currentPassword = user.Password;
        var recovery = _mapper.Map(model, user);

        //if(model.Password != null && model.Password != user.Password && recovery.Password != null)
        //{

        //    recovery.Password = PasswordBuilder.HashBCrypt(recovery.Password);

        //} else
        //{
        //recovery.Password = user.Password ?? PasswordBuilder.HashBCrypt("12345678");
        //}

        // Nếu Password không được cung cấp trong model, giữ nguyên mật khẩu hiện tại
     
        if (string.IsNullOrEmpty(model.Password))
        {
            user.Password = currentPassword;         
        }

        
        recovery.UpdatedBy = updatedBy;
        recovery.UpdatedAt = Utils.getCurrentDate();
        _userRepository.Update(recovery);
        await _userRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"thông tin người dùng cho mã #{model.Code} thành công.",
            Params = model.Code,
            Target = "User",
            TargetCode = model.Code
        };

        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }


    private int GetRoleLevel(string role)
    {
        switch (role.ToLower())
        {
            case "sa": return 3;
            case "admin": return 2;
            case "user": return 1;
            default: return 0; // Role không xác định
        }
    }



    public async Task UpdatePassword(long id, UserUpdatePasswordDto model, long updatedBy)
    {
        var user = await _userRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (user == null) throw new ArgumentException("Không tìm thấy người dùng!");

        if (model.Password != null)
        {
            user.Password = PasswordBuilder.HashBCrypt(model.Password);
            user.UpdatedBy = updatedBy;
            user.UpdatedAt = Utils.getCurrentDate();
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            var log = new ActivityLogDto
            {
                Contents = $"thông tin mật khẩu cho tài khoản mã #{user.Code} thành công.",
                Params = user.Code,
                Target = "User",
                TargetCode = user.Code
            };

            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
    }

    public async Task Delete(long id)
    {
        var user = await _userRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (user == null) throw new ArgumentException("Không tìm thấy người dùng!");
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
    }

    //public async Task<(IEnumerable<NhomResponse>?, int)> GetNhoms(NhomFilter model)
    //{
    //    var paging = new PaginationDto(model.PageNumber, model.PageSize);

    //    var query = _nhomRepository.Select().AsQueryable().AsNoTracking();

    //    if (model.Ma != null)
    //    { 
    //        query = query.Where(p => p.Ma != null && p.Ma.Contains(model.Ma));
    //    }

      

    //    var items = await query
    //        .OrderByDescending(p => p.Ma)
    //        .Skip((paging.PageNumber - 1) * paging.PageSize)
    //        .Take(paging.PageSize)
    //        .ToListAsync();

    //    var records = await query.CountAsync();
    //    var itemMapped = _mapper.Map<IEnumerable<NhomResponse>>(items);
    //    return (itemMapped, records);
    //}

    //public async Task<NhomResponse?> GetNhom(long id)
    //{
    //    var item = await _nhomRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
    //    var itemMapped = _mapper.Map<NhomResponse>(item);

    //    var users = _userRepository.Select().AsQueryable().AsNoTracking().Where(p => p.MaNhom == id).ToListAsync();
    //    itemMapped.Users = _mapper.Map<IEnumerable<UserResponse>>(users);

    //    return itemMapped;
    //}

    //public async Task TaoNhom(NhomDto model, long createdBy)
    //{
    //    if (model.Ma != null)
    //    {
    //        var nhom = await _nhomRepository.Select().AsNoTracking().FirstOrDefaultAsync(p => p.Ma == model.Ma);
    //        if (nhom != null) throw new ArgumentException("Mã nhóm đã được sử dụng!");
    //    }

    //    var newNhom = _mapper.Map<Nhom>(model);
    //    newNhom.NgayTao = DateTime.Now;
    //    newNhom.NguoiCapNhat = createdBy;
    //    _nhomRepository.Insert(newNhom);

    //    await _nhomRepository.SaveChangesAsync();

    //    // ____________ Log ____________
    //    var log = new ActivityLogDto
    //    {
    //        Contents = $"nhóm quyền {newNhom.Ma} thành công.",
    //        Params = newNhom.Ma,
    //        Target = "Nhom",
    //        TargetCode = newNhom.Ma.ToString(),
    //        UserId = createdBy
    //    };
    //    await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    //}

    //public async Task CapNhatNhom(long id, NhomDto model, long updatedBy)
    //{
    //    var nhom = await _nhomRepository.Select().AsNoTracking().Where(p => p.Id == id).SingleOrDefaultAsync();
    //    if (nhom == null) throw new ArgumentException("Không tìm thấy người dùng!");

    //    var recovery = _mapper.Map(model, nhom);
    //    recovery.NguoiCapNhat = updatedBy;
    //    _nhomRepository.Update(nhom);
    //    await _nhomRepository.SaveChangesAsync();

    //    var log = new ActivityLogDto
    //    {
    //        Contents = $"nhóm quyền mã #{nhom.Ma} thành công.",
    //        Params = nhom.Ma,
    //        Target = "User",
    //        TargetCode = nhom.Ma
    //    };

    //    await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    //}

    public async Task<List<GiaoDien>> GetGiaoDiens(GiaoDienFilter model)
    {
        var query = _giaoDienRepository.Select();

        if (model.TaiKhoan > 0)
        {
            query = query.Where(p => p.TaiKhoan == model.TaiKhoan);
        }

        if (!string.IsNullOrEmpty(model.TuKhoa))
        {
            query = query.Where(p => p.Ma.ToLower().Contains(model.TuKhoa.ToLower()) ||
                p.Ten.ToLower().Contains(model.TuKhoa.ToLower()));
        }
        List<GiaoDien>? giaodiens = await query.ToListAsync();
        return giaodiens;
    }

    public async Task<GiaoDien> GetGiaoDien(string Ma, long userId)
    {
        GiaoDien? giaodien = await _giaoDienRepository.Select().Where(p => p.TaiKhoan == userId && p.Ma == Ma).FirstOrDefaultAsync();
        if(giaodien == null)
        {
            giaodien = new GiaoDien
            {
                Ma          = Ma,
                Ten         = Ma,
                TaiKhoan    = userId,
                DuLieu      = ""
            };
        }
        return giaodien;
    }

    public async Task<GiaoDien> CreateUpdateGiaoDien(GiaoDienModel model, long createdBy)
    { 
        GiaoDien? giaodien = _giaoDienRepository.Select().Where(p => p.TaiKhoan == createdBy && p.Ma.ToLower() == model.Ma.ToLower()).FirstOrDefault();
        if(giaodien == null)
        {
            giaodien = _mapper.Map<GiaoDien>(model);
            giaodien.TaiKhoan = createdBy;
            giaodien.CapNhat = Utils.getCurrentDate();
            _giaoDienRepository.Insert(giaodien);
        } else
        {
            giaodien = _mapper.Map<GiaoDien>(model);
            giaodien.TaiKhoan = createdBy;
            giaodien.CapNhat = Utils.getCurrentDate();
            _giaoDienRepository.Update(giaodien);
        }
        
        await _giaoDienRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"giao diện hiển thị cho mã #{model.Ma} thành công.",
            Params = model.Ma,
            Target = "GiaoDien",
            TargetCode = model.Ma
        };

        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Update);

        return giaodien;
    }
}