using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface IProvinceRepository
{
    Task<IEnumerable<TinhThanh>?> FilterAsync(ProvinceFilter model);
    Task<TinhThanh> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(ProvinceDto model, long createdBy);
    Task UpdateAsync(long id, ProvinceDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class TinhThanhRepository : IProvinceRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<TinhThanh> _provinceRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "Tỉnh, thành phố";

    public TinhThanhRepository(
        IMapper mapper,
        IRepository<TinhThanh> provinceRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository,
        IMemoryCachingService cachingService
    )
    {
        _mapper = mapper;
        _provinceRepository = provinceRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _cachingService = cachingService;
    }

    public async Task<IEnumerable<TinhThanh>?> FilterAsync(ProvinceFilter model)
    {
        var query = _provinceRepository
            .Select();

        query = !string.IsNullOrEmpty(model.Name)
            ? query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Name.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var items = await query.ToListAsync();
        return items;
    }

    public async Task<TinhThanh> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _provinceRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(ProvinceDto model, long createdBy)
    {
        var query = _provinceRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (item != null) throw new ArgumentException($"{Label} đã tồn tại!");

        var newItem = _mapper.Map<TinhThanh>(model);
        _provinceRepository.Insert(newItem);
        await _provinceRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"tỉnh thành với mã #{newItem.Code} tên: {newItem.Name} thành công.",
            Params = newItem.Code.ToString() ?? "",
            Target = "ProjectType",
            TargetCode = newItem.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, ProvinceDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _provinceRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p => p.Name == model.Name || p.Code == model.Code);
        if (isExist != null) throw new ArgumentException($"Tên hoặc Code {Label} đã được dùng!");

        _mapper.Map(model, item);
        _provinceRepository.Update(item);
        await _provinceRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"tỉnh thành với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "ProjectType",
            TargetCode = item.Code.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _provinceRepository.Delete(item);
        await _provinceRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"tỉnh thành với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "ProjectType",
            TargetCode = item.Code.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}