using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Repositories;

public interface IAnToanRepository
{
    Task<(IEnumerable<AnToan>?, int)> FilterAsync(AnToanFilter model);
    Task<AnToan> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(AnToanDto model, long createdBy);
    Task UpdateAsync(long id, AnToanDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class AnToanRepository : IAnToanRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<AnToan> _anToanRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "an toan";

    public AnToanRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<AnToan> anToanRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _anToanRepository = anToanRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<AnToan>?, int)> FilterAsync(AnToanFilter model)
    {
        var query = _anToanRepository.Select();

        query = !string.IsNullOrEmpty(model.AnToanBucXa)
            ? query.Where(p => p.AnToanBucXa != null && p.AnToanBucXa.ToLower().Contains(model.AnToanBucXa.ToLower()))
            : query;

        //query = !string.IsNullOrEmpty(model.Keyword)
        //    ? query.Where(p =>
        //        p.SoBang.ToLower().Contains(model.Keyword.ToLower()) ||
        //        p.TenSangChe.ToLower().Contains(model.Keyword.ToLower()))
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

    public async Task<AnToan> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _anToanRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(AnToanDto model, long createdBy)
    {
        var query = _anToanRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.AnToanBucXa.ToLower() == model.AnToanBucXa.ToLower());
                //||p.SoBang.ToLower().ToLower() == model.SoBang.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<AnToan>(model);
        _anToanRepository.Insert(newItem);
        await _anToanRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin  #{newItem.AnToanBucXa} thành công.",
            Params = newItem.AnToanBucXa.ToString() ?? "",
            Target = "an toan",
            TargetCode = newItem.AnToanBucXa.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, AnToanDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _anToanRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.AnToanBucXa.ToLower().ToLower() == model.AnToanBucXa.ToLower());
                //||p.SoBang.ToLower().ToLower() == model.SoBang.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        _anToanRepository.Update(item);
        await _anToanRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"an toàn #{item.AnToanBucXa} thành công.",
            Params = item.AnToanBucXa.ToString() ?? "",
            Target = "AnToan",
            TargetCode = item.AnToanBucXa.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _anToanRepository.Delete(item);
        await _anToanRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"an toàn  #{item.AnToanBucXa} thành công.",
            Params = item.AnToanBucXa.ToString() ?? "",
            Target = "AnToan",
            TargetCode = item.AnToanBucXa.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}