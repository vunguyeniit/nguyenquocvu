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

public interface ILGSPRepository
{
    Task<(IEnumerable<LGSP>?, int)> FilterAsync(LGSPFilter model);
    Task<LGSP> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(LGSPDto model, long createdBy);
    Task UpdateAsync(long id, LGSPDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class LGSPRepository : ILGSPRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<LGSP> _LGSPRepository;
    private readonly IActivityLogRepository _activityLogRepository;

    private const string Label = "thông tin";

    public LGSPRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<LGSP> LGSPRepository,
        IRepository<User> userRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _LGSPRepository = LGSPRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<LGSP>?, int)> FilterAsync(LGSPFilter model)
    {
        var query = _LGSPRepository.Select();

        query = !string.IsNullOrEmpty(model.ky_bao_cao)
            ? query.Where(p => p.ky_bao_cao !=null && p.ky_bao_cao.ToLower().Contains(model.ky_bao_cao.ToLower()))
            : query;
        query = !string.IsNullOrEmpty(model.thoi_gian_bao_cao)
          ? query.Where(p => p.thoi_gian_bao_cao != null && p.thoi_gian_bao_cao.ToLower().Contains(model.thoi_gian_bao_cao.ToLower()))
          : query;
        query = model.ky_hd_moi.HasValue ? query.Where(p => p.ky_hd_moi == model.ky_hd_moi) : query;     
        query = model.kiem_tra_tien_do_theo_ky.HasValue ? query.Where(p => p.kiem_tra_tien_do_theo_ky == model.kiem_tra_tien_do_theo_ky) : query;     
        query = model.nghiem_thu_va_thanh_ly.HasValue ? query.Where(p => p.nghiem_thu_va_thanh_ly == model.nghiem_thu_va_thanh_ly) : query;     
        query = model.don_dang_ky_so_huu_tri_tue.HasValue ? query.Where(p => p.don_dang_ky_so_huu_tri_tue == model.don_dang_ky_so_huu_tri_tue) : query;   
        query = model.cong_tac_tham_dinh.HasValue ? query.Where(p => p.cong_tac_tham_dinh == model.cong_tac_tham_dinh) : query;       
        query = model.cap_phep_buc_xa_cap_moi.HasValue ? query.Where(p => p.cap_phep_buc_xa_cap_moi == model.cap_phep_buc_xa_cap_moi) : query;       
        query = model.cap_phep_buc_xa_gia_han.HasValue ? query.Where(p => p.cap_phep_buc_xa_gia_han == model.cap_phep_buc_xa_gia_han) : query;       
        query = model.cap_phep_buc_xa_sua_doi.HasValue ? query.Where(p => p.cap_phep_buc_xa_sua_doi == model.cap_phep_buc_xa_sua_doi) : query;      
        query = model.cap_phep_buc_xa_bo_sung.HasValue ? query.Where(p => p.cap_phep_buc_xa_bo_sung == model.cap_phep_buc_xa_bo_sung) : query;      
        query = model.so_luong_gian_hang.HasValue ? query.Where(p => p.so_luong_gian_hang == model.so_luong_gian_hang) : query; 
        query = model.so_luong_san_pham.HasValue ? query.Where(p => p.so_luong_san_pham == model.so_luong_san_pham) : query;
        query = model.so_luong_ho_tro.HasValue ? query.Where(p => p.so_luong_ho_tro == model.so_luong_ho_tro) : query;    
        query = model.cong_bo_hop_chuan.HasValue ? query.Where(p => p.cong_bo_hop_chuan == model.cong_bo_hop_chuan) : query;   
        query = model.cong_bo_hop_quy.HasValue ? query.Where(p => p.cong_bo_hop_quy == model.cong_bo_hop_quy) : query;      
        query = model.kiem_dinh.HasValue ? query.Where(p => p.kiem_dinh == model.kiem_dinh) : query;   
        query = model.hieu_chuan.HasValue ? query.Where(p => p.hieu_chuan == model.hieu_chuan) : query;     
        query = model.thu_nghiem.HasValue ? query.Where(p => p.thu_nghiem == model.thu_nghiem) : query;


        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.ky_bao_cao !=null && p.ky_bao_cao.ToLower().Contains(model.Keyword.ToLower())||
                p.thoi_gian_bao_cao != null && p.thoi_gian_bao_cao.ToLower().Contains(model.Keyword.ToLower())||
                p.ky_hd_moi.HasValue && p.ky_hd_moi.Value.ToString().Contains(model.Keyword.ToLower())||
                p.kiem_tra_tien_do_theo_ky.HasValue && p.kiem_tra_tien_do_theo_ky.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.nghiem_thu_va_thanh_ly.HasValue && p.nghiem_thu_va_thanh_ly.Value.ToString().Contains(model.Keyword.ToLower())||
                p.don_dang_ky_so_huu_tri_tue.HasValue && p.don_dang_ky_so_huu_tri_tue.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cong_tac_tham_dinh.HasValue && p.cong_tac_tham_dinh.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cap_phep_buc_xa_cap_moi.HasValue && p.cap_phep_buc_xa_cap_moi.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cap_phep_buc_xa_gia_han.HasValue && p.cap_phep_buc_xa_gia_han.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cap_phep_buc_xa_sua_doi.HasValue && p.cap_phep_buc_xa_sua_doi.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cap_phep_buc_xa_bo_sung.HasValue && p.cap_phep_buc_xa_bo_sung.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.so_luong_gian_hang.HasValue && p.so_luong_gian_hang.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.so_luong_san_pham.HasValue && p.so_luong_san_pham.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.so_luong_ho_tro.HasValue && p.so_luong_ho_tro.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cong_bo_hop_chuan.HasValue && p.cong_bo_hop_chuan.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.cong_bo_hop_quy.HasValue && p.cong_bo_hop_quy.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.kiem_dinh.HasValue && p.kiem_dinh.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.hieu_chuan.HasValue && p.hieu_chuan.Value.ToString().Contains(model.Keyword.ToLower()) ||
                p.thu_nghiem.HasValue && p.thu_nghiem.Value.ToString().Contains(model.Keyword.ToLower()))
            : query;
        //sort by
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ky_bao_cao":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ky_bao_cao) : query.OrderBy(p => p.ky_bao_cao);
                    break;
                case "thoi_gian_bao_cao":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.thoi_gian_bao_cao) : query.OrderBy(p => p.thoi_gian_bao_cao);
                    break;
                case "ky_hd_moi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.ky_hd_moi) : query.OrderBy(p => p.ky_hd_moi);
                    break;
                case "kiem_tra_tien_do_theo_ky":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.kiem_tra_tien_do_theo_ky) : query.OrderBy(p => p.kiem_tra_tien_do_theo_ky);
                    break;
                case "nghiem_thu_va_thanh_ly":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.nghiem_thu_va_thanh_ly) : query.OrderBy(p => p.nghiem_thu_va_thanh_ly);
                    break;
                case "don_dang_ky_so_huu_tri_tue":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.don_dang_ky_so_huu_tri_tue) : query.OrderBy(p => p.don_dang_ky_so_huu_tri_tue);
                    break;
                case "cong_tac_tham_dinh":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cong_tac_tham_dinh) : query.OrderBy(p => p.cong_tac_tham_dinh);
                    break;
                case "cap_phep_buc_xa_cap_moi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cap_phep_buc_xa_cap_moi) : query.OrderBy(p => p.cap_phep_buc_xa_cap_moi);
                    break;
                case "cap_phep_buc_xa_gia_han":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cap_phep_buc_xa_gia_han) : query.OrderBy(p => p.cap_phep_buc_xa_gia_han);
                    break;
                case "cap_phep_buc_xa_sua_doi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cap_phep_buc_xa_sua_doi) : query.OrderBy(p => p.cap_phep_buc_xa_sua_doi);
                    break;
                case "cap_phep_buc_xa_bo_sung":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cap_phep_buc_xa_bo_sung) : query.OrderBy(p => p.cap_phep_buc_xa_bo_sung);
                    break;
                case "so_luong_gian_hang":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_luong_gian_hang) : query.OrderBy(p => p.so_luong_gian_hang);
                    break;
                case "so_luong_san_pham":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_luong_san_pham) : query.OrderBy(p => p.so_luong_san_pham);
                    break;
                case "so_luong_ho_tro":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.so_luong_ho_tro) : query.OrderBy(p => p.so_luong_ho_tro);
                    break;
                case "cong_bo_hop_chuan":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cong_bo_hop_chuan) : query.OrderBy(p => p.cong_bo_hop_chuan);
                    break;
                case "cong_bo_hop_quy":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.cong_bo_hop_quy) : query.OrderBy(p => p.cong_bo_hop_quy);
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

    public async Task<LGSP> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _LGSPRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateAsync(LGSPDto model, long createdBy)
    {
        var query = _LGSPRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.ky_bao_cao.ToLower().ToLower() == model.ky_bao_cao.ToLower());
               
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<LGSP>(model);
        _LGSPRepository.Insert(newItem);
        await _LGSPRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin  #{newItem.ky_bao_cao} thành công.",
            Params = newItem.ky_bao_cao.ToString() ?? "",
            Target = "Ward",
            TargetCode = newItem.ky_bao_cao.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateAsync(long id, LGSPDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _LGSPRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.ky_bao_cao.ToLower().ToLower() == model.ky_bao_cao.ToLower());
              
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        _LGSPRepository.Update(item);
        await _LGSPRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin  #{item.ky_bao_cao}  thành công.",
            Params = item.ky_bao_cao.ToString() ?? "",
            Target = "LGSP",
            TargetCode = item.ky_bao_cao.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _LGSPRepository.Delete(item);
        await _LGSPRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"thông tin  #{item.ky_bao_cao} thành công.",
            Params = item.ky_bao_cao.ToString() ?? "",
            Target = "LGSP",
            TargetCode = item.ky_bao_cao.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}