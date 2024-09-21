using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using System.Globalization;
using System.Linq.Expressions;

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface IDonViRepository
{
    Task<(IEnumerable<DonVi>?, int)> FilterAsync(DonViFilter model);
    Task<DonVi?> GetByIdAsync(long id, bool isTracking = false);
    Task<DonVi?> GetByCode(string code);
    Task CreateAsync(DonViDto model, long createdBy);
    Task UpdateAsync(long id, DonViDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class DonViRepository : IDonViRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<DonVi> _departmentRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "Đơn vị";

    public DonViRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<DonVi> departmentRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _cachingService = cachingService;
        _departmentRepository = departmentRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<DonVi>?, int)> FilterAsync(DonViFilter model)
    {
        var query = _departmentRepository.Select();

        query = !string.IsNullOrEmpty(model.Name)
            ? query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Code)
            ? query.Where(p => p.Code.ToLower().Contains(model.Code.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Code)
          ? query.Where(p => p.Description.ToLower().Contains(model.Description.ToLower()))
          : query;
        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

        //Search NgayCapNhat
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
                case "name":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                case "description":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Description) : query.OrderBy(p => p.Description);
                    break;         
                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;
                case "createdat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                    break;

                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }



        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Name.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Code.ToLower().Contains(model.Keyword.ToLower())||
                p.Description.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }
   


    public async Task<DonVi?> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _departmentRepository.Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        return item;
    }

    public async Task<DonVi?> GetByCode(string code)
    {
        var query = _departmentRepository
            .Select();
        var item = await query.SingleOrDefaultAsync(p => p.Code == code);
        return item;
    }

    public async Task CreateAsync(DonViDto model, long createdBy)
    {
        var query = _departmentRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc mã {Label} đã tồn tại!");
      
        var newItem = _mapper.Map<DonVi>(model);
        newItem.CreatedAt = DateTime.UtcNow;
        newItem.UpdatedAt = DateTime.UtcNow;
        _departmentRepository.Insert(newItem);
        await _departmentRepository.SaveChangesAsync();

        // ____________ Log ____________
        //var currentUserId = createdBy;
        //var user = _cachingService.Get<User>($"user-{currentUserId}");
        //if (user == null)
        //{
        //    user = await _userRepository.Select().SingleAsync(p => p.Id == currentUserId)!;
        //    _cachingService.Set($"user-{currentUserId}", user);
        //}

        var log = new ActivityLogDto
        {
            Contents = $"đơn vị với mã #{newItem.Code} và tên: {newItem.Name} thành công.",
            Params = newItem.Code,
            Target = "Department",
            TargetCode = newItem.Code,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, DonViDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        if (item != null)
        {
            var isExist = await _departmentRepository
                .Select()
                .Where(p => p.Id != id)
                .FirstOrDefaultAsync(p =>
                    p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                    p.Code.ToLower().ToLower() == model.Code.ToLower());
            if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã được dùng!");     
            _mapper.Map(model, item);

            item.UpdatedAt = DateTime.UtcNow;
            _departmentRepository.Update(item);
            await _departmentRepository.SaveChangesAsync();

            // ____________ Log ____________
            //var currentUserId = updatedBy;
            //var user = _cachingService.Get<User>($"user-{currentUserId}");
            //if (user == null)
            //{
            //    user = await _userRepository.Select().SingleAsync(p => p.Id == currentUserId)!;
            //    _cachingService.Set($"user-{currentUserId}", user);
            //}

            var log = new ActivityLogDto
            {
                Contents = $"đơn vị với mã #{item.Code} và tên: {item.Name} thành công.",
                Params = item.Code,
                Target = "Department",
                TargetCode = item.Code,
                UserId = updatedBy
            };
            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        if (item != null)
        {
            _departmentRepository.Delete(item);
            await _departmentRepository.SaveChangesAsync();

            // ____________ Log ____________
            //var currentUserId = deletedBy;
            //var user = _cachingService.Get<User>($"user-{currentUserId}");
            //if (user == null)
            //{
            //    user = await _userRepository.Select().SingleAsync(p => p.Id == currentUserId)!;
            //    _cachingService.Set($"user-{currentUserId}", user);
            //}

            var log = new ActivityLogDto
            {
                Contents = $"đơn vị với mã #{item.Code} và tên: {item.Name} thành công.",
                Params = item.Code,
                Target = "Department",
                TargetCode = item.Code,
                UserId = deletedBy
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        }

    }
}