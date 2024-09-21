using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface IWardRepository
{
    Task<IEnumerable<Ward>?> FilterAsync(long districtId);
    Task<Ward> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(WardDto model, long createdBy);
    Task UpdateAsync(long id, WardDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class WardRepository : IWardRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<Ward> _wardRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "Phường, xã";

    public WardRepository(
        IMapper mapper,
        IRepository<Ward> wardRepository,
        IRepository<User> userRepository,
       IActivityLogRepository activityLogRepository,
        IMemoryCachingService cachingService
    )
    {
        _mapper = mapper;
        _wardRepository = wardRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _cachingService = cachingService;
    }

    public async Task<IEnumerable<Ward>?> FilterAsync(long districtId)
    {
        var query = _wardRepository
            .Select();

        var items = await query
            .Where(p => p.DistrictId == districtId)
            .ToListAsync();

        return items;
    }

    public async Task<Ward> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _wardRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(WardDto model, long createdBy)
    {
        var query = _wardRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (item != null) throw new ArgumentException($"{Label} đã tồn tại!");

        var newItem = _mapper.Map<Ward>(model);
        _wardRepository.Insert(newItem);
        await _wardRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"phường xã với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "Ward",
            TargetCode = item.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Delete);
    }

    public async Task UpdateAsync(long id, WardDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _wardRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (isExist != null) throw new ArgumentException($"Tên hoặc Code {Label} đã được dùng!");

        _mapper.Map(model, item);
        _wardRepository.Update(item);
        await _wardRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"phường xã với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "Ward",
            TargetCode = item.Code.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _wardRepository.Delete(item);
        await _wardRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"phường xã với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "Ward",
            TargetCode = item.Code.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}