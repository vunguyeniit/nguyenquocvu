using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RazorLight.Extensions;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories.CommonCategories;
using System.Globalization;
namespace SoKHCNVTAPI.Repositories;

public interface IToChucRepository
{
    Task<(IEnumerable<ToChuc>?, int)> FilterAsync(ToChucFilter model);
    Task<ToChuc?> GetByIdAsync(long id, bool isTracking = false);
    Task<ToChucResponse?> GetByCode(string code);
    Task<ToChuc> CreateAsync(ToChucDto model, long createdBy);
    Task UpdateAsync(long id, ToChucDto model, long updatedBy);
    Task DeleteAsync(long id, long deletedBy);

    // partners
    Task<(IEnumerable<DoiTacToChuc>?, int)> FilterPartnerAsync(DoiTacToChucFilter model);
    Task<DoiTacToChuc?> GetPartner(long id, bool isTracking = false);
    Task<DoiTacToChuc> CreatePartner(DoiTacToChucDto model, long createdBy);
    Task UpdatePartner(long id, DoiTacToChucDto model, long updatedBy);
    Task DeletePartner(long id, long deletedBy);

    // Staff
    Task<(IEnumerable<NhanSuToChuc>?, int)> FilterStaffAsync(NhanSuToChucFilter model);
    Task<NhanSuToChuc?> GetStaff(long id, bool isTracking = false);
    Task<NhanSuToChuc> CreateStaff(NhanSuToChucDto model, long createdBy);
    Task UpdateStaff(long id, NhanSuToChucDto model, long updatedBy);
    Task DeleteStaff(long id, long deletedBy);

    // Ket qua hoat dong
    Task<(IEnumerable<KQHDToChuc>?, int)> GetKQHDs(KQHDToChucFilter model);
    Task<KQHDToChuc?> GetKQHD(long id, bool isTracking = false);
    Task<KQHDToChuc> CreateKQHD(KQHDToChucDto model, long createdBy);
    Task UpdateKQHD(long id, KQHDToChucDto model, long updatedBy);
    Task DeleteKQHD(long id, long deletedBy);
}

public class ToChucRepository : IToChucRepository
{
    //private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IRepository<ToChuc> _tochucRepository;
    private readonly IRepository<KQHDToChuc> _kqhdToChucRepository;
    private readonly IRepository<NhanSuToChuc> _nhanSuToChucRepository;
    private readonly IRepository<DoiTacToChuc> _organizationPartnerRepository;
    private readonly IRepository<LoaiHinhToChuc> _loaiHinhToChucRepository;
    private readonly IRepository<HinhThucSoHuu> _ownershipFormRepository;
    private readonly IRepository<DinhDanhToChuc> _organizationIdentifierRepository;
    private readonly IRepository<NhiemVu> _nhiemVuRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNCRepository;

    private readonly IActivityLogRepository _activityLogRepository;
   
    public ToChucRepository(
        //DataContext context,
        IMapper mapper, IRepository<ToChuc> tochucRepository,
        IRepository<KQHDToChuc> kqhdToChucRepository,
        IRepository<NhanSuToChuc> nhanSuToChucRepository,
        IRepository<DoiTacToChuc> organizationPartnerRepository,
        IActivityLogRepository activityLogRepository,
        IRepository<HinhThucSoHuu> ownershipFormRepository,
        IRepository<DinhDanhToChuc> organizationIdentifierRepository,
        IRepository<LoaiHinhToChuc> loaiHinhToChucRepository,
        IRepository<NhiemVu> nhiemVuRepository
        , IRepository<LinhVucNghienCuu> linhVucNCRepository)
    {
        //_context = context;
        _mapper = mapper;
        _ownershipFormRepository = ownershipFormRepository;
        _tochucRepository = tochucRepository;
        _kqhdToChucRepository = kqhdToChucRepository;
        _nhanSuToChucRepository = nhanSuToChucRepository;
        _organizationPartnerRepository = organizationPartnerRepository;
        _loaiHinhToChucRepository = loaiHinhToChucRepository;
        _nhiemVuRepository = nhiemVuRepository;
        _activityLogRepository = activityLogRepository;
        _organizationIdentifierRepository = organizationIdentifierRepository;
        _linhVucNCRepository = linhVucNCRepository;
    }

    #region To Chuc
    public async Task<(IEnumerable<ToChuc>?, int)> FilterAsync(ToChucFilter model)
    {
        var query = _tochucRepository
            .Select();

        //search column
        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;

        query = !string.IsNullOrEmpty(model.TenToChuc)
            ? query.Where(p => p.TenToChuc.ToLower().Contains(model.TenToChuc.ToLower()))
            : query;    

        query = !string.IsNullOrEmpty(model.TinhThanh)
            ? query.Where(p => p.TinhThanh != null && p.TinhThanh.ToLower().Contains(model.TinhThanh.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.Ma)
           ? query.Where(p => p.Ma != null && p.Ma.ToLower().Contains(model.Ma.ToLower()))
           : query;

        //Search NgayCapNhat
        if (!string.IsNullOrEmpty(model.NgayCapNhat))
        {
            DateTime parsedDate;
            //parse chuỗi ngày tháng từ client
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

        //filter Id LinhVucNghienCuu 
        var queryLinhVucNghienCuu = _linhVucNCRepository.Select();
        if (!string.IsNullOrEmpty(model.LinhVucNC))
        {
            query = from tc in query
                    join lvnc in queryLinhVucNghienCuu on tc.LinhVucNC equals lvnc.Id.ToString()
                    where lvnc.Ten.ToLower().Contains(model.LinhVucNC.ToLower()) // Filter by Name
                    select tc;
        }
        //filter Id Loai Hinh 
        var queryLoaiHinhToChuc = _loaiHinhToChucRepository.Select();
        if (!string.IsNullOrEmpty(model.LoaiHinhToChuc))
        {
            
            query = from tc in query
                    join lhtc in queryLoaiHinhToChuc on tc.LoaiHinhToChuc equals lhtc.Id.ToString()
                    where lhtc.Ten.ToLower().Contains(model.LoaiHinhToChuc.ToLower()) // Filter by Name
                    select tc;
        }


        //sort by column
        if (!string.IsNullOrEmpty(model.order_by))
        {
            switch (model.order_by.ToLower())
            {
                case "ma":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.Ma) : query.OrderBy(p => p.Ma);
                    break;
                case "tentochuc":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TenToChuc) : query.OrderBy(p => p.TenToChuc);
                    break;
                case "linhvucnc":

                    query = model.sorted_by == "desc"
                              ? query.OrderByDescending(p => Convert.ToInt32(p.LinhVucNC))
                              : query.OrderBy(p => Convert.ToInt32(p.LinhVucNC));
                    break;
                case "loaihinhtochuc":

                    query = model.sorted_by == "desc"
                               ? query.OrderByDescending(p => p.LoaiHinhToChuc.Length).ThenByDescending(p => p.LoaiHinhToChuc)
                               : query.OrderBy(p => p.LoaiHinhToChuc.Length).ThenBy(p => p.LoaiHinhToChuc);
                    break;
                case "tinhthanh":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TinhThanh) : query.OrderBy(p => p.TinhThanh);
                    break;
                case "trangthai":
                    query = model.sorted_by == "desc" ? query.OrderByDescending(p => p.TrangThai) : query.OrderBy(p => p.TrangThai);
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

        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.TenToChuc != null && p.TenToChuc.ToLower().Contains(model.Keyword.ToLower()) ||
                p.DienThoai != null && p.DienThoai.ToLower().Contains(model.Keyword.ToLower()) ||
                p.DiaChi != null && p.DiaChi.ToLower().Contains(model.Keyword.ToLower()) ||
                p.CoQuanChuQuan != null && p.CoQuanChuQuan.ToLower().Contains(model.Keyword.ToLower()) ||
                p.TinhThanh != null && p.TinhThanh.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma.ToLower().Contains(model.Keyword.ToLower()) ||

                //search LinhVucNghienCuu
                p.LinhVucNC != null &&
                   queryLinhVucNghienCuu.Where(lvnc => lvnc.Id.ToString() == p.LinhVucNC)
                   .Any(lvnc => lvnc.Ten.ToLower().Contains(model.Keyword.ToLower())) ||

                // search LoaiHinh
                p.LoaiHinhToChuc != null &&
                    queryLoaiHinhToChuc.Where(tc => tc.Id.ToString() == p.LoaiHinhToChuc)
                    .Any(tc => tc.Ten.ToLower().Contains(model.Keyword.ToLower())))
            //)
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

    public async Task<ToChuc?> GetByIdAsync(long id, bool isTracking = false)
    {
        var query = _tochucRepository.Select();
        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        return item;
    }

    public async Task<ToChucResponse?> GetByCode(string code)
    {
        var organization = await _tochucRepository.Select().SingleOrDefaultAsync(p => p.Ma == code);
        if (organization != null)
        {
            var staffs = await _nhanSuToChucRepository
                .Select()
                .Where(p => p.MaToChuc == organization.Id)
                .ToListAsync();

            var partners = await _organizationPartnerRepository
                .Select()
                .Where(p => p.MaToChuc == organization.Id)
                .ToListAsync();

            //var organizationTypes = JsonConvert.DeserializeObject<List<long>>(organization.OrganizationTypeId);
            //var organizationType = await _organizationTypeRepository
            // .Select()
            // .Where(p => organizationTypes != null && organizationTypes.Contains(p.Id))
            // .ToListAsync();

            var result = _mapper.Map<ToChucResponse>(organization);
            result.NhanSuToChucs = staffs;
            result.DoiTacToChucs = partners;
            if (organization.LoaiHinhToChuc != null)
            {
                result.LoaiHinhToChucs = await _loaiHinhToChucRepository.Select().Where(p => organization.LoaiHinhToChuc.Contains(p.Id.ToString())).ToListAsync();
            }
            result._HinhThucSoHuu = await _ownershipFormRepository.Select().Where(p => p.Id == organization.HinhThucSoHuu).FirstOrDefaultAsync();
            return result;
        }
        return null;
    }

    public async Task<ToChuc> CreateAsync(ToChucDto model, long createdBy)
    {
        var query = _tochucRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Tổ chức đã tồn tại!");


        // check OwnershipFormsId
        var ownershipForms = await _ownershipFormRepository.Select()
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.HinhThucSoHuu);
        if (ownershipForms == null) throw new ArgumentException("Hình thức sở hữu không tồn tại!");

        // check lao hinh to chuc
        if(model.LoaiHinhToChuc != null)
        {
            string[]? loaiHinhToChucIds = model.LoaiHinhToChuc.Split(",");
            if(loaiHinhToChucIds != null)
            {
                foreach (var _id in loaiHinhToChucIds)
                {
                    var loaiHinhToChuc = await _loaiHinhToChucRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == long.Parse(_id));
                    if (loaiHinhToChuc == null) throw new ArgumentException("Loại hình tổ chức không tồn tại!");
                }
            }
        }
        // check OrganizationIdentifierId
        var organizationIdentifier = await _organizationIdentifierRepository.Select().AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.OrganizationIdentifierId);
        if (organizationIdentifier == null) throw new ArgumentException("Mã định danh tổ chức không tồn tại!");

        var newItem = _mapper.Map<ToChuc>(model);
        newItem.NguoiTao = createdBy;
        newItem.NgayTao = Utils.getCurrentDate();
        //OrganizationTypeId to array string
        _tochucRepository.Insert(newItem);
        await _tochucRepository.SaveChangesAsync();

        //call api
        await SyncUtils.SyncToChuc(newItem);

        var log = new ActivityLogDto
        {
            Contents = $"tổ chức với mã #{newItem.Ma} và tên {newItem.TenToChuc} thành công.",
            Params = newItem.Ma ?? "",
            Target = "ToChuc",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }
    
    public async Task UpdateAsync(long id, ToChucDto model, long updatedBy)
    {
        var item = await _tochucRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        // check OwnershipFormsId
        var ownershipForms = await _ownershipFormRepository.Select()
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.HinhThucSoHuu);
        if (ownershipForms == null) throw new ArgumentException("Hình thức sở hữu không tồn tại!");

        if (model.LoaiHinhToChuc != null)
        {
            string[]? loaiHinhToChucIds = model.LoaiHinhToChuc.Split(",");
            if (loaiHinhToChucIds != null)
            {
                foreach (var _id in loaiHinhToChucIds)
                {
                    var loaiHinhToChuc = await _loaiHinhToChucRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == long.Parse(_id));
                    if (loaiHinhToChuc == null) throw new ArgumentException("Loại hình tổ chức không tồn tại!");
                }
            }
        }

        // check OrganizationTypeId
        //var organizationType = _loaiHinhToChucRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == model.LoaiHinhToChuc);
        //if (ownershipForms == null) throw new ArgumentException("Loại hình tổ chức không tồn tại!");
        //var organizationTypeIsAllExist = await organizationType.ToListAsync();
        //if (organizationTypeIsAllExist == null) throw new ArgumentException("Loại tổ chức không tồn tại!");

        // check OrganizationIdentifierId
        var organizationIdentifier = await _organizationIdentifierRepository.Select()
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.OrganizationIdentifierId);
        if (organizationIdentifier == null) throw new ArgumentException("Mã định danh tổ chức không tồn tại!");

        _mapper.Map(model, item);
        item.NguoiCapNhat = updatedBy;
        item.NgayCapNhat = DateTime.Now;
        _tochucRepository.Update(item);
        await _tochucRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"tổ chức với mã #{item.Ma} và tên {item.TenToChuc} thành công.",
            Params = item.Ma ?? "",
            Target = "ToChuc",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteAsync(long id, long deteledBy)
    {
        var relatedTasks = await _nhiemVuRepository.Select()
      .Where(p => p.OrganizationId == id)
      .ToListAsync();

        // Xóa tất cả các NhiemVu liên quan
    
        foreach (var task in relatedTasks)
        {
            _nhiemVuRepository.Delete(task);
        }
        await _nhiemVuRepository.SaveChangesAsync();

        // Lấy bản ghi ToChuc
        var item = await GetByIdAsync(id, true);
        if (item != null)
        {
            _tochucRepository.Delete(item);
            await _tochucRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"tổ chức với mã #{item.Ma} và tên {item.TenToChuc} thành công.",
                Params = item.Ma ?? "",
                Target = "ToChuc",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region Doi Tac

    public async Task<(IEnumerable<DoiTacToChuc>?, int)> FilterPartnerAsync(DoiTacToChucFilter model)
    {
        var query = _organizationPartnerRepository
            .Select();

        query = !string.IsNullOrEmpty(model.TenDoiTac)
            ? query.Where(p => p.TenDoiTac.ToLower().Contains(model.TenDoiTac.ToLower()))
            : query;

        query = model.Loai.HasValue ? query.Where(p => p.Loai == model.Loai) : query;

        query = !string.IsNullOrEmpty(model.NamThamGia)
            ? query.Where(p => p.NamThamGia != null && p.NamThamGia.ToLower().Contains(model.NamThamGia.ToLower()))
            : query;

        query = !string.IsNullOrEmpty(model.NoiDungHopTac)
           ? query.Where(p => p.NoiDungHopTac.ToLower().Contains(model.NoiDungHopTac.ToLower()))
           : query;

        query = model.MaToChuc.HasValue
            ? query.Where(p => p.MaToChuc == model.MaToChuc)
            : query;

        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.TenDoiTac.ToLower().Contains(model.Keyword.ToLower()) ||
                p.NamThamGia != null &&  p.NamThamGia.ToLower().Contains(model.Keyword.ToLower()) ||
                p.NoiDungHopTac != null && p.NoiDungHopTac.ToLower().Contains(model.Keyword.ToLower()))
            : query;


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<DoiTacToChuc> GetPartner(long id, bool isTracking = false)
    {
        var query = _organizationPartnerRepository.Select().AsQueryable();

        query = !isTracking ? query.AsNoTracking() : query;
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy đối tác!");
        return item;
    }

    public async Task<DoiTacToChuc> CreatePartner(DoiTacToChucDto model, long createdBy)
    {
        var query = _organizationPartnerRepository.Select().AsQueryable();

        var item = await query.FirstOrDefaultAsync(p => p.TenDoiTac == model.TenDoiTac);
        if (item != null) throw new ArgumentException("Đối tác đã tồn tại!");

        // check exist OrganizationId
        var organization = await _tochucRepository.Select()
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");

        // pass
        var newItem = _mapper.Map<DoiTacToChuc>(model);
        newItem.NguoiTao = createdBy;
        newItem.NgayTao = Utils.getCurrentDate();
        _organizationPartnerRepository.Insert(newItem);
        await _organizationPartnerRepository.SaveChangesAsync();
 
        var log = new ActivityLogDto
        {
            Contents = $"đối tác với tên {newItem.TenDoiTac} thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "DoiTacToChuc",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Update);
        return newItem;
    }

    public async Task UpdatePartner(long id, DoiTacToChucDto model, long updatedBy)
    {
        var item = await _organizationPartnerRepository.Select().SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy đối tác!");

        // check exist OrganizationId
        var organization = await _tochucRepository.Select()
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");

        // Mapping
        _mapper.Map(model, item);
        item.NgayCapNhat = Utils.getCurrentDate();
        item.NguoiCapNhat = updatedBy;
        _organizationPartnerRepository.Update(item);
        await _organizationPartnerRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"đối tác với tên {item.TenDoiTac} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "DoiTacToChuc",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeletePartner(long id, long deletedBy)
    {
        var item = await _organizationPartnerRepository.Select()
            .SingleOrDefaultAsync(p => p.Id == id);

        if (item == null) throw new ArgumentException("Không tìm thấy mã định tổ chức!");

        _organizationPartnerRepository.Delete(item);
        await _organizationPartnerRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"đối tác với tên {item.TenDoiTac} thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "DoiTacToChuc",
            TargetCode = item.Id.ToString(),
            UserId = deletedBy
        };
        await _activityLogRepository.SaveLogAsync(log, deletedBy, LogMode.Delete);
    }

    #endregion

    #region Staff
    public async Task<(IEnumerable<NhanSuToChuc>?, int)> FilterStaffAsync(NhanSuToChucFilter model)
    {

        var query = _nhanSuToChucRepository.Select();

        query = model.MaToChuc.HasValue
            ? query.Where(p => p.MaToChuc == model.MaToChuc)
            : query;

      

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayCapNhat)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }
    

    public async Task<NhanSuToChuc?> GetStaff(long id, bool isTracking = false)
    {
        var query = _nhanSuToChucRepository.Select().AsQueryable();
        query = !isTracking ? query.AsNoTracking() : query;
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);

        return item;
    }

    public async Task<NhanSuToChuc> CreateStaff(NhanSuToChucDto model, long createdBy)
    {
        var query = _nhanSuToChucRepository.Select();

        // Check is exist OrganizationId
        var organization = await _tochucRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");


        var newItem = _mapper.Map<NhanSuToChuc>(model);
        newItem.NgayCapNhat = Utils.getCurrentDate();
        newItem.ThoiGian = Utils.getCurrentDate();
        _nhanSuToChucRepository.Insert(newItem);
        await _nhanSuToChucRepository.SaveChangesAsync();


        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "NhanSuToChuc",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task UpdateStaff(long id, NhanSuToChucDto model, long updatedBy)
    {
        var item = await _nhanSuToChucRepository.Select().SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy đối tác!");
        // Check is exist OrganizationId
        var organization = await _tochucRepository.Select().AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");


        _mapper.Map(model, item);
        item.NgayCapNhat = Utils.getCurrentDate();
        item.ThoiGian = Utils.getCurrentDate();

        _nhanSuToChucRepository.Update(item);
        await _nhanSuToChucRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "NhanSuToChuc",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteStaff(long id, long createdBy)
    {
        var item = await _nhanSuToChucRepository.Select().SingleOrDefaultAsync(p => p.Id == id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");

        _nhanSuToChucRepository.Delete(item);
        await _nhanSuToChucRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "NhanSuToChuc",
            TargetCode = item.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Delete);

    }

    #endregion

    #region Ket qua hoat dong

    public async Task<(IEnumerable<KQHDToChuc>?, int)> GetKQHDs(KQHDToChucFilter model)
    {
        var query = _kqhdToChucRepository.Select();

        query = model.MaToChuc.HasValue
            ? query.Where(p => p.MaToChuc == model.MaToChuc)
            : query;

        query = !string.IsNullOrEmpty(model.SoHuuTriTue)
          ? query.Where(p => p.SoHuuTriTue.ToLower().Contains(model.SoHuuTriTue.ToLower()))
          : query;


        query = !string.IsNullOrEmpty(model.SangKien)
          ? query.Where(p => p.SangKien.ToLower().Contains(model.SangKien.ToLower()))
          : query;


        query = !string.IsNullOrEmpty(model.SanPhamCongNgheUngDung)
         ? query.Where(p => p.SanPhamCongNgheUngDung.ToLower().Contains(model.SanPhamCongNgheUngDung.ToLower()))
         : query;


        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.NgayTao)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();

        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<KQHDToChuc?> GetKQHD(long id, bool isTracking = false)
    {
        var query = _kqhdToChucRepository.Select().AsQueryable();

        query = !isTracking ? query.AsNoTracking() : query;

        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        return item;
    }

    public async Task<KQHDToChuc> CreateKQHD(KQHDToChucDto model, long createdBy)
    {
        var query = _kqhdToChucRepository.Select();

        // Check is exist OrganizationId
        var organization = await _tochucRepository.Select().AsNoTracking().SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");


        var newItem = _mapper.Map<KQHDToChuc>(model);
        newItem.NgayTao = Utils.getCurrentDate();
        newItem.ThoiGian = Utils.getCurrentDate();
        _kqhdToChucRepository.Insert(newItem);
        await _kqhdToChucRepository.SaveChangesAsync();


        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = newItem.Id.ToString() ?? "",
            Target = "KQHDToChuc",
            TargetCode = newItem.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task UpdateKQHD(long id, KQHDToChucDto model, long updatedBy)
    {
        var item = await _kqhdToChucRepository.Select().SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy đối tác!");

        // Check is exist OrganizationId
        var organization = await _tochucRepository.Select().AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == model.MaToChuc);
        if (organization == null) throw new ArgumentException("Tổ chức không tồn tại!");

        _mapper.Map(model, item);
        item.ThoiGian = Utils.getCurrentDate();

        _kqhdToChucRepository.Update(item);
        await _kqhdToChucRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "KQHDToChuc",
            TargetCode = item.Id.ToString(),
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task DeleteKQHD(long id, long createdBy)
    {
        var item = await _kqhdToChucRepository.Select().SingleOrDefaultAsync(p => p.Id == id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");

        _kqhdToChucRepository.Delete(item);
        await _kqhdToChucRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nhân sự tổ chức thành công.",
            Params = item.Id.ToString() ?? "",
            Target = "KQHDToChuc",
            TargetCode = item.Id.ToString(),
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Delete);

    }

    #endregion

}