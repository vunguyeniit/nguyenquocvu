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

public interface ICongBoRepository
{
    Task<(IEnumerable<CongBo>?, int)> FilterAsync(CongBoFilter model);
    Task<CongBo> GetByIdAsync(long id);
    Task CreateAsync(CongBoDto model, long createdBy);
    Task UpdateAsync(long id, CongBoDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class CongBoRepository : ICongBoRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<CongBo> _congBoRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "công bố";

    public CongBoRepository(
        IMapper mapper,
        //IMemoryCachingService cachingService,
        IRepository<CongBo> thongTinRepository,
        //IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _congBoRepository = thongTinRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<CongBo>?, int)> FilterAsync(CongBoFilter model)
    {
        var query = _congBoRepository.Select();

        query = !string.IsNullOrEmpty(model.ChiSoDeMuc)
            ? query.Where(p => p.ChiSoDeMuc.ToLower().Contains(model.ChiSoDeMuc.ToLower()))
            : query;
        query = !string.IsNullOrEmpty(model.NhanDe)
          ? query.Where(p => p.NhanDe.ToLower().Contains(model.NhanDe.ToLower()))
          : query;

        query = !string.IsNullOrEmpty(model.LinhVucNghienCuu)
         ? query.Where(p => p.LinhVucNghienCuu !=null && p.LinhVucNghienCuu.ToLower().Contains(model.LinhVucNghienCuu.ToLower()))
         : query;

        query = !string.IsNullOrEmpty(model.DangTaiLieu)
       ? query.Where(p => p.DangTaiLieu != null && p.DangTaiLieu.ToLower().Contains(model.DangTaiLieu.ToLower()))
       : query;

        query = !string.IsNullOrEmpty(model.TacGia)
      ? query.Where(p => p.TacGia != null && p.TacGia.ToLower().Contains(model.TacGia.ToLower()))
      : query;

        query = !string.IsNullOrEmpty(model.NamXuatBan)
       ? query.Where(p => p.NamXuatBan != null && p.NamXuatBan.ToLower().Contains(model.NamXuatBan.ToLower()))
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
                p.LinhVucNghienCuu.ToLower().Contains(model.Keyword.ToLower()) ||
                p.NhanDe.ToLower().Contains(model.Keyword.ToLower())||
                p.ChiSoDeMuc.ToLower().Contains(model.Keyword.ToLower())||
                p.LinhVucNghienCuu.ToLower().Contains(model.Keyword.ToLower())||
                p.TacGia != null && p.TacGia.ToLower().Contains(model.Keyword.ToLower())
                )
            : query;
        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "nhande":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NhanDe) : query.OrderBy(p => p.NhanDe);
                    break;
                case "linhvucnghiencuu":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.LinhVucNghienCuu) : query.OrderBy(p => p.LinhVucNghienCuu);
                    break;
                case "dangtailieu":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.DangTaiLieu) : query.OrderBy(p => p.DangTaiLieu);
                    break;
                case "tacgia":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TacGia) : query.OrderBy(p => p.TacGia);
                    break;
                case "namxuatban":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NamXuatBan) : query.OrderBy(p => p.NamXuatBan);
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
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }

    public async Task<CongBo?> GetByIdAsync(long id)
    {
        var query = _congBoRepository.Select();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        return item;
    }

    public async Task CreateAsync(CongBoDto model, long createdBy)
    {
        var query = _congBoRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.NhanDe.ToLower().ToLower() == model.NhanDe.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<CongBo>(model);
        newItem.NgayTao = DateTime.UtcNow;
        newItem.NgayCapNhat = DateTime.UtcNow;
        _congBoRepository.Insert(newItem);
        await _congBoRepository.SaveChangesAsync();
        await SyncUtils.SyncCongBo(newItem);

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"công bố với mã #{newItem.ChiSoDeMuc} tên: {newItem.NhanDe} thành công.",
            Params = newItem.ChiSoDeMuc.ToString() ?? "",
            Target = "TieuChuan",
            TargetCode = newItem.ChiSoDeMuc.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, CongBoDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id);
        var isExist = await _congBoRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.ChiSoDeMuc.ToLower().ToLower() == model.ChiSoDeMuc.ToLower() ||
                p.NhanDe.ToLower().ToLower() == model.NhanDe.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");
        item.NgayCapNhat = DateTime.UtcNow;
        _mapper.Map(model, item);
        _congBoRepository.Update(item);
        await _congBoRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"công bố với mã #{item.ChiSoDeMuc} tên: {item.NhanDe} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "TieuChuan",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id);
        if (item != null)
        {
            _congBoRepository.Delete(item);
            await _congBoRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"công bố với mã #{item.ChiSoDeMuc} tên: {item.NhanDe} thành công.",
                Params = item.Id.ToString() ?? "",
                Target = "TieuChuan",
                TargetCode = item.Id.ToString(),
                UserId = deletedBy
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        }
    }
}