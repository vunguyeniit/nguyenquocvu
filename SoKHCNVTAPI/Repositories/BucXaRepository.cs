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
using System.Globalization;

namespace SoKHCNVTAPI.Repositories;

public interface IBucXaRepository
{
    Task<(IEnumerable<BucXa>?, int)> FilterAsync(BucXaFilter model);
    Task<BucXa> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(BucXaDto model, long createdBy);
    Task UpdateAsync(long id, BucXaDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class BucXaRepository : IBucXaRepository
{ 
    private readonly IMapper _mapper;
    private readonly IRepository<BucXa> _BucXaRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "BucXa";

    public BucXaRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<BucXa> BucXaRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _BucXaRepository = BucXaRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<BucXa>?, int)> FilterAsync(BucXaFilter model)
    {
        var query = _BucXaRepository.Select();

        query = !string.IsNullOrEmpty(model.ho_va_ten)
            ? query.Where(p => p.ho_va_ten != null && p.ho_va_ten.ToLower().Contains(model.ho_va_ten.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.ngay_sinh)
            ? query.Where(p => p.ngay_sinh != null && p.ngay_sinh.ToLower().Contains(model.ngay_sinh.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.so_cccd)
           ? query.Where(p => p.so_cccd != null && p.so_cccd.ToLower().Contains(model.so_cccd.ToLower()))
           : query;

        query = !string.IsNullOrEmpty(model. so_chung_chi)
         ? query.Where(p => p.so_chung_chi != null && p.so_chung_chi.ToLower().Contains(model.so_chung_chi.ToLower()))
         : query;

            query = !string.IsNullOrEmpty(model.ngay_cap)
          ? query.Where(p => p.ngay_cap != null && p.ngay_cap.ToLower().Contains(model.ngay_cap.ToLower()))
          : query;

            query = !string.IsNullOrEmpty(model.ghi_chu)
          ? query.Where(p => p.ghi_chu != null && p.ghi_chu.ToLower().Contains(model.ghi_chu.ToLower()))
          : query;

        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.NgayCapNhat))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.NgayCapNhat, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.NgayCapNhat.HasValue && p.NgayCapNhat.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.NgayCapNhat + "' is not valid for NgayCapNhat.");
            }
        }
        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ho_va_ten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ho_va_ten) : query.OrderBy(p => p.ho_va_ten);
                    break;
                case "ngay_sinh":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ngay_sinh) : query.OrderBy(p => p.ngay_sinh);
                    break;
                case "so_cccd":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_cccd) : query.OrderBy(p => p.so_cccd);
                    break;
                case "so_chung_chi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_chung_chi) : query.OrderBy(p => p.so_chung_chi);
                    break;
                case "ngay_cap":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ngay_cap) : query.OrderBy(p => p.ngay_cap);
                    break;
                case "ghi_chu":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ghi_chu) : query.OrderBy(p => p.ghi_chu);
                    break;
                case "ngaycapnhat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NgayCapNhat) : query.OrderBy(p => p.NgayCapNhat);
                    break;
                default:
                    query = query.OrderByDescending(p => p.NgayTao); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.NgayTao); }
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.ho_va_ten != null && p.ho_va_ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.so_cccd != null && p.so_cccd.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ngay_sinh != null && p.ngay_sinh.ToLower().Contains(model.Keyword.ToLower()) ||
                p.so_chung_chi != null && p.so_chung_chi.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ngay_cap != null && p.ngay_cap.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ghi_chu != null && p.ghi_chu.ToLower().Contains(model.Keyword.ToLower())
                )
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }
    public async Task<BucXa> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _BucXaRepository.Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }
    public async Task CreateAsync(BucXaDto model, long createdBy)
    {
        var query = _BucXaRepository
             .Select();
        var item = await query
            .FirstOrDefaultAsync(p =>
                p.so_cccd.ToLower() == model.so_cccd.ToLower());
       
        if (item != null) throw new ArgumentException($"số cccd đã tồn tại!");


        var newItem = _mapper.Map<BucXa>(model);
        newItem.NgayTao = Utils.getCurrentDate();
        newItem.NgayCapNhat = Utils.getCurrentDate();
        _BucXaRepository.Insert(newItem);
        await _BucXaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{newItem.ho_va_ten} tên: {newItem.ho_va_ten} thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "BucXa",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    
    public async Task UpdateAsync(long id, BucXaDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        if (item == null) throw new ArgumentException($"bức xạ không tồn tại");

        _mapper.Map(model, item);
        item.NgayCapNhat = Utils.getCurrentDate();
        _BucXaRepository.Update(item);
        await _BucXaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{item.ho_va_ten} tên: {item.ho_va_ten} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "BucXa",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _BucXaRepository.Delete(item);
        await _BucXaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"bức xạ với mã #{item.ho_va_ten} tên: {item.ho_va_ten} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "BucXa",
            TargetCode = item.Id.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}