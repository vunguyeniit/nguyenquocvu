using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using SoKHCNVTAPI.Enums;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FluentEmail.Core;
using DocumentFormat.OpenXml.Spreadsheet;


namespace SoKHCNVTAPI.Repositories;

public interface IActivityLogUserRepository
{
    Task<(IEnumerable<ActivityLogUser>?, int)> FilterAsync(ActivityLogUserFilter model);
    //Task SaveLogUserAsync(ActivityLogUserDto log, long actionBy, Enum mode);
}

public class ActivityLogUserRepository : IActivityLogUserRepository
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRepository<ActivityLogUser> _activityLogUserRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMemoryCachingService _cachingService;

    public ActivityLogUserRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<User> userRepository,
        IRepository<ActivityLogUser> activityLogUserRepository,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _cachingService = cachingService;
        _userRepository = userRepository;
        _activityLogUserRepository = activityLogUserRepository;
    }

    public async Task<(IEnumerable<ActivityLogUser>?, int)> FilterAsync(ActivityLogUserFilter model)
    {
        var query =  _activityLogUserRepository.Select();
        var UserId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);       
        var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
      
        // Nếu người dùng không phải là Superadmin, chỉ lấy log của chính họ
        if (role.ToLower() != "sa")
        {
            query = query.Where(p => p.UserId == UserId);
        } 
      
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

                case "createdat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
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
    //public async Task SaveLogUserAsync(ActivityLogUserDto log, long actionBy, Enum mode)
    //{
    //    // ____________ Log ____________
    //    var currentUserId = actionBy;
    //    var user = _cachingService.Get<User>($"user-{currentUserId}");
    //    if (user == null)
    //    {
    //        user = await _userRepository.Select().SingleAsync(p => p.Id == currentUserId)!;
    //        _cachingService.Set($"user-{currentUserId}", user);
    //    }



    //    var newLogAssign = new ActivityLogUserDto
    //    {
    //        Contents = content,
    //        FullName = fullName,
    //        Params = log.Params,
    //        Target = log.Target,
    //        TargetCode = log.TargetCode ?? "",
    //        UserId = currentUserId
    //    };
    //    var newLog = _mapper.Map<ActivityLogUser>(newLogAssign);
    //    _activityLogUserRepository.Insert(newLog);
    //    await _activityLogUserRepository.SaveChangesAsync();
    //}

    


}