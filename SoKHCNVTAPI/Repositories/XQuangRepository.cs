using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using System.Globalization;
namespace SoKHCNVTAPI.Repositories;

public interface IXQuangRepository
{
    Task<(IEnumerable<XQuang>?, int)> FilterAsync(XQuangFilter model);
    Task<XQuang> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(XQuangDto model, long createdBy);
    Task UpdateAsync(long id, XQuangDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class XQuangRepository : IXQuangRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<XQuang> _XQuangRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    public XQuangRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<XQuang> XQuangRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _XQuangRepository = XQuangRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<XQuang>?, int)> FilterAsync(XQuangFilter model)
    {
        var query = _XQuangRepository.Select();

        query = !string.IsNullOrEmpty(model.ten_co_so)
            ? query.Where(p => p.ten_co_so != null && p.ten_co_so.ToLower().Contains(model.ten_co_so.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.lien_he)
           ? query.Where(p => p.lien_he != null && p.lien_he.ToLower().Contains(model.lien_he.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.loai_thiet_bi)
           ? query.Where(p => p.loai_thiet_bi != null && p.loai_thiet_bi.ToLower().Contains(model.loai_thiet_bi.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.serial_number)
           ? query.Where(p => p.serial_number != null && p.serial_number.ToLower().Contains(model.serial_number.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.hang_san_xuat)
           ? query.Where(p => p.hang_san_xuat != null && p.hang_san_xuat.ToLower().Contains(model.hang_san_xuat.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.nuoc_san_xuat)
           ? query.Where(p => p.nuoc_san_xuat != null && p.nuoc_san_xuat.ToLower().Contains(model.nuoc_san_xuat.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.nam_san_xuat)
           ? query.Where(p => p.nam_san_xuat != null && p.nam_san_xuat.ToLower().Contains(model.nam_san_xuat.ToLower()))
           : query;


        query = !string.IsNullOrEmpty(model.so_giay_phep_nam_truoc)
          ? query.Where(p => p.so_giay_phep_nam_truoc != null && p.so_giay_phep_nam_truoc.ToLower().Contains(model.so_giay_phep_nam_truoc.ToLower()))
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
                case "ten_co_so":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ten_co_so) : query.OrderBy(p => p.ten_co_so);
                    break;
                case "lien_he":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.lien_he) : query.OrderBy(p => p.lien_he);
                    break;
                case "loai_thiet_bi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.loai_thiet_bi) : query.OrderBy(p => p.loai_thiet_bi);
                    break;
                case "serial_number":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.serial_number) : query.OrderBy(p => p.serial_number);
                    break;
                case "hang_san_xuat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.hang_san_xuat) : query.OrderBy(p => p.hang_san_xuat);
                    break;
                case "nuoc_san_xuat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.nuoc_san_xuat) : query.OrderBy(p => p.nuoc_san_xuat);
                    break;
                case "nam_san_xuat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.nam_san_xuat) : query.OrderBy(p => p.nam_san_xuat);
                    break;
                case "so_giay_phep_nam_truoc":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_giay_phep_nam_truoc) : query.OrderBy(p => p.so_giay_phep_nam_truoc);
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
                p.ten_co_so !=null && p.ten_co_so.ToLower().Contains(model.Keyword.ToLower()) ||
                p.serial_number != null && p.serial_number.ToLower().Contains(model.Keyword.ToLower()) ||
                p.lien_he != null && p.lien_he.ToLower().Contains(model.Keyword.ToLower()) ||
                p.loai_thiet_bi != null && p.loai_thiet_bi.ToLower().Contains(model.Keyword.ToLower()) ||
                p.hang_san_xuat!=null && p.hang_san_xuat.ToLower().Contains(model.Keyword.ToLower())||
                p.nuoc_san_xuat !=null && p.nuoc_san_xuat.ToLower().Contains(model.Keyword.ToLower())||
                p.nam_san_xuat != null  && p.nam_san_xuat.ToLower().Contains(model.Keyword.ToLower()) ||
                p.so_giay_phep_nam_truoc != null &&  p.so_giay_phep_nam_truoc.ToLower().Contains(model.Keyword.ToLower())
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

    public async Task<XQuang> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _XQuangRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy  X Quang!");
        return item;
    }

    public async Task CreateAsync(XQuangDto model, long createdBy)
    {
        var query = _XQuangRepository
            .Select();
        //
        var item = await query
            .FirstOrDefaultAsync(p =>
                p.ten_co_so.ToLower().ToLower() == model.ten_co_so.ToLower()||
                 p.serial_number.ToLower().ToLower() == model.serial_number.ToLower());
               
        if (item != null) throw new ArgumentException($"Tên hoặc số serial đã tồn tại!");

        var newItem = _mapper.Map<XQuang>(model);
        _XQuangRepository.Insert(newItem);
        await _XQuangRepository.SaveChangesAsync();
        
        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin {newItem.ten_co_so} thành công.",
            Params = newItem.ten_co_so.ToString() ?? "",
            Target = "XQuang",
            TargetCode = newItem.ten_co_so.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, XQuangDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        //var isExist = await _XQuangRepository
        //    .Select()
        //    .Where(p => p.Id != id)
        //    .FirstOrDefaultAsync(p =>
        //        p.ten_co_so.ToLower().ToLower() == model.ten_co_so.ToLower());
        //if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        _XQuangRepository.Update(item);
        await _XQuangRepository.SaveChangesAsync();
        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin {item.ten_co_so} thành công.",
            Params = item.ten_co_so.ToString() ?? "",
            Target = "XQuang",
            TargetCode = item.ten_co_so.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
    
    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _XQuangRepository.Delete(item);
        await _XQuangRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin {item.ten_co_so} thành công.",
            Params = item.ten_co_so.ToString() ?? "",
            Target = "XQuang",
            TargetCode = item.ten_co_so.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}