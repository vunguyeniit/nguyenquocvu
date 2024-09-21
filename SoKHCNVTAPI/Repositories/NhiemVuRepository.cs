using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories.CommonCategories;
using SoKHCNVTAPI.Services;
using System.Globalization;
namespace SoKHCNVTAPI.Repositories;

public interface INhiemVuRepository
{
    Task<(IEnumerable<NhiemVu?>, int)> SearchAsync(CommonFilterDto model);
    Task<NhiemVu?> GetByIdAsync(long id, bool isTracking = false);
    Task<long> Info(MissionInformationDto model, long createdBy);
    Task UpdateInfoAsync(long missionId, MissionInformationDto model, long updatedBy);
    Task Processing(long missionId, MissionProcessingDto model, long updatedBy);
    Task UpdateProcessingAsync(long missionId, MissionProcessingDto model, long updatedBy);
    Task Result(long missionId, MissionResultDto model, long updatedBy);
    Task UpdateResultAsync(long missionId, MissionResultDto model, long updatedBy);
    Task Application(long missionId, MissionApplicationDto model, long updatedBy);
    Task UpdateApplicationAsync(long missionId, MissionApplicationDto model, long updatedBy);
    Task DeleteAsync(long missionId, long deletedBy);

    //Task<NhiemVu> BieuDo(CommonFilterDto model);
}

public class NhiemVuRepository : INhiemVuRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<NhiemVu> _nhiemVuRepository;
    private readonly IRepository<DinhDanhNhiemVu> _dinhDanhNhiemVuRepository;
    private readonly IRepository<CapDoNhiemVu> _missionLevelRepository;
    private readonly IRepository<ToChuc> _organizationRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucnghienCuuRepository;
    private readonly IRepository<LoaiDuAn> _loaiHinhDuAnRepository;
    private readonly IRepository<CanBo> _canBoRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;

    //private const string Label = "Nhiệm vụ";

    public NhiemVuRepository(
        IMapper mapper,
        IRepository<NhiemVu> missionRepository,
        IRepository<ToChuc> organizationRepository,
        IRepository<CapDoNhiemVu> missionLevelRepository,
        IRepository<DinhDanhNhiemVu> missionIdentifierRepository,
        IRepository<LinhVucNghienCuu> researchFieldRepository,
        IRepository<LoaiDuAn> projectTypeRepository,
        IRepository<CanBo> canBoRepository,
        IActivityLogRepository activityLogRepository,
        IMemoryCachingService cachingService)
    {
        _mapper = mapper;
        _nhiemVuRepository = missionRepository;
        _organizationRepository = organizationRepository;
        _missionLevelRepository = missionLevelRepository;
        _canBoRepository = canBoRepository;
        _activityLogRepository = activityLogRepository;
        _cachingService = cachingService;
        _dinhDanhNhiemVuRepository = missionIdentifierRepository;
        _linhVucnghienCuuRepository = researchFieldRepository;
        _loaiHinhDuAnRepository = projectTypeRepository;
    }

    public async Task<(IEnumerable<NhiemVu?>, int)> SearchAsync(CommonFilterDto model)
    {
        var query = _nhiemVuRepository.Select().AsNoTracking();

        query = !string.IsNullOrEmpty(model.Code)
            ? query.Where(p => p.Code != null && p.Code.ToLower().Contains(model.Code.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Name)
           ? query.Where(p => p.Name != null && p.Name.ToLower().Contains(model.Name.ToLower()))
           : query;

        query = !string.IsNullOrEmpty(model.MissionNumber)
           ? query.Where(p => p.MissionNumber != null && p.MissionNumber.ToLower().Contains(model.MissionNumber.ToLower()))
           : query;

        query = !string.IsNullOrEmpty(model.AnticipatedProduct)
         ? query.Where(p => p.AnticipatedProduct != null && p.AnticipatedProduct.ToLower().Contains(model.AnticipatedProduct.ToLower()))
         : query;

        query = model.GovernmentExpenses.HasValue
               ? query.Where(p => p.GovernmentExpenses != null && p.GovernmentExpenses == model.GovernmentExpenses.Value)
               : query;

        query = model.SelfExpenses.HasValue
              ? query.Where(p => p.SelfExpenses != null && p.SelfExpenses == model.SelfExpenses.Value)
              : query;

        query = model.OtherExpenses.HasValue
         ? query.Where(p => p.OtherExpenses != null && p.OtherExpenses == model.OtherExpenses.Value)
         : query;

        query = model.TotalExpenses.HasValue
                ? query.Where(p => p.TotalExpenses != null && p.TotalExpenses == model.TotalExpenses.Value)
                : query;

        query = model.TotalTimeWithMonth.HasValue
              ? query.Where(p => p.TotalTimeWithMonth != null && p.TotalTimeWithMonth == model.TotalTimeWithMonth.Value)
              : query;

        query = model.Status.HasValue ? query.Where(p => p.Status == model.Status) : query;

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

        query = query.Include(p => p.MissionIdentify);
        query = query.Include(p => p.MissionLevel);
        query = query.Include(p => p.Organization);
        query = query.Include(p => p.LinhVucNghienCuu);
        query = query.Include(p => p.ProjectType);
        query = query.Where(p => p.MissionIdentify!.Id == p.MissionIdentifyId);
        query = query.Where(p => p.MissionLevel!.Id == p.MissionLevelId);
        query = query.Where(p => p.Organization!.Id == p.OrganizationId);
        query = query.Where(p => p.LinhVucNghienCuu!.Id == p.LinhVucNghienCuuId);
        query = query.Where(p => p.ProjectType!.Id == p.ProjectTypeId);

        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "name":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                case "missionnumber":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.MissionNumber) : query.OrderBy(p => p.MissionNumber);
                    break;
                case "anticipatedproduct":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.AnticipatedProduct) : query.OrderBy(p => p.AnticipatedProduct);
                    break;
                case "governmentexpenses":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.GovernmentExpenses) : query.OrderBy(p => p.GovernmentExpenses);
                    break;
                case "selfexpenses":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.SelfExpenses) : query.OrderBy(p => p.SelfExpenses);
                    break;
                case "otherexpenses":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.OtherExpenses) : query.OrderBy(p => p.OtherExpenses);
                    break;
                case "totalexpenses":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TotalExpenses) : query.OrderBy(p => p.TotalExpenses);
                    break;
                case "totaltimewithmonth":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TotalTimeWithMonth) : query.OrderBy(p => p.TotalTimeWithMonth);
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
        if (!string.IsNullOrEmpty(model.Keyword))
        {
            string keywordCleaned = model.Keyword.Replace(",", "").ToLower();

            query = query.Where(p =>
                p.Code.ToLower().Contains(model.Keyword.ToLower())
                || p.Name.ToLower().Contains(model.Keyword.ToLower())
                || p.MissionNumber.ToLower().Contains(model.Keyword.ToLower()) ||
                p.AnticipatedProduct!=null && p.AnticipatedProduct.ToLower().Contains(model.Keyword.ToLower()) ||
                p.GovernmentExpenses.HasValue && p.GovernmentExpenses.Value.ToString().ToLower().Contains(keywordCleaned) ||
                p.SelfExpenses.HasValue && p.SelfExpenses.Value.ToString().Contains(keywordCleaned) ||
                p.OtherExpenses.HasValue && p.OtherExpenses.Value.ToString().ToLower().Contains(keywordCleaned) ||
                p.TotalExpenses.HasValue && p.TotalExpenses.Value.ToString().ToLower().Contains(keywordCleaned));
        }
        
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderByDescending(p => p.CreatedAt)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }
    
    public async Task<NhiemVu?> GetByIdAsync(long id, bool isTracking = false)
    {
        var item = await _nhiemVuRepository.Select(isTracking).SingleOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            //var nhiemVuResponse = _mapper.Map<NhiemVuResponse>(item);
            if (item.CanBo != null)
            {
                string[] canboIds = item.CanBo.Split(",");
                if (canboIds.Length > 0)
                {
                    item.CanBos = await _canBoRepository.Select().Where(p => canboIds.Contains(p.Id.ToString())).ToListAsync();
                }
            }
            return item;
        }
        return null;
    }

    /// <summary>
    /// Add new mission
    /// </summary>
    /// <param name="model"></param>
    /// <returns>ID</returns>
    /// <exception cref="Exception"></exception>
    public async Task<long> Info(MissionInformationDto model, long createdBy)
    {
        var query = _nhiemVuRepository.Select();

        var missionIdentifier = await _dinhDanhNhiemVuRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MissionIdentifyId);
        if (missionIdentifier == null) throw new Exception("Mã định danh nhiệm vụ không tồn tại!");

        // check MissionLevel
        var missionLevel = await _missionLevelRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MissionLevelId);
        if (missionLevel == null) throw new Exception("Cấp nhiệm vụ không tồn tại!");

        // check Organization
        //var organization = await _organizationRepository.Select()
        //    .AsNoTracking()
        //    .FirstOrDefaultAsync(p => p.Id == model.OrganizationId);
        //if (organization == null) throw new Exception("Tổ chức không tồn tại!");

        // check ResearchField
        var researchField = await _linhVucnghienCuuRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.LinhVucNghienCuuId);
        if (researchField == null) throw new Exception("Lĩnh vực nghiên cứu không tồn tại!");

        // check ProjectType
        //var projectType = await _loaiHinhDuAnRepository.Select()
        //    .AsNoTracking()
        //    .FirstOrDefaultAsync(p => p.Id == model.ProjectTypeId);
        //if (projectType == null) throw new Exception("Loại hình dự án không tồn tại!");

        var newItem = _mapper.Map<NhiemVu>(model);

        // TODO: Cap Code: Lấy code bảng MissionIdentifiers + .XXX/YY => CR.XXX/YY

        if (missionIdentifier.Code.IsNullOrEmpty())
        {
            throw new Exception("Mã định danh nhiệm vụ không tồn tại!");
        }

        newItem.Code = missionIdentifier.Code + "." + Utils.getCurrentDate().ToString("MMyffff");

        _nhiemVuRepository.Insert(newItem); 
        newItem.CreatedAt = Utils.getCurrentDate();
        newItem.UpdatedAt = Utils.getCurrentDate();
        await _nhiemVuRepository.SaveChangesAsync();

        // Call api khcn
        await SyncUtils.SyncNhiemVu(newItem);

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {newItem.Code} và tên: {newItem.Name} thành công.",
            Params = newItem.Code ?? "",
            Target = "Mission",
            TargetCode = newItem.Code,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem.Id;
    }
    
    public async Task UpdateInfoAsync(long missionId, MissionInformationDto model, long updatedBy)
    {

        var missionLevel = await _missionLevelRepository
            .Select()
            .SingleOrDefaultAsync(p => p.Id == model.MissionLevelId);
        if (missionLevel == null) throw new Exception("Loại hình nhiệm vụ không tồn tại!");

        //var organization = await _organizationRepository
        //    .Select()
        //    .SingleOrDefaultAsync(p => p.Id == model.OrganizationId);
        //if (organization == null) throw new Exception("Tổ chức không tồn tại!");

        var researchField = await _linhVucnghienCuuRepository
            .Select()
            .SingleOrDefaultAsync(p => p.Id == model.LinhVucNghienCuuId);
        if (researchField == null) throw new Exception("Lĩnh vực nghiên cứu không tồn tại!");

        //var projectType = await _loaiHinhDuAnRepository
        //    .Select()
        //    .SingleOrDefaultAsync(p => p.Id == model.ProjectTypeId);
        //if (projectType == null) throw new Exception("Loại hình dự án không tồn tại!");

        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = DateTime.Now;
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task Processing(long missionId, MissionProcessingDto model, long updatedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task UpdateProcessingAsync(long missionId, MissionProcessingDto model, long updatedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task Result(long missionId, MissionResultDto model, long updatedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task UpdateResultAsync(long missionId, MissionResultDto model, long updatedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
    public async Task Application(long infoId, MissionApplicationDto model, long updatedBy)
    {
        var item = await GetByIdAsync(infoId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
    public async Task UpdateApplicationAsync(long missionId, MissionApplicationDto model, long updatedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _mapper.Map(model, item);
        item.UpdatedAt = Utils.getCurrentDate();
        _nhiemVuRepository.Update(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }
    public async Task DeleteAsync(long missionId, long deletedBy)
    {
        var item = await GetByIdAsync(missionId, true);
        _nhiemVuRepository.Delete(item);
        await _nhiemVuRepository.SaveChangesAsync();

        // ____________ Log ____________
        var log = new ActivityLogDto
        {
            Contents = $"nhiệm vụ với mã {item.Code} và tên: {item.Name} thành công.",
            Params = item.Code ?? "",
            Target = "Mission",
            TargetCode = item.Code,
            UserId = deletedBy
        };

        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }
}