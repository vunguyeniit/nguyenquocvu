using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SoKHCNVTAPI.Repositories;

public interface ICanBoRepository
{
    Task<(IEnumerable<CanBo>?, int)> FilterAsync(CanBoFilter model);
    Task<CanBoResponse?> GetByIdAsync(long id);
    //Task<OfficerResponse> GetByCodeAsync(long id, bool isTracking = false);
    Task CreateAsync(CanBoDto model, long createdBy);
    Task UpdateAsync(long id, CanBoDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);

    Task<(IEnumerable<CongBoKHCN>?, int)> FilterPublicationAsync(CongBoKHCNFilter model);
    Task<CongBoKHCN> GetPublicationByIdAsync(long id, bool isTracking = false);
    Task CreatePublicationAsync(CongBoKHCNDto model, long createdBy);
    Task UpdatePublicationAsync(long id, CongBoKHCNDto model, long updatedBy);
    Task DeletePublicationAsync(long id, long deletedBy);

    //HocVanCanBo
    Task<(IEnumerable<HocVanCanBoResponse>?, int)> HocVanCanBoPagingAsync(HocVanCanBoFilter model);
    Task<HocVanCanBoResponse> GetHocVanCanBo(long id);
    Task<HocVanCanBoResponse> TaoHocVanCanBo(HocVanCanBoDto model, long createdBy);
    Task CapNhatHocVanCanBo(long id, HocVanCanBoDto patch, long updatedBy);
    Task XoaHocVanCanBo(long id, long updatedBy);

    //NhiemVuCanBo
    Task<(IEnumerable<NhiemVuCanBoResponse>?, int)> NhiemVuCanBoPagingAsync(NhiemVuCanBoFilter model);
    Task<NhiemVuCanBoResponse> GetNhiemVuCanBo(long id);
    Task<NhiemVuCanBoResponse> TaoNhiemVuCanBo(NhiemVuCanBoDto model, long createdBy);
    Task CapNhatNhiemVuCanBo(long id, NhiemVuCanBoDto patch, long updatedBy);
    Task XoaNhiemVuCanBo(long id, long updatedBy);
    //CanBoCongTac

    Task<(IEnumerable<CanBoCongTacResponse>?, int)> CanBoCongTacPagingAsync(CanBoCongTacFilter model);
    Task<CanBoCongTacResponse> GetCanBoCongTac(long id);
    Task<CanBoCongTacResponse> TaoCanBoCongTac(CanBoCongTacDto model, long createdBy);
    Task CapNhatCanBoCongTac(long id, CanBoCongTacDto patch, long updatedBy);
    Task XoaCanBoCongTac(long id, long updatedBy);
}

public class CanBoRepository : ICanBoRepository
{
    //private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepository<CanBo> _canboRepository;
    //private readonly IRepository<QuocGia> _countryRepository;
    //private readonly IRepository<TrinhDoDaoTao> _educationLevelRepository;
    //private readonly IRepository<ToChuc> _organizationRepository;
    private readonly IRepository<CongBoKHCN> _congBoKHCNRepository;
   // private readonly IRepository<Entities.CommonCategories.Degree> _degreeRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNghienCuuRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IRepository<HocVanCanBo> _hocVanCanBoRepository;
    private readonly IRepository<NhiemVuCanBo> _nhiemVuCanBoRepository;
    private readonly IRepository<CanBoCongTac> _canBoCongTacRepository;
    private readonly IRepository<SoKHCNVTAPI.Entities.CommonCategories.Degree>  _hocViRepository;
    private readonly IRepository<ToChuc> _toChucRepository;

    public CanBoRepository(
        //DataContext context,
        IMapper mapper,
        //IRepository<QuocGia> countryRepository,
        //IRepository<TrinhDoDaoTao> educationLevelRepository,
        IRepository<CanBo> officerRepository,
        IActivityLogRepository activityLogRepository,
        //IRepository<ToChuc> organizationRepository,
        IRepository<CongBoKHCN> officerPublicationRepository,
        //IRepository<Entities.CommonCategories.Degree> degreeRepository,
        IRepository<LinhVucNghienCuu> researchFieldRepository,
        IRepository<HocVanCanBo> officerEducationrRepository,
        IRepository<NhiemVuCanBo> officerMissionRepository,
         IRepository<ToChuc> toChucRepository,
        IRepository <SoKHCNVTAPI.Entities.CommonCategories.Degree> hocViRepository,
         

    IRepository<CanBoCongTac> canBoCongTacRepository
        )

    {
        //_context = context;
        _mapper = mapper;
        //_countryRepository = countryRepository;
        //_educationLevelRepository = educationLevelRepository;
        _canboRepository = officerRepository;
        //_organizationRepository = organizationRepository;
        _congBoKHCNRepository = officerPublicationRepository;
        //_degreeRepository = degreeRepository;
        _linhVucNghienCuuRepository = researchFieldRepository;
        _activityLogRepository = activityLogRepository;
        _hocVanCanBoRepository = officerEducationrRepository;
        _nhiemVuCanBoRepository = officerMissionRepository;
        _canBoCongTacRepository = canBoCongTacRepository;
        _hocViRepository = hocViRepository;
        _toChucRepository = toChucRepository;

    }

    #region Can bo

    public async Task<(IEnumerable<CanBo>?, int)> FilterAsync(CanBoFilter model)
    {
        var query = _canboRepository.Select();

        query = !string.IsNullOrEmpty(model.Ma) ? query.Where(p => p.Ma.Contains(model.Ma)) : query;
        query = !string.IsNullOrEmpty(model.HoVaTen)
            ? query.Where(p => p.HoVaTen.Contains(model.HoVaTen))
            : query;  
        
        query = !string.IsNullOrEmpty(model.DienThoai) ? query.Where(p => p.DienThoai != null && p.DienThoai.Contains(model.DienThoai)) : query;       
        query = !string.IsNullOrEmpty(model.Email) ? query.Where(p => p.Email != null && p.Email.Contains(model.Email)) : query;


        //filter Id HocVi
        var queryHocVi = _hocViRepository.Select();
        if (!string.IsNullOrEmpty(model.HocVi))
        {

            query = from cb in query
                    join hv in queryHocVi on cb.HocVi equals hv.Id.ToString()
                    where hv.Name.ToLower().Contains(model.HocVi.ToLower()) // Filter by Name
                    select cb;
        }
        //filter Id LinhVucNghienCuu       
        var queryLinhVucNghienCuu = _linhVucNghienCuuRepository.Select();
        if (!string.IsNullOrEmpty(model.LinhVucNC) || !string.IsNullOrEmpty(model.Keyword))
        {
            List<string> linhVucNghienCuuIds = new List<string>();

            // Nếu có từ khóa trong model.Keyword, lọc dựa trên từ khóa (search LVNC)
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                linhVucNghienCuuIds = await queryLinhVucNghienCuu
                    .Where(lvnc => lvnc.Ten.ToLower().Contains(model.Keyword.ToLower()))
                    .Select(lvnc => lvnc.Id.ToString())
                    .ToListAsync();
            }
            // Nếu có từ khóa trong model.LinhVucNC, lọc dựa trên LinhVucNC
            else if (!string.IsNullOrEmpty(model.LinhVucNC))
            {
                var searchTerms = model.LinhVucNC.Split(';').Select(term => term.Trim().ToLower()).ToList();

                linhVucNghienCuuIds = await queryLinhVucNghienCuu
                    .Where(lvnc => searchTerms.Any(term => lvnc.Ten.ToLower().Contains(term)))
                    .Select(lvnc => lvnc.Id.ToString())
                    .ToListAsync();
            }
            // Lấy toàn bộ kết quả
            var results = await query.ToListAsync();
            // Lọc kết quả dựa trên các ID tìm được
            var filteredResults = results
                .Where(cb => cb.LinhVucNC != null &&
                    cb.LinhVucNC.Split(',')
                    .Select(id => id.Trim())
                    .Intersect(linhVucNghienCuuIds)
                    .Count() == linhVucNghienCuuIds.Count) // Đảm bảo tất cả các ID đều khớp
                .ToList();

            var result = filteredResults.Count;

            return (filteredResults, result);
        }


        //filter Id CoQuanCongTac
        var queryToChuc = _toChucRepository.Select();
        if (!string.IsNullOrEmpty(model.CoQuanCongTac))
        {
            query = from cb in query
                    join tc in queryToChuc on cb.CoQuanCongTac equals tc.Id.ToString()
                    where tc.TenToChuc !=null && tc.TenToChuc.ToLower().Contains(model.CoQuanCongTac.ToLower()) // Filter by Name
                    select cb;
        }
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ma.Contains(model.Keyword) || p.HoVaTen.Contains(model.Keyword) || p.DienThoai != null && p.DienThoai.Contains(model.Keyword) ||
                p.Email != null && p.Email.Contains(model.Keyword) ||

                 //search CoQuanChuQuan
                 p.HocVi != null &&
                     queryHocVi.Where(hv => hv.Id.ToString() == p.HocVi)
                     .Any(hv => hv.Name.ToLower().Contains(model.Keyword.ToLower())) ||
                
                 // search coquancongtac
                 p.CoQuanCongTac != null &&
                    queryToChuc.Where(tc => tc.Id.ToString() == p.CoQuanCongTac)
                    .Any(tc => tc.TenToChuc != null && tc.TenToChuc.ToLower().Contains(model.Keyword.ToLower())))
            : query;

        //sort by column
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "hovaten":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.HoVaTen) : query.OrderBy(p => p.HoVaTen);
                    break;
                case "hocvi":
                    query = model.sorted_by == "desc"
                               ? query.OrderByDescending(p => Convert.ToInt32(p.HocVi))
                               : query.OrderBy(p => Convert.ToInt32(p.HocVi));
                    break;
                case "linhvucnc":
                    query = model.sorted_by == "desc"
                               ? query.OrderByDescending(p => p.LinhVucNC.Length).ThenByDescending(p => p.LinhVucNC)
                               : query.OrderBy(p => p.LinhVucNC.Length).ThenBy(p => p.LinhVucNC);
                    break;                               
                case "coquancongtac":
                    query = model.sorted_by == "desc"
                                ? query.OrderByDescending(p => Convert.ToInt32(p.CoQuanCongTac))
                              : query.OrderBy(p => Convert.ToInt32(p.CoQuanCongTac));
                    break;
                default:
                    query = query.OrderBy(p => p.NgayTao); // Sắp xếp mặc định
                    break;
            }
        }
        else { query = query.OrderByDescending(p => p.NgayTao); }

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            //.OrderBy(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        IList<CanBoResponse> canBoResponses = new List<CanBoResponse>();
        foreach (var item in items)
        {
            var canBoResponse = _mapper.Map<CanBoResponse>(item);
            if (canBoResponse.LinhVucNC != null)
            {
                IList<LinhVucNghienCuu> linhVucNghienCuus = new List<LinhVucNghienCuu>();
                string[] lvncs = canBoResponse.LinhVucNC.Split(',');
                if (lvncs.Length > 0)
                {
                    foreach (var lvn in lvncs)
                    {
                        var linhvucngiencuu = await _linhVucNghienCuuRepository.Select().Where(p => p.Id.ToString() == lvn).FirstOrDefaultAsync();
                        if (linhvucngiencuu != null)
                        {
                            linhVucNghienCuus.Add(linhvucngiencuu);
                        }
                    }
                }
                canBoResponse.LinhVucNghienCuus = linhVucNghienCuus;
            }
            canBoResponses.Add(canBoResponse);
        }
        var total = await query.CountAsync();

        return (items, total);
    }

    public async Task<CanBoResponse?> GetByIdAsync(long id)
    {
        var query = _canboRepository.Select();

        var item = await query.SingleOrDefaultAsync(p => p.Id == id);

        var canBoResponse = _mapper.Map<CanBoResponse>(item);

        if(canBoResponse.LinhVucNC != null)
        {
            IList < LinhVucNghienCuu> linhVucNghienCuus = new List<LinhVucNghienCuu>();
            string[] lvncs = canBoResponse.LinhVucNC.Split(',');
            if(lvncs.Length > 0)
            {
                foreach( var lvn in lvncs)
                {
                    var linhvucngiencuu = await _linhVucNghienCuuRepository.Select().Where(p => p.Id == int.Parse(lvn)).FirstOrDefaultAsync();
                    if (linhvucngiencuu != null)
                    {
                        linhVucNghienCuus.Add(linhvucngiencuu);
                    }
                }
            }
            canBoResponse.LinhVucNghienCuus = linhVucNghienCuus;
        }

        return canBoResponse;      
    }

    //public async Task<OfficerResponse> GetByCodeAsync(string code, bool isTracking = false)
    //{
    //    var item = await _officerRepository
    //        .Select(isTracking)
    //        .FirstOrDefaultAsync(p => p.Code == code);
    //    if (item == null) throw new ArgumentException("Không tìm thấy!");

    //    var educationLevel = await _educationLevelRepository
    //        .Select()
    //        .Where(p => p.IsDeleted == false)
    //        .FirstOrDefaultAsync(p => p.Id == item.EducationLevelId);

    //    var count = await _countryRepository
    //        .Select()
    //        .Where(p => p.IsDeleted == false)
    //        .FirstOrDefaultAsync(p => p.Id == item.CountryId);

    //    var degree = await _degreeRepository
    //        .Select()
    //        .Where(p => p.IsDeleted == false)
    //        .FirstOrDefaultAsync(p => p.Id == item.DegreeId);

    //    //var organization = await _organizationRepository
    //    //    .Select()
    //    //    .Where(p => p.IsDeleted == false)
    //    //    .FirstOrDefaultAsync(p => p.Id == item.OrganizationId);

    //    IEnumerable<ResearchField>? researchFields = null;
    //    if (!item.ResearchFieldIds.IsNullOrEmpty())
    //    {
    //        string[] _researchFieldIds = item.ResearchFieldIds.Split(",");
            
    //        if (_researchFieldIds.Length > 0)
    //        {
    //            researchFields = await _researchFieldRepository
    //                .Select()
    //                .Where(p => p.IsDeleted == false)
    //                .Where(p => _researchFieldIds.Contains(p.Id.ToString())).ToListAsync();
               
    //        }
    //    }

    //    var result = _mapper.Map<OfficerResponse>(item);
    //    result.EducationLevel = educationLevel;
    //    result.Country = count;
    //    result.Degree = degree;
    //    //result.Organization = organization;
    //    result.ResearchFieldIds = item.ResearchFieldIds ?? "";
    //    result.ResearchFields = researchFields;
    //    return result;
    //}

    public async Task CreateAsync(CanBoDto model, long createdBy)
    {
        var isCodeExist = await _canboRepository.Select()
            .FirstOrDefaultAsync(p => p.Ma == model.Ma);
        if (isCodeExist != null) throw new ArgumentException("Mã cán bộ đã tồn tại");

        // Organization
        //var organization = await _organizationRepository.Select()
        //    //.Where(p => p.IsDeleted == false)
        //    .FirstOrDefaultAsync(p => p.Id == model.OrganizationId);
        //if (organization == null) throw new ArgumentException("Tổ chức không tồn tại");

        // // EducationLevel
        //var educationLevel = await _educationLevelRepository.Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.EducationLevelId);
        //if (educationLevel == null) throw new ArgumentException("Trình độ đào tạo không tồn tại");
        
        //// // Degrees
        //var degree = await _degreeRepository.Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.DegreeId);
        //if (degree == null) throw new ArgumentException("Học vị không tồn tại");
        
        //// // Country
        //var country = await _countryRepository.Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.CountryId);
        //if (country == null) throw new ArgumentException("Quốc tịch không tồn tại");


        // // ResearchFieldIds
        //var researchFields = _researchFieldRepository.Select().Where(p => p.IsDeleted == false);
        //var researchFieldIsExist = model.ResearchFieldIds != null && model.ResearchFieldIds.Select(id =>
        //    researchFields.SingleOrDefaultAsync(p => p.Id == id)).Any(researchFieldExist => false);
        //if (researchFieldIsExist) throw new ArgumentException("Lĩnh vực nghiên cứu không tồn tại");
        var newItem = _mapper.Map<CanBo>(model);
        //newItem.ResearchFieldIds = model.ResearchFieldIds ?? "";
        //OrganizationTypeId to array string---
        //newItem.LinhVucNC = JsonConvert.SerializeObject(model.LinhVucNC);
        _canboRepository.Insert(newItem);
        await _canboRepository.SaveChangesAsync();
        //call api
        await SyncUtils.SyncCanBo(newItem);
    }

    public async Task UpdateAsync(long id, CanBoDto model, long updatedBy)
    {
        var query = _canboRepository.Select();

        // Code
        //var codeIsExist = await query.FirstOrDefaultAsync(p => p.Code == model.Code);
        //if (codeIsExist != null) throw new ArgumentException("Mã cán bộ đã tồn tại");

        //// Organization
        //var organization = await _organizationRepository
        //    .Select()
        //    //.Where(p => p.IsDeleted == false)
        //    .FirstOrDefaultAsync(p => p.Id == model.OrganizationId);
        //if (organization == null) throw new ArgumentException("Tổ chức không tồn tại");


        // EducationLevel
        //var educationLevel = await _educationLevelRepository
        //    .Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.EducationLevelId);
        //if (educationLevel == null) throw new ArgumentException("Trình độ đào tạo không tồn tại");

        //// Degrees
        //var degree = await _degreeRepository
        //    .Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.DegreeId);
        //if (degree == null) throw new ArgumentException("Học vị không tồn tại");

        //// Country
        //var national = await _countryRepository
        //    .Select()
        //    .FirstOrDefaultAsync(p => p.Id == model.CountryId);
        //if (national == null) throw new ArgumentException("Quốc tịch không tồn tại");


        // ResearchFieldIds
       
        //if (!model.ResearchFieldIds.IsNullOrEmpty())
        //{
        //    var researchFields = _researchFieldRepository.Select().Where(p => p.IsDeleted == false);
        //    string[] _researchFieldIds = model.ResearchFieldIds.Split(",");
        //    var researchFieldIsExist = model.ResearchFieldIds != null && _researchFieldIds.Select(id => researchFields.SingleOrDefaultAsync(p => p.Id.ToString() == id)).Any(researchFieldExist => false);
        //    if (researchFieldIsExist) throw new ArgumentException("Lĩnh vực nghiên cứu không tồn tại");
        //}
       

        var item = await _canboRepository.Select().FirstOrDefaultAsync(p => p.Id == id );
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        _mapper.Map(model, item);
        //if (!model.ResearchFieldIds.IsNullOrEmpty())
        //{
        //    item.ResearchFieldIds = model.ResearchFieldIds ?? "";
        //}
        _canboRepository.Update(item);   
        await _canboRepository.SaveChangesAsync();
    }
    public async Task DeleteAsync(long id, long deteledBy)
    {
        var item = await _canboRepository.Select().Where(p => p.Id == id).FirstOrDefaultAsync();
        _canboRepository.Delete(item);
        await _canboRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"cán bộ với mã #{item.Ma} và tên {item.HoVaTen} thành công.",
            Params = item.Ma ?? "",
            Target = "Cán Bộ",
            TargetCode = item.Ma,
            UserId = deteledBy
        };
        await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
    }

    #endregion

    #region Cong bo khoa hoc

    public async Task<(IEnumerable<CongBoKHCN>?, int)> FilterPublicationAsync(CongBoKHCNFilter model)
    {
     
        var query = _congBoKHCNRepository.Select();

        query = !string.IsNullOrEmpty(model.LoaiCongBo)
           ? query.Where(p => p.LoaiCongBo != null && p.LoaiCongBo.ToLower().Contains(model.LoaiCongBo.ToLower()))
           : query;

        query = !string.IsNullOrEmpty(model.TenCongBo)
          ? query.Where(p => p.TenCongBo != null && p.TenCongBo.ToLower().Contains(model.TenCongBo.ToLower()))
          : query;

        query = !string.IsNullOrEmpty(model.NguonTrich)
        ? query.Where(p => p.NguonTrich != null && p.NguonTrich.ToLower().Contains(model.NguonTrich.ToLower()))
        : query;


        query = !string.IsNullOrEmpty(model.ISBN)
       ? query.Where(p => p.ISBN != null && p.ISBN.ToLower().Contains(model.ISBN.ToLower()))
       : query;


        query = !string.IsNullOrEmpty(model.URL)
       ? query.Where(p => p.URL != null && p.URL.ToLower().Contains(model.URL.ToLower()))
       : query;

        query = model.MaCanBo > 0
         ? query.Where(p => p.MaCanBo == model.MaCanBo)
         : query;

        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.TenCongBo != null && p.TenCongBo.ToLower().Contains(model.Keyword.ToLower()) ||
                p.ISBN != null && p.ISBN.ToLower().Contains(model.Keyword.ToLower()))
            : query;
        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var total = await query.CountAsync();
        return (items, total);
    }

    public async Task<CongBoKHCN> GetPublicationByIdAsync(long id, bool isTracking = false)
    {
        var item = await _congBoKHCNRepository
            .Select(isTracking)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return item;
    }

    public async Task CreatePublicationAsync(CongBoKHCNDto model, long createdBy)
    {
        var officer = await _canboRepository
            .Select()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        var newItem = _mapper.Map<CongBoKHCN>(model);
        newItem.NgayTao = Utils.getCurrentDate();
        _congBoKHCNRepository.Insert(newItem);
        await _congBoKHCNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"tổ chức với mã #{newItem.TenCongBo} thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "CongBoKHCN",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);
    }

    public async Task UpdatePublicationAsync(long id, CongBoKHCNDto model, long updatedBy)
    {
        var officer = await _canboRepository
            .Select()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        var item = await GetPublicationByIdAsync(id, true);
        _mapper.Map(model, item);
        _congBoKHCNRepository.Update(item);
        await _congBoKHCNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"tổ chức với mã #{item.TenCongBo} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "CongBoKHCN",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeletePublicationAsync(long id, long deletedBy)
    {
        var item = await GetPublicationByIdAsync(id, true);
        _congBoKHCNRepository.Delete(item);
        await _congBoKHCNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"tổ chức với mã #{item.TenCongBo} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "OfficerPublication",
            TargetCode = item.Id.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

    #endregion

    #region HocVanCanBo
    public async Task<(IEnumerable<HocVanCanBoResponse>?, int)> HocVanCanBoPagingAsync(HocVanCanBoFilter model)
    {
        var query = _hocVanCanBoRepository.Select().AsNoTracking();

        query = model.MaCanBo > 0
         ? query.Where(p => p.MaCanBo == model.MaCanBo)
         : query;

        query = !string.IsNullOrEmpty(model.BacDaoTao)
         ? query.Where(p => p.BacDaoTao.ToLower().Contains(model.BacDaoTao.ToLower()))
         : query;

            query = !string.IsNullOrEmpty(model.TrinhDoDaoTao)
          ? query.Where(p => p.TrinhDoDaoTao.ToLower().Contains(model.TrinhDoDaoTao.ToLower()))
          : query;

            query = !string.IsNullOrEmpty(model.ChuyenNganh)
           ? query.Where(p => p.ChuyenNganh.ToLower().Contains(model.ChuyenNganh.ToLower()))
           : query;

            query = !string.IsNullOrEmpty(model.NoiDaoTao)
           ? query.Where(p => p.NoiDaoTao.ToLower().Contains(model.NoiDaoTao.ToLower()))
           : query;


            query = !string.IsNullOrEmpty(model.ChucDanhKHCN)
             ? query.Where(p => p.ChucDanhKHCN.ToLower().Contains(model.ChucDanhKHCN.ToLower()))
             : query;

            query = !string.IsNullOrEmpty(model.NamTotNghiep)
           ? query.Where(p => p.NamTotNghiep.ToLower().Contains(model.NamTotNghiep.ToLower()))
           : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var total = await query.CountAsync();

        return (_mapper.Map<IEnumerable<HocVanCanBoResponse>>(items), total);
    }

    public async Task<HocVanCanBoResponse> GetHocVanCanBo(long id)
    {
        var item = await _hocVanCanBoRepository.Select().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return _mapper.Map<HocVanCanBoResponse>(item);
    }

    public async Task<HocVanCanBoResponse> TaoHocVanCanBo(HocVanCanBoDto model, long createdBy)
    {
        //var isCodeExist = await _officerEducationrRepository.Select().FirstOrDefaultAsync(p => p.Ma == model.Ma);
        //if (isCodeExist != null) throw new ArgumentException("Mã cán bộ đã tồn tại");
        // check OfficerId
        var officer = await _canboRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        var item = _mapper.Map<HocVanCanBo>(model);
        item.NgayTao = Utils.getCurrentDate();
        item.NgayCapNhat = Utils.getCurrentDate();
        _hocVanCanBoRepository.Insert(item);
        await _hocVanCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"học vấn cán bộ {item.BacDaoTao} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "HocVanCanBo",
            TargetCode = item.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        // map response
        var response = _mapper.Map<HocVanCanBoResponse>(item);
        return response;
    }

    public async Task CapNhatHocVanCanBo(long id, HocVanCanBoDto model, long updatedBy)
    {
        var query = _hocVanCanBoRepository.Select().AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // check OfficerId
        var officer = await _canboRepository.Select().AsNoTracking().FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        _mapper.Map(model, item);
        _hocVanCanBoRepository.Update(item);
        await _hocVanCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"học vấn cán bộ với {item.BacDaoTao} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "HocVanCanBo",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaHocVanCanBo(long id, long updatedBy)
    {
        var query = _hocVanCanBoRepository.Select()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // pass
        _hocVanCanBoRepository.Delete(item);
        await _hocVanCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"học vấn cán bộ với {item.BacDaoTao} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "HocVanCanBo",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Delete);
    }
    #endregion

    #region NhiemVuCanBo
    public async Task<(IEnumerable<NhiemVuCanBoResponse>?, int)> NhiemVuCanBoPagingAsync(NhiemVuCanBoFilter model)
    {


        var query = _nhiemVuCanBoRepository.Select()
            .AsNoTracking();
        query = model.MaCanBo > 0
         ? query.Where(p => p.MaCanBo == model.MaCanBo)
         : query;

        query = !string.IsNullOrEmpty(model.TenNhiemVu)
       ? query.Where(p => p.TenNhiemVu.ToLower().Contains(model.TenNhiemVu))
       : query;

            query = !string.IsNullOrEmpty(model.VaiTroThamGia)
         ? query.Where(p => p.VaiTroThamGia.ToLower().Contains(model.VaiTroThamGia))
         : query;

        query = !string.IsNullOrEmpty(model.NamKetThuc)
        ? query.Where(p => p.NamKetThuc.ToLower().Contains(model.NamKetThuc))
        : query;


        query = !string.IsNullOrEmpty(model.NamBatDau)
       ? query.Where(p => p.NamBatDau.ToLower().Contains(model.NamBatDau))
       : query;


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var total = await query.CountAsync();

        return (_mapper.Map<IEnumerable<NhiemVuCanBoResponse>>(items), total);
    }

    public async Task<NhiemVuCanBoResponse> GetNhiemVuCanBo(long id)
    {
        var item = await _nhiemVuCanBoRepository.Select().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return _mapper.Map<NhiemVuCanBoResponse>(item);
    }

    public async Task<NhiemVuCanBoResponse> TaoNhiemVuCanBo(NhiemVuCanBoDto model, long createdBy)
    {
        // check OfficerId
        var officer = await _canboRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        var item = _mapper.Map<NhiemVuCanBo>(model);
        _nhiemVuCanBoRepository.Insert(item);
        await _nhiemVuCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.TenNhiemVu} và tên: {item.TenNhiemVu} thành công.",
            Params = item.TenNhiemVu ?? "",
            Target = "OfficerEducation",
            TargetCode = item.TenNhiemVu,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        // map response
        var response = _mapper.Map<NhiemVuCanBoResponse>(item);
        return response;
    }

    public async Task CapNhatNhiemVuCanBo(long id, NhiemVuCanBoDto model, long updatedBy)
    {
        var query = _nhiemVuCanBoRepository.Select()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // check OfficerId
        var officer = await _canboRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        _mapper.Map(model, item);
        _nhiemVuCanBoRepository.Update(item);
        await _nhiemVuCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.TenNhiemVu} và tên: {item.TenNhiemVu} thành công.",
            Params = item.TenNhiemVu ?? "",
            Target = "OfficerEducation",
            TargetCode = item.TenNhiemVu,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaNhiemVuCanBo(long id, long updatedBy)
    {
        var query = _nhiemVuCanBoRepository.Select()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // pass
        _nhiemVuCanBoRepository.Delete(item);
        await _nhiemVuCanBoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.Id} và tên: {item.TenNhiemVu} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "OfficerEducation",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Delete);
    }

    #endregion

    #region CanBoCongTac
    public async Task<(IEnumerable<CanBoCongTacResponse>?, int)> CanBoCongTacPagingAsync(CanBoCongTacFilter model)
    {
        var query = _canBoCongTacRepository.Select().AsNoTracking();
        query = model.MaCanBo > 0
           ? query.Where(p => p.MaCanBo == model.MaCanBo)
           : query;


        query = !string.IsNullOrEmpty(model.ThoiGianCongTac)
         ? query.Where(p => p.ThoiGianCongTac.ToLower().Contains(model.ThoiGianCongTac.ToLower()))
         : query;

        query = !string.IsNullOrEmpty(model.ViTriCongTac)
         ? query.Where(p => p.ViTriCongTac.ToLower().Contains(model.ViTriCongTac.ToLower()))
         : query;

        query = !string.IsNullOrEmpty(model.ToChucCongTac)
         ? query.Where(p => p.ToChucCongTac.ToLower().Contains(model.ToChucCongTac.ToLower()))
         : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var total = await query.CountAsync();

        return (_mapper.Map<IEnumerable<CanBoCongTacResponse>>(items), total);
    }

    public async Task<CanBoCongTacResponse> GetCanBoCongTac(long id)
    {
        var item = await _canBoCongTacRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return _mapper.Map<CanBoCongTacResponse>(item);
    }

    public async Task<CanBoCongTacResponse> TaoCanBoCongTac(CanBoCongTacDto model, long createdBy)
    {
        //var isCodeExist = await _canBoCongTacRepository.Select()
        //    .FirstOrDefaultAsync(p => p.Code == model.Code);
        //if (isCodeExist != null) throw new ArgumentException("Mã cán bộ đã tồn tại");
        // check OfficerId
        var officer = await _canboRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        var item = _mapper.Map<CanBoCongTac>(model);
        item.NgayTao = Utils.getCurrentDate();
        item.NgayCapNhat = Utils.getCurrentDate();
        _canBoCongTacRepository.Insert(item);
        await _canBoCongTacRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.MaCanBo} và tên: {item.MaCanBo} thành công.",
            Params = item.MaCanBo.ToString() ?? "",
            Target = "CanBoCongTac",
            TargetCode = item.MaCanBo.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        // map response
        var response = _mapper.Map<CanBoCongTacResponse>(item);
        return response;
    }

    public async Task CapNhatCanBoCongTac(long id, CanBoCongTacDto model, long updatedBy)
    {
        var query = _canBoCongTacRepository.Select().AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // check OfficerId
        var officer = await _canboRepository.Select()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == model.MaCanBo);
        if (officer == null) throw new ArgumentException("Cán bộ không tồn tại");

        // pass
        _mapper.Map(model, item);
        _canBoCongTacRepository.Update(item);
        await _canBoCongTacRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.MaCanBo} và tên: {item.MaCanBo} thành công.",
            Params = item.MaCanBo.ToString() ?? "",
            Target = "CanBoCongTac",
            TargetCode = item.MaCanBo.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaCanBoCongTac(long id, long updatedBy)
    {
        var query = _canBoCongTacRepository.Select()
            .AsQueryable();

        // check exist
        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");

        // pass
        _canBoCongTacRepository.Delete(item);
        await _canBoCongTacRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ cán bộ với mã {item.MaCanBo} và tên: {item.MaCanBo} thành công.",
            Params = item.MaCanBo.ToString() ?? "",
            Target = "OfficerEducation",
            TargetCode = item.MaCanBo.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Delete);
    }
    #endregion
}