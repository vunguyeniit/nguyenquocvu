using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using SoKHCNVTAPI.Enums;
using System.Globalization;


namespace SoKHCNVTAPI.Repositories;

public interface IActivityLogRepository
{
    Task<(IEnumerable<ActivityLog>?, int)> FilterAsync(ActivityLogFilter model);
    Task SaveLogAsync(ActivityLogDto log,long actionBy ,Enum mode);
}

public class ActivityLogRepository : IActivityLogRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<ActivityLog> _activityLogRepository;
    private readonly IRepository<ActivityLogUser> _activityLogUserRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMemoryCachingService _cachingService;

    public ActivityLogRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<User> userRepository,
        IRepository<ActivityLog> activityLogRepository,
         IRepository<ActivityLogUser> activityLogUserRepository
        )
    {
        _mapper = mapper;
        _cachingService = cachingService;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _activityLogUserRepository = activityLogUserRepository;
    }

    public async Task<(IEnumerable<ActivityLog>?, int)> FilterAsync(ActivityLogFilter model)
    {
        var query = _activityLogRepository.Select();

        query = !string.IsNullOrEmpty(model.Contents)
            ? query.Where(p => p.Contents.ToLower().Contains(model.Contents.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.FullName)
            ? query.Where(p => p.FullName.ToLower().Contains(model.FullName.ToLower()))
            : query;
        //search ngaytao
        if (!string.IsNullOrEmpty(model.CreatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.CreatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.CreatedAt.HasValue && p.CreatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.CreatedAt + "' is not valid for NgayCapNhat.");
            }
        }

        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Contents.ToLower().Contains(model.Keyword.ToLower()) ||
                p.FullName.ToLower().Contains(model.Keyword.ToLower()))
            : query;


        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
             
                case "fullname":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.FullName) : query.OrderBy(p => p.FullName);
                    break;
                case "contents":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Contents) : query.OrderBy(p => p.Contents);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }


        var validated = new PaginationDto(model.PageNumber, model.PageSize);

        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }
    public async Task SaveLogAsync(ActivityLogDto log, long actionBy,Enum mode)
    {
        // ____________ Log ____________
        var currentUserId = actionBy;
        var user = _cachingService.Get<User>($"user-{currentUserId}");
        if (user == null)
        {
            user = await _userRepository.Select().SingleAsync(p => p.Id == currentUserId)!;
            _cachingService.Set($"user-{currentUserId}", user);
        }

        var fullName = $"{user?.Fullname}";

        var content = "";
        var time = DateTime.UtcNow;
        switch (mode)
        {
            case LogMode.Create:
                content = $"{fullName} đã tạo mới {log.Contents}";
                break;
            case LogMode.Update:
                content = $"{fullName} đã cập nhật {log.Contents}";

                break;
            case LogMode.Delete:
                content = $"{fullName} đã xoá {log.Contents}";

                break;
            case LogMode.Approve:
                content = $"{fullName} đã chấp nhận {log.Contents}";

                break;
            case LogMode.Import:
                content = $"{fullName} đã tải lên {log.Contents}";

                break;
            case LogMode.Export:
                content = $"{fullName} đã tải về {log.Contents}";
                break;
            case LogMode.Account:
                content = $"{fullName} đã {log.Contents}{time.ToString("dd/MM/yyyy")}";
                break;
            default:
                content = $"{fullName} {log.Contents}";
                break;
        }
       
        var newLogAssign = new ActivityLogDto
        {
            Contents = content,
            FullName = fullName,
            Params = log.Params,
            Target = log.Target,
            TargetCode = log.TargetCode ?? "",
            UserId = currentUserId
            
        };
        var newLog = _mapper.Map<ActivityLog>(newLogAssign);

        _activityLogRepository.Insert(newLog);
        await _activityLogRepository.SaveChangesAsync();

        // Đồng bộ với ActivityLogUser

        var newLogUserAssign = new ActivityLogUserDto
        {
            Contents = content,
            FullName = fullName,
            Params = log.Params,
            Target = log.Target,
            TargetCode = log.TargetCode ?? "",
            UserId = currentUserId,
            TimeLogin = DateTime.UtcNow
        };
        var newLogUser = _mapper.Map<ActivityLogUser>(newLogUserAssign);

        _activityLogUserRepository.Insert(newLogUser);
        await _activityLogUserRepository.SaveChangesAsync();
    }
}