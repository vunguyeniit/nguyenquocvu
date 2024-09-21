using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using System.Diagnostics;
using System.Globalization;
namespace SoKHCNVTAPI.Repositories;
public interface ISoHuuTriTueRepository
{
    Task<(IEnumerable<SoHuuTriTue>?, int)> FilterAsync(SoHuuTriTueFilter model);
    Task<SoHuuTriTue> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(SoHuuTriTueDto model, long createdBy);
    Task UpdateAsync(long id, SoHuuTriTueDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class SoHuuTriTueRepository : ISoHuuTriTueRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<SoHuuTriTue> _soHuuTriTueRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "sở hữu trí tuệ";

    public SoHuuTriTueRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<SoHuuTriTue> soHuuTriTueRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _soHuuTriTueRepository = soHuuTriTueRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<SoHuuTriTue>?, int)> FilterAsync(SoHuuTriTueFilter model)
    {
        var query = _soHuuTriTueRepository.Select();

        query = !string.IsNullOrEmpty(model.SoBang)
            ? query.Where(p => p.SoBang.ToLower().Contains(model.SoBang.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.LoaiSoHuu)
          ? query.Where(p => p.LoaiSoHuu != null && p.LoaiSoHuu.ToLower().Contains(model.LoaiSoHuu.ToLower()))
          : query;

        query = !string.IsNullOrEmpty(model.ChuBang)
        ? query.Where(p => p.ChuBang != null && p.ChuBang.ToLower().Contains(model.ChuBang.ToLower()))
        : query;

            query = !string.IsNullOrEmpty(model.PhanLoai)
          ? query.Where(p => p.PhanLoai.ToLower().Contains(model.PhanLoai.ToLower()))
          : query;

            query = !string.IsNullOrEmpty(model.TenSangChe)
        ? query.Where(p => p.TenSangChe.ToLower().Contains(model.TenSangChe.ToLower()))
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
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.SoBang.ToLower().Contains(model.Keyword.ToLower()) ||
                p.TenSangChe.ToLower().Contains(model.Keyword.ToLower())||
                p.LoaiSoHuu.ToLower().Contains(model.Keyword.ToLower())||
                p.ChuBang.ToLower().Contains(model.Keyword.ToLower())||
                p.PhanLoai.ToLower().Contains(model.Keyword.ToLower())
                )
            : query;
        //sort by colum
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "sobang":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.SoBang) : query.OrderBy(p => p.SoBang);
                    break;
                case "tensangche":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TenSangChe) : query.OrderBy(p => p.TenSangChe);
                    break;
                case "loaisohuu":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.LoaiSoHuu) : query.OrderBy(p => p.LoaiSoHuu);
                    break;
                case "phanloai":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.PhanLoai) : query.OrderBy(p => p.PhanLoai);
                    break;
                case "chubang":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ChuBang) : query.OrderBy(p => p.ChuBang);
                    break;
                default:
                    query = query.OrderByDescending(p => p.NgayTao); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.NgayTao); }

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<SoHuuTriTue> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _soHuuTriTueRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(SoHuuTriTueDto model, long createdBy)
    {
        var query = _soHuuTriTueRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.TenSangChe.ToLower().ToLower() == model.TenSangChe.ToLower() ||
                p.SoBang.ToLower().ToLower() == model.SoBang.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<SoHuuTriTue>(model);
        newItem.NgayTao = DateTime.UtcNow;
        newItem.NgayCapNhat = DateTime.UtcNow;
        _soHuuTriTueRepository.Insert(newItem);
        await _soHuuTriTueRepository.SaveChangesAsync();
        //call api
        await SyncUtils.SyncSoHuuTriTue(newItem);

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"sở hữu trí tuệ với mã #{newItem.SoBang} tên: {newItem.TenSangChe} thành công.",
            Params = newItem.SoBang.ToString() ?? "",
            Target = "SoHuTriTue",
            TargetCode = newItem.SoBang.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, SoHuuTriTueDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _soHuuTriTueRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.TenSangChe.ToLower().ToLower() == model.TenSangChe.ToLower() ||
                p.SoBang.ToLower().ToLower() == model.SoBang.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        item.NgayCapNhat = DateTime.UtcNow;
        _soHuuTriTueRepository.Update(item);
        await _soHuuTriTueRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"sở hữu trí tuệ với mã #{item.SoBang} tên: {item.TenSangChe} thành công.",
            Params = item.SoBang.ToString() ?? "",
            Target = "SoHuTriTue",
            TargetCode = item.SoBang.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _soHuuTriTueRepository.Delete(item);
        await _soHuuTriTueRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"sở hữu trí tuệ với mã #{item.SoBang} tên: {item.TenSangChe} thành công.",
            Params = item.SoBang.ToString() ?? "",
            Target = "SoHuTriTue",
            TargetCode = item.SoBang.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}