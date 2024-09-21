using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface IDistrictRepository
{
    Task<IEnumerable<District>?> FilterAsync(long provinceId);
    Task<District> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(DistrictDto model, long createdBy);
    Task UpdateAsync(long id, DistrictDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class DistrictRepository : IDistrictRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<District> _districtRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "Quận huyện";

    public DistrictRepository(
        IMapper mapper,
        IRepository<District> districtRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository,
        IMemoryCachingService cachingService
    )
    {
        _mapper = mapper;
        _districtRepository = districtRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _cachingService = cachingService;
    }

    public async Task<IEnumerable<District>?> FilterAsync(long provinceId)
    {
        var query = _districtRepository.Select();

        var items = await query
            .Where(p => p.ProvinceId == provinceId)
            .ToListAsync();

        return items;
    }

    public async Task<District> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _districtRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(DistrictDto model, long createdBy)
    {
        var query = _districtRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (item != null) throw new ArgumentException($"{Label} đã tồn tại!");

        var newItem = _mapper.Map<District>(model);
        _districtRepository.Insert(newItem);
        await _districtRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{newItem.Code} tên: {newItem.Name} thành công.",
            Params = newItem.Code.ToString() ?? "",
            Target = "EducationLevel",
            TargetCode = newItem.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, DistrictDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _districtRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (isExist != null) throw new ArgumentException($"Tên hoặc Code {Label} đã được dùng!");

        _mapper.Map(model, item);
        _districtRepository.Update(item);
        await _districtRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "EducationLevel",
            TargetCode = item.Code.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _districtRepository.Delete(item);
        await _districtRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "EducationLevel",
            TargetCode = item.Code.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}