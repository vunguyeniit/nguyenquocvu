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

namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface IChuyenGiaRepository
{
    Task<(IEnumerable<ChuyenGia>?, int)> GetExperts(ExpertFilter model);
    Task<ChuyenGia> GetExpert(long id, bool isTracking = false);
    Task CreateExpert(ExpertDto model, long createdBy);
    Task UpdateExpert(long id, ExpertDto model, long updatedBy);
    Task DeleteExpert(long id, long deletedBy);
   
    // Ma dinh danh chueyn gia
    Task<(IEnumerable<DinhDanhChuyenGia>?, int)> FilterAsync(ExpertIdentifierFilter model);
    Task<DinhDanhChuyenGia> GetByIdAsync(long id, bool isTracking = false);
    Task CreateAsync(ExpertIdentifierDto model, long createdBy);
    Task UpdateAsync(long id, ExpertIdentifierDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);
}

public class ChuyenGiaRepository : IChuyenGiaRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<DinhDanhChuyenGia> _dinhDanhChuyeGiaRepository;
    private readonly IRepository<ChuyenGia> _expertRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    //private readonly IMemoryCachingService _cachingService;

    private const string Label = "định danh chuyên gia";

    public ChuyenGiaRepository(
        IMapper mapper,
        //IMemoryCachingService cachingService,
        IRepository<DinhDanhChuyenGia> expertIdentifierRepository,
        IRepository<ChuyenGia> expertRepository,
        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        //_cachingService = cachingService;
        _dinhDanhChuyeGiaRepository = expertIdentifierRepository;
        _expertRepository = expertRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<(IEnumerable<DinhDanhChuyenGia>?, int)> FilterAsync(ExpertIdentifierFilter model)
    {
        var query = _dinhDanhChuyeGiaRepository
            .Select();
       
        query = !string.IsNullOrEmpty(model.Name)
            ? query.Where(p => p.Name.ToLower().Contains(model.Name.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Code)
            ? query.Where(p => p.Code.ToLower().Contains(model.Code.ToLower()))
            : query;
        query = !string.IsNullOrEmpty(model.Description)
           ? query.Where(p => p.Description.ToLower().Contains(model.Description.ToLower()))
           : query;
        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;



        //search
        if (!string.IsNullOrEmpty(model.CreatedAt))
        {
            DateTime parsedDate;
            // Thử parse chuỗi ngày tháng từ client
            if (DateTime.TryParseExact(model.CreatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                var targetDate = parsedDate.Date; // Lấy phần ngày
                // So sánh phần ngày của NgayCapNhat
                query = query.Where(p => p.CreatedAt.HasValue && p.CreatedAt.Value.Date == targetDate);
            }
            else
            {
                // Xử lý lỗi nếu chuỗi ngày tháng không hợp lệ
                throw new Exception("The value '" + model.CreatedAt + "' is not valid for NgayCapNhat.");
            }
        }

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

        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "code":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Code) : query.OrderBy(p => p.Code);
                    break;
                case "name":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                case "description":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Description) : query.OrderBy(p => p.Description);
                    break;
                case "status":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status);
                    break;
                case "updatedat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt);
                    break;
                case "createdat":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt);
                    break;

                default:
                    query = query.OrderByDescending(p => p.CreatedAt); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.CreatedAt); }
        
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Name.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Code.ToLower().Contains(model.Keyword.ToLower())||
                p.Description.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<DinhDanhChuyenGia> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _dinhDanhChuyeGiaRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }
    
    public async Task CreateAsync(ExpertIdentifierDto model, long createdBy)
    {
        var query = _dinhDanhChuyeGiaRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (item != null) throw new ArgumentException($"Tên hoặc {Label} đã tồn tại!");

        var newItem = _mapper.Map<DinhDanhChuyenGia>(model);
        newItem.CreatedAt = DateTime.UtcNow;
        newItem.UpdatedAt = DateTime.UtcNow;
        _dinhDanhChuyeGiaRepository.Insert(newItem);
        await _dinhDanhChuyeGiaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{newItem.Code} tên: {newItem.Name} thành công.",
            Params = newItem.Code.ToString() ?? "",
            Target = "ExpertIdentifier",
            TargetCode = newItem.Code.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }
    public async Task UpdateAsync(long id, ExpertIdentifierDto model, long updatedBy)
    {
        var item = await GetByIdAsync(id, true);
        var isExist = await _dinhDanhChuyeGiaRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                p.Name.ToLower().ToLower() == model.Name.ToLower() ||
                p.Code.ToLower().ToLower() == model.Code.ToLower());
        if (isExist != null) throw new ArgumentException($"Tên hoặc {Label} đã được dùng!");

        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.UtcNow;
        _dinhDanhChuyeGiaRepository.Update(item);
        await _dinhDanhChuyeGiaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "ExpertIdentifier",
            TargetCode = item.Code.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deletedBy)
    {
        var item = await GetByIdAsync(id, true);
        _dinhDanhChuyeGiaRepository.Delete(item);
        await _dinhDanhChuyeGiaRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"mã định danh chuyên gia với mã #{item.Code} tên: {item.Name} thành công.",
            Params = item.Code.ToString() ?? "",
            Target = "ExpertIdentifier",
            TargetCode = item.Code.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }


    // GET Expert
    public async Task<(IEnumerable<ChuyenGia>?, int)> GetExperts(ExpertFilter model)
    {
        var query = _expertRepository
            .Select();
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.HoVaTen)
        ? query.Where(p => p.HovaTen !=null && p.HovaTen.ToLower().Contains(model.HoVaTen.ToLower()))
        : query;

        query = !string.IsNullOrEmpty(model.HocVi)
       ? query.Where(p => p.HocVi !=null && p.HocVi.ToLower().Contains(model.HocVi.ToLower()))
       : query;

        query = !string.IsNullOrEmpty(model.BangCap)
     ? query.Where(p => p.BangCap != null && p.BangCap.ToLower().Contains(model.BangCap.ToLower()))
     : query;

        query = !string.IsNullOrEmpty(model.SDT)
      ? query.Where(p => p.SDT != null && p.SDT.ToLower().Contains(model.SDT.ToLower()))
      : query;

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
        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
            //
                p.HovaTen != null && p.HovaTen.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma.ToLower().Contains(model.Keyword.ToLower()) ||
                p.SDT != null && p.SDT.ToLower().Contains(model.Keyword.ToLower()) ||
                p.HocVi != null && p.HocVi.ToLower().Contains(model.Keyword.ToLower()) ||
                p.BangCap != null && p.BangCap.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        //sort by colunm
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "hovaten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.HovaTen) : query.OrderBy(p => p.HovaTen);
                    break;
                case "hocvi":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.HocVi) : query.OrderBy(p => p.HocVi);
                    break;
                case "bangcap":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.BangCap) : query.OrderBy(p => p.BangCap);
                    break;
                case "sdt":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.SDT) : query.OrderBy(p => p.SDT);
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
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();

        return (items, records);
    }

    public async Task<ChuyenGia> GetExpert(long id, bool isTracking = false)
    {
        var query = _expertRepository
            .Select(isTracking);
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException($"Không tìm thấy {Label}!");
        return item;
    }

    public async Task CreateExpert(ExpertDto model, long createdBy)
    {
        var query = _expertRepository
            .Select();

        var item = await query
            .FirstOrDefaultAsync(p => p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException($"Mã  chuyên gia đã tồn tại!");

        var newItem = _mapper.Map<ChuyenGia>(model);
        newItem.CreatedAt = Utils.getCurrentDate();
        _expertRepository.Insert(newItem);
        await _expertRepository.SaveChangesAsync();

        //call api
        await SyncUtils.SyncChuyenGia(newItem);

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"chuyên gia với mã #{newItem.Ma} tên: {newItem.HovaTen} thành công.",
            Params = newItem.Ma.ToString() ?? "",
            Target = "Expert",
            TargetCode = newItem.Ma.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdateExpert(long id, ExpertDto model, long updatedBy)
    {
        var item = await GetExpert(id, true);
        var isExist = await _expertRepository
            .Select()
            .Where(p => p.Id != id)
            .FirstOrDefaultAsync(p =>
                 p.Ma.ToLower().ToLower() == model.Ma.ToLower());
        if (isExist != null) throw new ArgumentException($"Mã chuyên gia+ đã được dùng!");

        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.UtcNow;     
        _expertRepository.Update(item);
        await _expertRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"chuyên gia với mã #{item.Ma} tên: {item.HovaTen} thành công.",
            Params = item.Ma.ToString() ?? "",
            Target = "Expert",
            TargetCode = item.Ma.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteExpert(long id, long deletedBy)
    {
        var item = await GetExpert(id, true);
        _expertRepository.Delete(item);
        await _expertRepository.SaveChangesAsync();


        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"chuyên gia với mã #{item.Ma} tên: {item.HovaTen} thành công.",
            Params = item.Ma.ToString() ?? "",
            Target = "Expert",
            TargetCode = item.Ma.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}