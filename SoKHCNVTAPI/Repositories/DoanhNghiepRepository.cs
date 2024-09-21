using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Entities.CommonCategories;
using Microsoft.IdentityModel.Tokens;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoKHCNVTAPI.Repositories;

public interface IDoanhNghiepRepository
{
    Task<(IEnumerable<DoanhNghiep>?, int)> FilterAsync(DoanhNghiepFilter model);
    Task<DoanhNghiep> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(DoanhNghiepDto model, long createdBy);
    Task UpdateAsync(long id, DoanhNghiepDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class DoanhNghiepRepository : IDoanhNghiepRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<DoanhNghiep> _companyRepository;
    private readonly IRepository<LoaiHinhToChuc> _loaiHinhToChucRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNghienCuuRepository;
    private readonly IRepository<DonVi> _donViRepository;

    //private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    //private readonly IMemoryCachingService _cachingService;
    private const string Label = "doanh nghiệp";

    public DoanhNghiepRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<DoanhNghiep> companyRepository,
        IRepository<LoaiHinhToChuc> loaiHinhToChucRepository,
        IRepository<LinhVucNghienCuu> linhVucNghienCuuRepository,
        IRepository<DonVi> donViRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        //_cachingService = cachingService;
        _companyRepository = companyRepository;
        _loaiHinhToChucRepository = loaiHinhToChucRepository;
        _linhVucNghienCuuRepository = linhVucNghienCuuRepository;
        _donViRepository = donViRepository;
        //_userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<DoanhNghiep>?, int)> FilterAsync(DoanhNghiepFilter model)
    {
        var query = _companyRepository
            .Select();

        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.MaSoThue)
            ? query.Where(p => p.MaSoThue != null && p.MaSoThue.ToLower().Contains(model.MaSoThue.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Ten)
            ? query.Where(p => p.Ten != null && p.Ten.ToLower().Contains(model.Ten.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.DiaChi)
            ? query.Where(p => p.DiaChi != null && p.DiaChi.ToLower().Contains(model.DiaChi.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.TenTiengAnh)
            ? query.Where(p => p.TenTiengAnh != null && p.TenTiengAnh.ToLower().Contains(model.TenTiengAnh.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.TenVietTat)
            ? query.Where(p => p.TenVietTat != null && p.TenVietTat.ToLower().Contains(model.TenVietTat.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.TinhThanh)
            ? query.Where(p => p.TinhThanh!.ToLower().Contains(model.TinhThanh.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.EnglishName)
            ? query.Where(p => p.TenTiengAnh != null && p.TenTiengAnh.ToLower().Contains(model.EnglishName.ToLower()))
            : query;

        query = model.Status.HasValue ? query.Where(p => p.Status== model.Status) : query;

        query = string.IsNullOrEmpty(model.Presentative)
            ? query
            : query.Where(
            p => p.ThuTruong != null && p.ThuTruong.ToLower().Contains(model.Presentative.ToLower()));

        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.UpdatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.UpdatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.UpdatedAt.HasValue && p.UpdatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.UpdatedAt + "' is not valid for NgayCapNhat.");
            }
        }

        //filter Id Loai Hinh 
        var queryLoaiHinhToChuc = _loaiHinhToChucRepository.Select();
        if (!string.IsNullOrEmpty(model.LoaiHinhToChuc))
        {
            // Join DoanhNghiep with LoaiHinhToChuc
            query = from dn in query
                    join lhtc in queryLoaiHinhToChuc on dn.LoaiHinhToChuc equals lhtc.Id.ToString()
                    where lhtc.Ten.ToLower().Contains(model.LoaiHinhToChuc.ToLower()) // Filter by Name
                    select dn;
        }

        //filter Id LinhVucNghienCuu 
        var queryLinhVucNghienCuu = _linhVucNghienCuuRepository.Select();
        if (!string.IsNullOrEmpty(model.LinhVucNghienCuu))
        {
            // Join DoanhNghiep with LinhVucNghienCuu
            query = from dn in query
                    join lvnc in queryLinhVucNghienCuu on dn.LinhVucNghienCuu equals lvnc.Id.ToString()
                    where lvnc.Ten.ToLower().Contains(model.LinhVucNghienCuu.ToLower()) // Filter by Name
                    select dn;
        }

        //filter Id CoQuanChuQuan
        var queryCoQuanChuQuan = _donViRepository.Select();
        if (!string.IsNullOrEmpty(model.CoQuanChuQuan))
        {
            // Join DoanhNghiep with CoQuanChuQuan
            query = from dn in query
                    join coQuanChuQuan in queryCoQuanChuQuan on dn.CoQuanChuQuan equals coQuanChuQuan.Id.ToString()
                    where coQuanChuQuan.Name.ToLower().Contains(model.CoQuanChuQuan.ToLower()) // Filter by Name
                    select dn;
        }

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;
     
            query = !string.IsNullOrEmpty(model.Keyword)? query.Where( p =>
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()) ||
                p.MaSoThue != null && p.MaSoThue.ToLower().Contains(model.Keyword.ToLower()) ||
                p.TenVietTat != null && p.TenVietTat.ToLower().Contains(model.Keyword.ToLower()) ||
                p.TenTiengAnh != null && p.TenTiengAnh.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ten != null && p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||

                 //search CoQuanChuQuan
                 p.CoQuanChuQuan != null &&
                      queryCoQuanChuQuan.Where(cq => cq.Id.ToString() == p.CoQuanChuQuan)
                     .Any(cq => cq.Name.ToLower().Contains(model.Keyword.ToLower())) ||
                 //search LinhVucNghienCuu                
                 p.LinhVucNghienCuu != null &&
                    queryLinhVucNghienCuu.Where(lvnc => lvnc.Id.ToString() == p.LinhVucNghienCuu)
                    .Any(lvnc => lvnc.Ten.ToLower().Contains(model.Keyword.ToLower())) ||
                 // search LoaiHinh
                 p.LoaiHinhToChuc != null &&
                    queryLoaiHinhToChuc.Where(lhtc => lhtc.Id.ToString() == p.LoaiHinhToChuc)
                    .Any(lhtc => lhtc.Ten.ToLower().Contains(model.Keyword.ToLower())))
            :query;             
        // status
        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

        //sort by
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
                case "loaihinhtochuc":
                    query = model.sorted_by == "desc"
                        ? query.OrderByDescending(p => string.IsNullOrEmpty(p.LoaiHinhToChuc) ? 0 : Convert.ToInt32(p.LoaiHinhToChuc))
                        : query.OrderBy(p => string.IsNullOrEmpty(p.LoaiHinhToChuc) ? 0 : Convert.ToInt32(p.LoaiHinhToChuc));
                    break;

                case "linhvucnghiencuu":
                    query = model.sorted_by == "desc"
                        ? query.OrderByDescending(p => string.IsNullOrEmpty(p.LinhVucNghienCuu) ? 0 : Convert.ToInt32(p.LinhVucNghienCuu))
                        : query.OrderBy(p => string.IsNullOrEmpty(p.LinhVucNghienCuu) ? 0 : Convert.ToInt32(p.LinhVucNghienCuu));
                    break;

                case "coquanchuquan":
                    query = model.sorted_by == "desc"
                      ? query.OrderByDescending(p => string.IsNullOrEmpty(p.CoQuanChuQuan) ? 0 : Convert.ToInt32(p.CoQuanChuQuan))
                      : query.OrderBy(p => string.IsNullOrEmpty(p.CoQuanChuQuan) ? 0 : Convert.ToInt32(p.CoQuanChuQuan));
                    break;
                case "tinhthanh":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TinhThanh) : query.OrderBy(p => p.TinhThanh);
                    break;
                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;
                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }     
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<DoanhNghiep> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _companyRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(DoanhNghiepDto model, long createdBy)
    {
        var query = _companyRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower() || p.MaSoThue.ToLower().ToLower() == model.MaSoThue.ToLower());
        if (item != null) throw new ArgumentException($"Mã hoặc mã số thuế  {Label} đã tồn tại!");

        var newItem = _mapper.Map<DoanhNghiep>(model);
        newItem.CreatedAt = DateTime.UtcNow;

        _companyRepository.Insert(newItem);
        await _companyRepository.SaveChangesAsync();

        // Call api khcn
        await SyncUtils.SyncDoanhNghiep(newItem);
        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"doanh nghiêp với mã #{newItem.Ma} tên: {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "DoanhNghiep",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, DoanhNghiepDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _companyRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower().ToLower() == model.Ma.ToLower() ||
                p.MaSoThue.ToLower().ToLower() == model.MaSoThue.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên, mã hoặc mã số thuế  {Label} đã tồn tại!");

        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.UtcNow;
        _companyRepository.Update(item);
        await _companyRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"doanh nghiêp với mã #{item.Ma} tên: {item.Ten} thành công.",
            Params = item.Ma,
            Target = "DoanhNghiep",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _companyRepository.Delete(item);
        await _companyRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"doanh nghiêp với mã #{item.Ma} tên: {item.Ten} thành công.",
            Params = item.Ma,
            Target = "DoanhNghiep",
            TargetCode = item.Ma,
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}