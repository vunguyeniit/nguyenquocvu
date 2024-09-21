using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using System.Globalization;

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface ITrangThaiNhiemVuRepository
{
    Task<(IEnumerable<TrangThaiNhiemVu>?, int)> FilterAsync(TrangThaiNhiemVuFilter model);
    Task<TrangThaiNhiemVu> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(TrangThaiNhiemVuDto model, long createdBy);
    Task UpdateAsync(long id, TrangThaiNhiemVuDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class TrangThaiNhiemVuRepository : ITrangThaiNhiemVuRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<TrangThaiNhiemVu> _missionStatusRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "Trạng thái nhiệm vụ";

    public TrangThaiNhiemVuRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<TrangThaiNhiemVu> missionStatusRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _cachingService = cachingService;
        _missionStatusRepository = missionStatusRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<TrangThaiNhiemVu>?, int)> FilterAsync(TrangThaiNhiemVuFilter model)
    {
        var query = _missionStatusRepository
            .Select();

        query = !string.IsNullOrEmpty(model.Name)
            ? query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))
            : query;
        query = !string.IsNullOrEmpty(model.Description)
          ? query.Where(p => p.Description.ToLower().Contains(model.Description.ToLower()))
          : query;

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

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

        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "color":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Color) : query.OrderBy(p => p.Color);
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
                p.Name.ToLower().Contains(model.Keyword.ToLower())||
                p.Description.ToLower().Contains(model.Keyword.ToLower())
                )
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

    public async Task<TrangThaiNhiemVu> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _missionStatusRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(TrangThaiNhiemVuDto model, long createdBy)
    {
        var query = _missionStatusRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower());
        if (item != null) throw new ArgumentException($"Tên {Label} đã tồn tại!");

        var newItem = _mapper.Map<TrangThaiNhiemVu>(model);
        newItem.CreatedAt = DateTime.UtcNow;
        newItem.UpdatedAt = DateTime.UtcNow;
        _missionStatusRepository.Insert(newItem);
        await _missionStatusRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"loại nhiệm vụ với mã #{newItem.Id} tên: {newItem.Name} thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "MissionStatus",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, TrangThaiNhiemVuDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _missionStatusRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên {Label} đã được dùng!");

        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.UtcNow;
        _missionStatusRepository.Update(item);
        await _missionStatusRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"loại nhiệm vụ với mã #{item.Id} tên: {item.Name} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "MissionStatus",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _missionStatusRepository.Delete(item);
        await _missionStatusRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"loại nhiệm vụ với mã #{item.Id} tên: {item.Name} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "MissionStatus",
            TargetCode = item.Id.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}