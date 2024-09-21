using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface ILinhVucNghienCuuRepository
{
    Task<(IEnumerable<LinhVucNghienCuu>?, int)> FilterAsync(LinhVucNghienCuuFilter model);
    Task<LinhVucNghienCuuResponse?> GetByIdAsync(long id);
    Task<LinhVucNghienCuuResponse?> GetByCode(string code);
    Task CreateAsync(LinhVucNghienCuuDto model, long createdBy);
    Task UpdateAsync(long id, LinhVucNghienCuuDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class LinhVucNghienCuuRepository : ILinhVucNghienCuuRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNghienCuuRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    private const string Label = "lĩch vực nghiên cứu";

    public LinhVucNghienCuuRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<LinhVucNghienCuu> researchFieldRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _cachingService = cachingService;
        _linhVucNghienCuuRepository = researchFieldRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<LinhVucNghienCuu>?, int)> FilterAsync(LinhVucNghienCuuFilter model)
    {
        var query = _linhVucNghienCuuRepository.Select();

        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Ten)
           ? query.Where(p => p.Ten.ToLower().Contains(model.Ten.ToLower()))
           : query;

        query = !string.IsNullOrEmpty(model.MoTa)
          ? query.Where(p => p.MoTa.ToLower().Contains(model.MoTa.ToLower()))
          : query;

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        if (!string.IsNullOrEmpty(model.NgayTao))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.NgayTao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.NgayTao.HasValue && p.NgayTao.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.NgayTao + "' is not valid for NgayCapNhat.");
            }
        }

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

        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "ten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ten) : query.OrderBy(p => p.Ten);
                    break;
                case "mota":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.MoTa) : query.OrderBy(p => p.MoTa);
                    break;
                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TrangThai) : query.OrderBy(p => p.TrangThai);
                    break;
                case "ngaytao":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.NgayTao) : query.OrderBy(p => p.NgayTao);
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


        query = model.Cha >= 0 ? query.Where(p => p.Cha == model.Cha) : query;

        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma.ToLower().Contains(model.Keyword.ToLower())||
                p.MoTa.ToLower().Contains(model.Keyword.ToLower()))
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

    public async Task<LinhVucNghienCuuResponse?> GetByIdAsync(long id)
    {
        var item = await _linhVucNghienCuuRepository.Select().SingleOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            var result = _mapper.Map<LinhVucNghienCuuResponse>(item);
            result.Subs = await _linhVucNghienCuuRepository.Select().Where(p => p.Cha == id).ToListAsync();
            return result;
        }
        return null;
    }

    public async Task<LinhVucNghienCuuResponse?> GetByCode(string code)
    {
        var item = await _linhVucNghienCuuRepository.Select().SingleOrDefaultAsync(p => p.Ma == code);
        if (item != null)
        {
            var result = _mapper.Map<LinhVucNghienCuuResponse>(item);
            result.Subs = await _linhVucNghienCuuRepository.Select().Where(p => p.Cha == item.Id).ToListAsync();
            return result;
        }
        return null;
    }
    public async Task CreateAsync(LinhVucNghienCuuDto model, long createdBy)
    {
        var query = _linhVucNghienCuuRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException($"Mã {Label} đã tồn tại!");

        var newItem = _mapper.Map<LinhVucNghienCuu>(model);
        newItem.NguoiCapNhat = createdBy;
        newItem.NgayCapNhat = DateTime.UtcNow;
        newItem.NgayTao = DateTime.UtcNow;
        _linhVucNghienCuuRepository.Insert(newItem);
        await _linhVucNghienCuuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"lĩch vực nghiên cứu với mã #{newItem.Ma} tên: {newItem.Ten} thành công.",
            Params = newItem.Ma.ToString() ?? "",
            Target = "LinhVucNghienCuu",
            TargetCode = newItem.Ma.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, LinhVucNghienCuuDto model, long updatedBy)
    {
        var item = await _linhVucNghienCuuRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (item != null)
        {
            var isExist = await _linhVucNghienCuuRepository
                .Select()
                .Where(p => p.Id != id)
                .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
            if (isExist != null) throw new ArgumentException($"Tên hoặc mã {Label} đã được dùng!");

            _mapper.Map(model, item);
            item.NguoiCapNhat = updatedBy;
            item.NgayCapNhat = DateTime.UtcNow;      
            _linhVucNghienCuuRepository.Update(item);
            await _linhVucNghienCuuRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"lĩch vực nghiên cứu với mã #{item.Ma} tên: {item.Ten} thành công.",
                Params = item.Ma.ToString() ?? "",
                Target = "LinhVucNghienCuu",
                TargetCode = item.Ma.ToString(),
                UserId = updatedBy
            };
            await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
        }
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await _linhVucNghienCuuRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        if (item != null)
        {
            _linhVucNghienCuuRepository.Delete(item);
            await _linhVucNghienCuuRepository.SaveChangesAsync();

            // ____________ Log ____________
            var log = new ActivityLogDto
            {
                Contents = $"lĩch vực nghiên cứu với mã #{item.Ma} tên: {item.Ten} thành công.",
                Params = item.Ma.ToString() ?? "",
                Target = "LinhVucNghienCuu",
                TargetCode = item.Ma.ToString(),
                UserId = deletedBy
            };
            await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
        }
    }
}