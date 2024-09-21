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

public interface IThongTinRepository
{
    Task<(IEnumerable<ThongTin>?, int)> FilterAsync(ThongTinFilter model);
    Task<ThongTin> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(ThongTinDto model, long createdBy);
    Task UpdateAsync(long id, ThongTinDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class ThongTinRepository : IThongTinRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<ThongTin> _thongTinRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "thông tin";

    public ThongTinRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<ThongTin> thongTinRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _thongTinRepository = thongTinRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<ThongTin>?, int)> FilterAsync(ThongTinFilter model)
    {
        var query = _thongTinRepository.Select();

        query = !string.IsNullOrEmpty(model.MaSoQuocGia)
            ? query.Where(p => p.MaSoQuocGia != null && p.MaSoQuocGia.ToLower().Contains(model.MaSoQuocGia.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.TenQuocGia)
          ? query.Where(p => p.TenQuocGia.ToLower().Contains(model.TenQuocGia.ToLower()))
          : query;

        query = !string.IsNullOrEmpty(model.ThongKeNhanLuc)
          ? query.Where(p => p.ThongKeNhanLuc != null && p.ThongKeNhanLuc.ToLower().Contains(model.ThongKeNhanLuc.ToLower()))
          : query;

        query = !string.IsNullOrEmpty(model.ThongKeKinhPhi)
        ? query.Where(p => p.ThongKeKinhPhi != null && p.ThongKeKinhPhi.ToLower().Contains(model.ThongKeKinhPhi.ToLower()))
        : query;

        query = !string.IsNullOrEmpty(model.ThongKeKetQua)
        ? query.Where(p => p.ThongKeKetQua != null && p.ThongKeKetQua.ToLower().Contains(model.ThongKeKetQua.ToLower()))
        : query;

        query = !string.IsNullOrEmpty(model.ThongKeHoatDong)
        ? query.Where(p => p.ThongKeHoatDong != null && p.ThongKeHoatDong.ToLower().Contains(model.ThongKeHoatDong.ToLower()))
        : query;


        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "masoquocgia":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.MaSoQuocGia) : query.OrderBy(p => p.MaSoQuocGia);
                    break;
                case "tenquocgia":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TenQuocGia) : query.OrderBy(p => p.TenQuocGia);
                    break;
                case "thoigian":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ThoiGian) : query.OrderBy(p => p.ThoiGian);
                    break;
                case "thongkenhanluc":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ThongKeNhanLuc) : query.OrderBy(p => p.ThongKeNhanLuc);
                    break;
                case "thongkekinhphi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ThongKeKinhPhi) : query.OrderBy(p => p.ThongKeKinhPhi);
                    break;
                case "thongkeketqua":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ThongKeKetQua) : query.OrderBy(p => p.ThongKeKetQua);
                    break;
                case "thongkehoatdong":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ThongKeHoatDong) : query.OrderBy(p => p.ThongKeHoatDong);
                    break;
                case "ngaycapnhat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NgayCapNhat) : query.OrderBy(p => p.NgayCapNhat);
                    break;
                default:
                    query = query.OrderByDescending(p => p.NgayTao); // Sắp xếp mặc định
                    break;
            }
        }
        //search keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.MaSoQuocGia != null && p.MaSoQuocGia.ToLower().Contains(model.Keyword.ToLower()) ||
                p.TenQuocGia != null && p.TenQuocGia.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ThongKeNhanLuc != null && p.ThongKeNhanLuc.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ThongKeKinhPhi != null && p.ThongKeKinhPhi.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ThongKeKetQua != null && p.ThongKeKetQua.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ThongKeHoatDong != null && p.ThongKeHoatDong.ToLower().Contains(model.Keyword.ToLower()))
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

    public async Task<ThongTin> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _thongTinRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }
    public async Task CreateAsync(ThongTinDto model, long createdBy)
    {
        var query = _thongTinRepository
            .Select();
        //
        var item = await query
            .FirstOrDefaultAsync(p =>
                p.TenQuocGia.ToLower().ToLower() == model.TenQuocGia.ToLower() ||
                p.MaSoQuocGia.ToLower().ToLower() == model.MaSoQuocGia.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<ThongTin>(model);
        _thongTinRepository.Insert(newItem);
        await _thongTinRepository.SaveChangesAsync();
        await SyncUtils.SyncThongTinKHCN(newItem);

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{newItem.MaSoQuocGia} tên: {newItem.TenQuocGia} thành công.",
            Params = newItem.MaSoQuocGia.ToString() ?? "",
            Target = "ThongTin",
            TargetCode = newItem.MaSoQuocGia.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }
    public async Task UpdateAsync(long id, ThongTinDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _thongTinRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.TenQuocGia.ToLower().ToLower() == model.TenQuocGia.ToLower() ||
                p.MaSoQuocGia.ToLower().ToLower() == model.MaSoQuocGia.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        _thongTinRepository.Update(item);
        await _thongTinRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{item.MaSoQuocGia} tên: {item.TenQuocGia} thành công.",
            Params = item.MaSoQuocGia.ToString() ?? "",
            Target = "ThongTin",
            TargetCode = item.MaSoQuocGia.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _thongTinRepository.Delete(item);
        await _thongTinRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin với mã #{item.MaSoQuocGia} tên: {item.TenQuocGia} thành công.",
            Params = item.MaSoQuocGia.ToString() ?? "",
            Target = "ThongTin",
            TargetCode = item.MaSoQuocGia.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}