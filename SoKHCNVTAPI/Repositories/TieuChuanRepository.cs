using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Repositories;

public interface ITieuChuanRepository
{
    Task<(IEnumerable<TieuChuan>?, int)> FilterAsync(TieuChuanFilter model);
    Task<TieuChuan> GetByIdAsync(long id);
    Task CreateAsync(TieuChuanDto model, long createdBy);
    Task UpdateAsync(long id, TieuChuanDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class TieuChuanRepository : ITieuChuanRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<TieuChuan> _tieuChuanRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "tiêu chuẩn";

    public TieuChuanRepository(
        IMapper mapper,
        //IMemoryCachingService cachingService,
        IRepository<TieuChuan> tieuChuanRepository,
        //IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _tieuChuanRepository = tieuChuanRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<TieuChuan>?, int)> FilterAsync(TieuChuanFilter model)
    {
        var query = _tieuChuanRepository.Select();

        query = !string.IsNullOrEmpty(model.TenTieuChuan)
            ? query.Where(p => p.TenTieuChuan.ToLower().Contains(model.TenTieuChuan.ToLower()))
            : query;
        query = !string.IsNullOrEmpty(model.SoHieu)
          ? query.Where(p => p.SoHieu.ToLower().Contains(model.SoHieu.ToLower()))
          : query;
        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.TenTieuChuan.ToLower().Contains(model.Keyword.ToLower()) ||
                p.SoHieu.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query.OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }

    public async Task<TieuChuan?> GetByIdAsync(long id)
    {
        var query = _tieuChuanRepository.Select();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        //if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(TieuChuanDto model, long createdBy)
    {
        var query = _tieuChuanRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.SoHieu.ToLower().ToLower() == model.SoHieu.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<TieuChuan>(model);
        newItem.NgayTao = Utils.getCurrentDate();
        _tieuChuanRepository.Insert(newItem);
        await _tieuChuanRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"tiêu chuẩn với số hiệu #{newItem.SoHieu} tên: {newItem.TenTieuChuan} thành công.",
            Params = newItem.SoHieu.ToString() ?? "",
            Target = "TieuChuan",
            TargetCode = newItem.SoHieu.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, TieuChuanDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            var isExist = await _tieuChuanRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.SoHieu.ToLower().ToLower() == model.SoHieu.ToLower() ||
                p.TenTieuChuan.ToLower().ToLower() == model.TenTieuChuan.ToLower());
            if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

            _mapper.Map(model, item);
            _tieuChuanRepository.Update(item);
            await _tieuChuanRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"tiêu chuẩn với mã #{item.SoHieu} tên: {item.TenTieuChuan} thành công.",
                Params = item.SoHieu.ToString() ?? "",
                Target = "TieuChuan",
                TargetCode = item.SoHieu.ToString(),
                UserId = updatedBy
            };
            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            _tieuChuanRepository.Delete(item);
            await _tieuChuanRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"tiêu chuẩn với mã #{item.SoHieu} tên: {item.TenTieuChuan} thành công.",
                Params = item.SoHieu.ToString() ?? "",
                Target = "TieuChuan",
                TargetCode = item.SoHieu.ToString(),
                UserId = deletedBy
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        }
    }
}