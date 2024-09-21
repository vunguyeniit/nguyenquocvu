using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
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

public interface IThongKeRepository
{
    Task<(IEnumerable<ThongKe>?, int)> FilterAsync(ThongKeFilter model);
    Task<ThongKe> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(ThongKeDto model, long createdBy);
    Task UpdateAsync(long id, ThongKeDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class ThongKeRepository : IThongKeRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<ThongKe> _ThongKeRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "thông ke";

    public ThongKeRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<ThongKe> ThongKeRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _ThongKeRepository = ThongKeRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<ThongKe>?, int)> FilterAsync(ThongKeFilter model)
    {
        var query = _ThongKeRepository.Select();

        //query = !string.IsNullOrEmpty(model.TenBieuMau)
        //    ? query.Where(p => p.TenBieuMau.ToLower().Contains(model.TenBieuMau.ToLower()))
        //    : query;

        //query = !string.IsNullOrEmpty(model.Keyword)
        //    ? query.Where(p =>
        //        p.MaSoQuocGia.ToLower().Contains(model.Keyword.ToLower()) ||
        //        p.TenQuocGia.ToLower().Contains(model.Keyword.ToLower()))
        //    : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }

    public async Task<ThongKe> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _ThongKeRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(ThongKeDto model, long createdBy)
    {
        var query = _ThongKeRepository.Select();

        //var item = await query.FirstOrDefaultAsync(p => p.LoaiBieuMau != null && p.LoaiBieuMau.ToLower().ToLower() == model.LoaiBieuMau.ToLower());
        //if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<ThongKe>(model);
        newItem.NgayTao = Utils.getCurrentDate();
        newItem.NgayCapNhat = Utils.getCurrentDate();
        _ThongKeRepository.Insert(newItem);
        await _ThongKeRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{newItem.LoaiBieuMau} tên: {newItem.TenBieuMau} thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "ThongKe",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, ThongKeDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        //var isExist = await _ThongKeRepository
        //    .Select()
        //    .Where(p => p.Id != id)
        //    .FirstOrDefaultAsync(p =>
        //        p.TenBieuMau.ToLower().ToLower() == model.TenBieuMau.ToLower() ||
        //        p.LoaiBieuMau.ToLower().ToLower() == model.LoaiBieuMau.ToLower());
        
        if (item == null) throw new ArgumentException($"Thống kê không tồn tại");

        _mapper.Map(model, item);
        item.NgayCapNhat = Utils.getCurrentDate();
        _ThongKeRepository.Update(item);
        await _ThongKeRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{item.LoaiBieuMau} tên: {item.TenBieuMau} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "ThongKe",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _ThongKeRepository.Delete(item);
        await _ThongKeRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{item.LoaiBieuMau} tên: {item.TenBieuMau} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "ThongKe",
            TargetCode = item.Id.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}