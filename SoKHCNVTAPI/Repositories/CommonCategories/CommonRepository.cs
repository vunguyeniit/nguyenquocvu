using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Services;


namespace SoKHCNVTAPI.Repositories.CommonCategories;

public interface ICommonRepository
{
    // tham quyen thanh lap
    Task<(IEnumerable<ThamQuyenThanhLap>?, int)> GetThamQuyenThanhLaps(ThamQuyenThanhLapFilter model);
    Task<ThamQuyenThanhLap> GetThamQuyenThanhLap(long id, bool isTracking = false);
    Task<ThamQuyenThanhLap> TaoThamQuyenThanhLap(ThamQuyenThanhLapDto model, long createdBy);
    Task CapNhatThamQuyenThanhLap(long id, ThamQuyenThanhLapDto model, long updatedBy);
    Task XoaThamQuyenThanhLap(long id, long deteledBy);

    // DoTuoi
    Task<(IEnumerable<DoTuoi>?, int)> GetDoTuois(DoTuoiFilter model);
    Task<DoTuoi> GetDoTuoi(long id, bool isTracking = false);
    Task<DoTuoi> TaoDoTuoi(DoTuoiDto model, long createdBy);
    Task CapNhatDoTuoi(long id, DoTuoiDto model, long updatedBy);
    Task XoaDoTuoi(long id, long deteledBy);

    // HinhThucChuyenGiao
    Task<(IEnumerable<HinhThucChuyenGiao>?, int)> GetHinhThucChuyenGiaos(HinhThucChuyenGiaoFilter model);
    Task<HinhThucChuyenGiao> GetHinhThucChuyenGiao(long id, bool isTracking = false);
    Task<HinhThucChuyenGiao> TaoHinhThucChuyenGiao(HinhThucChuyenGiaoDto model, long createdBy);
    Task CapNhatHinhThucChuyenGiao(long id, HinhThucChuyenGiaoDto model, long updatedBy);
    Task XoaHinhThucChuyenGiao(long id, long deteledBy);
    //HinhThucThanhLap
    Task<(IEnumerable<HinhThucThanhLap>?, int)> GetHinhThucThanhLaps(HinhThucThanhLapFilter model);
    Task<HinhThucThanhLap> GetHinhThucThanhLap(long id, bool isTracking = false);
    Task<HinhThucThanhLap> TaoHinhThucThanhLap(HinhThucThanhLapDto model, long createdBy);
    Task CapNhatHinhThucThanhLap(long id,HinhThucThanhLapDto model, long updatedBy);
    Task XoaHinhThucThanhLap(long id, long deteledBy);
    //LinhVucDaoTaoKHCN
    Task<(IEnumerable<LinhVucDaoTaoKHCN>?, int)> GetLinhVucDaoTaoKHCNs(LinhVucDaoTaoKHCNFilter model);
    Task<LinhVucDaoTaoKHCN> GetLinhVucDaoTaoKHCN(long id, bool isTracking = false);
    Task<LinhVucDaoTaoKHCN> TaoLinhVucDaoTaoKHCN(LinhVucDaoTaoKHCNDto model, long createdBy);
    Task CapNhatLinhVucDaoTaoKHCN(long id, LinhVucDaoTaoKHCNDto model, long updatedBy);
    Task XoaLinhVucDaoTaoKHCN(long id, long deteledBy);
    //LinhVucKHCN
    Task<(IEnumerable<LinhVucKHCN>?, int)> GetLinhVucKHCNs(LinhVucKHCNFilter model);
    Task<LinhVucKHCN> GetLinhVucKHCN(long id, bool isTracking = false);
    Task<LinhVucKHCN> TaoLinhVucKHCN(LinhVucKHCNDto model, long createdBy);
    Task CapNhatLinhVucKHCN(long id, LinhVucKHCNDto model, long updatedBy);
    Task XoaLinhVucKHCN(long id, long deteledBy);
    //LoaiHinhToChucDN
    Task<(IEnumerable<LoaiHinhToChucDN>?, int)> GetLoaiHinhToChucDNs(LoaiHinhToChucDNFilter model);
    Task<LoaiHinhToChucDN> GetLoaiHinhToChucDN(long id, bool isTracking = false);
    Task<LoaiHinhToChucDN> TaoLoaiHinhToChucDN(LoaiHinhToChucDNDto model, long createdBy);
    Task CapNhatLoaiHinhToChucDN(long id, LoaiHinhToChucDNDto model, long updatedBy);
    Task XoaLoaiHinhToChucDN(long id, long deteledBy);
    //MucTieuKTXH
    Task<(IEnumerable<MucTieuKTXH>?, int)> GetMucTieuKTXHs(MucTieuKTXHFilter model);
    Task<MucTieuKTXH> GetMucTieuKTXH(long id, bool isTracking = false);
    Task<MucTieuKTXH> TaoMucTieuKTXH(MucTieuKTXHDto model, long createdBy);
    Task CapNhatMucTieuKTXH(long id,MucTieuKTXHDto model, long updatedBy);
    Task XoaMucTieuKTXH(long id, long deteledBy);
    //QuyChuanKT
    Task<(IEnumerable<QuyChuanKT>?, int)> GetQuyChuanKTs(QuyChuanKTFilter model);
    Task<QuyChuanKT> GetQuyChuanKT(long id, bool isTracking = false);
    Task<QuyChuanKT> TaoQuyChuanKT(QuyChuanKTDto model, long createdBy);
    Task CapNhatQuyChuanKT(long id, QuyChuanKTDto model, long updatedBy);
    Task XoaQuyChuanKT(long id, long deteledBy);
    //NguonCapKinhPhi
    Task<(IEnumerable<NguonCapKinhPhi>?, int)> GetNguonCapKinhPhis(NguonCapKinhPhiFilter model);
    Task<NguonCapKinhPhi> GetNguonCapKinhPhi(long id, bool isTracking = false);
    Task<NguonCapKinhPhi> TaoNguonCapKinhPhi(NguonCapKinhPhiDto model, long createdBy);
    Task CapNhatNguonCapKinhPhi(long id, NguonCapKinhPhiDto model, long updatedBy);
    Task XoaNguonCapKinhPhi(long id, long deteledBy);
    //DoiTacQT
    Task<(IEnumerable<DoiTacQT>?, int)> GetDoiTacQTs(DoiTacQTFilter model);
    Task<DoiTacQT> GetDoiTacQT(long id, bool isTracking = false);
    Task<DoiTacQT> TaoDoiTacQT(DoiTacQTDto model, long createdBy);
    Task CapNhatDoiTacQT(long id, DoiTacQTDto model, long updatedBy);
    Task XoaDoiTacQT(long id, long deteledBy);
    //HinhThucHTQT
    Task<(IEnumerable<HinhThucHTQT>?, int)> GetHinhThucHTQTs(HinhThucHTQTFilter model);
    Task<HinhThucHTQT> GetHinhThucHTQT(long id, bool isTracking = false);
    Task<HinhThucHTQT> TaoHinhThucHTQT(HinhThucHTQTDto model, long createdBy);
    Task CapNhatHinhThucHTQT(long id, HinhThucHTQTDto model, long updatedBy);
    Task XoaHinhThucHTQT(long id, long deteledBy);
    //NguonKPHTQT 
    Task<(IEnumerable<NguonKPHTQT>?, int)> GetNguonKPHTQTs(NguonKPHTQTFilter model);
    Task<NguonKPHTQT> GetNguonKPHTQT(long id, bool isTracking = false);
    Task<NguonKPHTQT> TaoNguonKPHTQT(NguonKPHTQTDto model, long createdBy);
    Task CapNhatNguonKPHTQT(long id, NguonKPHTQTDto model, long updatedBy);
    Task XoaNguonKPHTQT(long id, long deteledBy);
    //LinhVucNCHTQT
    Task<(IEnumerable<LinhVucNCHTQT>?, int)> GetLinhVucNCHTQTs(LinhVucNCHTQTFilter model);
    Task<LinhVucNCHTQT> GetLinhVucNCHTQT(long id, bool isTracking = false);
    Task<LinhVucNCHTQT> TaoLinhVucNCHTQT(LinhVucNCHTQTDto model, long createdBy);
    Task CapNhatLinhVucNCHTQT(long id, LinhVucNCHTQTDto model, long updatedBy);
    Task XoaLinhVucNCHTQT(long id, long deteledBy);
    //LinhVucUngDung
    Task<(IEnumerable<LinhVucUngDung>?, int)> GetLinhVucUngDungs(LinhVucUngDungFilter model);
    Task<LinhVucUngDung> GetLinhVucUngDung(long id, bool isTracking = false);
    Task<LinhVucUngDung> TaoLinhVucUngDung(LinhVucUngDungDto model, long createdBy);
    Task CapNhatLinhVucUngDung(long id, LinhVucUngDungDto model, long updatedBy);
    Task XoaLinhVucUngDung(long id, long deteledBy);
    // MauPhuongTienDo
    Task<(IEnumerable<MauPhuongTienDo>?, int)> GetMauPhuongTienDos(MauPhuongTienDoFilter model);
    Task<MauPhuongTienDo> GetMauPhuongTienDo(long id, bool isTracking = false);
    Task<MauPhuongTienDo> TaoMauPhuongTienDo(MauPhuongTienDoDto model, long createdBy);
    Task CapNhatMauPhuongTienDo(long id, MauPhuongTienDoDto model, long updatedBy);
    Task XoaMauPhuongTienDo(long id, long deteledBy);
    //ChucDanh
    Task<(IEnumerable<ChucDanh>?, int)> GetChucDanhs(ChucDanhFilter model);
    Task<ChucDanh> GetChucDanh(long id, bool isTracking = false);
    Task<ChucDanh> TaoChucDanh(ChucDanhDto model, long createdBy);
    Task CapNhatChucDanh(long id, ChucDanhDto model, long updatedBy);
    Task XoaChucDanh(long id, long deteledBy);
    //QuocTich
    Task<(IEnumerable<QuocTich>?, int)> GetQuocTichs(QuocTichFilter model);
    Task<QuocTich> GetQuocTich(long id, bool isTracking = false);
    Task<QuocTich> TaoQuocTich(QuocTichDto model, long createdBy);
    Task CapNhatQuocTich(long id, QuocTichDto model, long updatedBy);
    Task XoaQuocTich(long id, long deteledBy);

    //TrinhDoChuyenMon
    Task<(IEnumerable<TrinhDoChuyenMon>?, int)> GetTrinhDoChuyenMons(TrinhDoChuyenMonFilter model);
    Task<TrinhDoChuyenMon> GetTrinhDoChuyenMon(long id, bool isTracking = false);
    Task<TrinhDoChuyenMon> TaoTrinhDoChuyenMon(TrinhDoChuyenMonDto model, long createdBy);
    Task CapNhatTrinhDoChuyenMon(long id,TrinhDoChuyenMonDto model, long updatedBy);
    Task XoaTrinhDoChuyenMon(long id, long deteledBy);

    //LoaiHinhNhiemVu
    Task<(IEnumerable<LoaiHinhNhiemVu>?, int)> GetLoaiHinhNhiemVus(LoaiHinhNhiemVuFilter model);
    Task<LoaiHinhNhiemVu> GetLoaiHinhNhiemVu(long id, bool isTracking = false);
    Task<LoaiHinhNhiemVu> TaoLoaiHinhNhiemVu(LoaiHinhNhiemVuDto model, long createdBy);
    Task CapNhatLoaiHinhNhiemVu(long id, LoaiHinhNhiemVuDto model, long updatedBy);
    Task XoaLoaiHinhNhiemVu(long id, long deteledBy);

    //CapQuanLy
    Task<(IEnumerable<CapQuanLy>?, int)> GetCapQuanLys(CapQuanLyFilter model);
    Task<CapQuanLy> GetCapQuanLy(long id, bool isTracking = false);
    Task<CapQuanLy> TaoCapQuanLy(CapQuanLyDto model, long createdBy);
    Task CapNhatCapQuanLy(long id, CapQuanLyDto model, long updatedBy);
    Task XoaCapQuanLy(long id, long deteledBy);

    //LinhVucNghienCuu
    Task<(IEnumerable<LinhVucNghienCuu>?, int)> GetLinhVucNghienCuus(LinhVucNghienCuuFilter model);
    Task<LinhVucNghienCuu> GetLinhVucNghienCuu(long id, bool isTracking = false);
    Task<LinhVucNghienCuu> TaoLinhVucNghienCuu(LinhVucNghienCuuDto model, long createdBy);
    Task CapNhatLinhVucNghienCuu(long id, LinhVucNghienCuuDto model, long updatedBy);
    Task XoaLinhVucNghienCuu(long id, long deteledBy);


    //CapQuanLyHTQT
    Task<(IEnumerable<CapQuanLyHTQT>?, int)> GetCapQuanLyHTQTs(CapQuanLyHTQTFilter model);
    Task<CapQuanLyHTQT> GetCapQuanLyHTQT(long id, bool isTracking = false);
    Task<CapQuanLyHTQT> TaoCapQuanLyHTQT(CapQuanLyHTQTDto model, long createdBy);
    Task CapNhatCapQuanLyHTQT(long id, CapQuanLyHTQTDto model, long updatedBy);
    Task XoaCapQuanLyHTQT(long id, long deteledBy);

    //LinhVucNghienCuuTTQT
    Task<(IEnumerable<LinhVucNghienCuuTTQT>?, int)> GetLinhVucNghienCuuTTQTs(LinhVucNghienCuuTTQTFilter model);
    Task<LinhVucNghienCuuTTQT> GetLinhVucNghienCuuTTQT(long id, bool isTracking = false);
    Task<LinhVucNghienCuuTTQT> TaoLinhVucNghienCuuTTQT(LinhVucNghienCuuTTQTDto model, long createdBy);
    Task CapNhatLinhVucNghienCuuTTQT(long id, LinhVucNghienCuuTTQTDto model, long updatedBy);
    Task XoaLinhVucNghienCuuTTQT(long id, long deteledBy);
    //NguonCapKPEnum
    Task<(IEnumerable<NguonCapKPEnum>?, int)> GetNguonCapKPEnums(NguonCapKPEnumFilter model);
    Task<NguonCapKPEnum> GetNguonCapKPEnum(long id, bool isTracking = false);
    Task<NguonCapKPEnum> TaoNguonCapKPEnum(NguonCapKPEnumDto model, long createdBy);
    Task CapNhatNguonCapKPEnum(long id, NguonCapKPEnumDto model, long updatedBy);
    Task XoaNguonCapKPEnum(long id, long deteledBy);
    //LoaiHinhToChuc
    Task<(IEnumerable<LoaiHinhToChuc>?, int)> GetLoaiHinhToChucs(LoaiHinhToChucFilter model);
    Task<LoaiHinhToChuc> GetLoaiHinhToChuc(long id, bool isTracking = false);
    Task<LoaiHinhToChuc> TaoLoaiHinhToChuc(LoaiHinhToChucDto model, long createdBy);
    Task CapNhatLoaiHinhToChuc(long id, LoaiHinhToChucDto model, long updatedBy);
    Task XoaLoaiHinhToChuc(long id, long deteledBy);

    //Tieu chuan

    //Task<(IEnumerable<TieuChuan>?, int)> GetTieuChuans(TieuChuanFilter model);
    //Task<LoaiHinhToChuc> GetTieuChuan(long id, bool isTracking = false);
    //Task<LoaiHinhToChuc> TaoTieuChuan(TieuChuanDto model, long createdBy);
    //Task CapNhatTieuChuan(long id, TieuChuanDto model, long updatedBy);
    //Task XoaTieuChuan(long id, long deteledBy);
}

public class CommonRepository : ICommonRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<DoTuoi> _doTuoiRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMemoryCachingService _cachingService;
    private readonly IRepository<ThamQuyenThanhLap> _thamQuyenThanhLapRepository;
    private readonly IRepository<HinhThucChuyenGiao> _hinhThucChuyenGiaoRepository;
    private readonly IRepository<HinhThucThanhLap> _hinhThucThanhLapRepository;
    private readonly IRepository<LinhVucDaoTaoKHCN> _linhVucDaoTaoKHCNRepository;
    private readonly IRepository<LinhVucKHCN> _linhVucKHCNRepository;
    private readonly IRepository<LoaiHinhToChucDN> _loaiHinhToChucDNRepository;
    private readonly IRepository<MucTieuKTXH> _mucTieuKTXHRepository;
    private readonly IRepository<QuyChuanKT> _quyChuanKTRepository;
    private readonly IRepository<NguonCapKinhPhi> _nguonCapKinhPhiRepository;
    private readonly IRepository<DoiTacQT> _doiTacQTRepository;
    private readonly IRepository<HinhThucHTQT> _hinhThucHTQTRepository;
    private readonly IRepository<NguonKPHTQT> _nguonKPHTQTRepository;
    private readonly IRepository<LinhVucNCHTQT> _linhVucNCHTQTRepository;
    private readonly IRepository<LinhVucUngDung> _linhVucUngDungRepository;
    private readonly IRepository<MauPhuongTienDo> _mauPhuongTienDoRepository;
    private readonly IRepository<ChucDanh> _chucDanhRepository;
    private readonly IRepository<QuocTich> _quocTichRepository;
    private readonly IRepository<TrinhDoChuyenMon> _trinhDoChuyenMonRepository;
    private readonly IRepository<LoaiHinhNhiemVu> _loaiHinhNhiemVuRepository;
    private readonly IRepository<CapQuanLy> _capQuanLyRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNghienCuuRepository;
    private readonly IRepository<CapQuanLyHTQT> _capQuanLyHTQTRepository;
    private readonly IRepository<LinhVucNghienCuuTTQT> _linhVucNghienCuuTTQTRepository;
    private readonly IRepository<NguonCapKPEnum> _nguonCapKPEnumRepository;
    private readonly IRepository<LoaiHinhToChuc> _loaiHinhToChucRepository;
    //
    private readonly IRepository<TieuChuan> _tieuChuanRepository;
    public CommonRepository(
        IMapper mapper,
        IMemoryCachingService cachingService,
        IRepository<DoTuoi> doTuoiRepository,
        IRepository<User> userRepository,
        IRepository<ThamQuyenThanhLap> thamQuyenThanhLapRepository,
        IRepository<HinhThucChuyenGiao> hinhThucChuyenGiaoRepository,
        IRepository<HinhThucThanhLap> hinhThucThanhLapRepository,
        IRepository<LinhVucDaoTaoKHCN> linhVucDaoTaoKHCNRepository,
        IRepository<LinhVucKHCN> linhVucKHCNRepository,
        IRepository<LoaiHinhToChucDN> loaiHinhToChucDNRepository,
        IRepository<MucTieuKTXH> mucTieuKTXHRepository,
        IRepository<QuyChuanKT> quyChuanKTRepository,
        IRepository<NguonCapKinhPhi> nguonCapKinhPhiRepository,
        IRepository<DoiTacQT> doiTacQTRepository,
        IRepository<HinhThucHTQT> hinhThucHTQTRepository,
        IRepository<NguonKPHTQT> nguonKPHTQTRepository,
        IRepository<LinhVucNCHTQT> linhVucNCHTQTRepository,
        IRepository<LinhVucUngDung> linhVucUngDungRepository,
        IRepository<MauPhuongTienDo> mauPhuongTienDoRepository,
        IRepository<ChucDanh> chucDanhRepository,
        IRepository<QuocTich> quocTichRepository,
        IRepository<TrinhDoChuyenMon> trinhDoChuyenMonRepository,
        IRepository<LoaiHinhNhiemVu> loaiHinhNhiemVuRepository,
        IRepository<CapQuanLy> capQuanLyRepository,
        IRepository<LinhVucNghienCuu> linhVucNghienCuuRepository,
        IRepository<CapQuanLyHTQT> capQuanLyHTQTRepository,
        IRepository<LinhVucNghienCuuTTQT> linhVucNghienCuuTTQTRepository,
        IRepository<NguonCapKPEnum> nguonCapKPEnumRepository,
        IRepository<LoaiHinhToChuc> loaiHinhToChucRepository,

         IRepository<TieuChuan> tieuChuanRepository,

        IActivityLogRepository activityLogRepository)
    {
        _mapper = mapper;
        _cachingService = cachingService;
        _doTuoiRepository = doTuoiRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _thamQuyenThanhLapRepository = thamQuyenThanhLapRepository;
        _hinhThucChuyenGiaoRepository = hinhThucChuyenGiaoRepository;
        _hinhThucThanhLapRepository = hinhThucThanhLapRepository;
        _linhVucDaoTaoKHCNRepository = linhVucDaoTaoKHCNRepository;
        _linhVucKHCNRepository = linhVucKHCNRepository;
        _loaiHinhToChucDNRepository = loaiHinhToChucDNRepository;
        _mucTieuKTXHRepository = mucTieuKTXHRepository;
        _quyChuanKTRepository = quyChuanKTRepository;
        _nguonCapKinhPhiRepository = nguonCapKinhPhiRepository;
        _doiTacQTRepository = doiTacQTRepository;
        _hinhThucHTQTRepository = hinhThucHTQTRepository;
        _nguonKPHTQTRepository = nguonKPHTQTRepository;
        _linhVucNCHTQTRepository = linhVucNCHTQTRepository;
        _linhVucUngDungRepository = linhVucUngDungRepository;
        _mauPhuongTienDoRepository = mauPhuongTienDoRepository;
        _chucDanhRepository = chucDanhRepository;
        _quocTichRepository = quocTichRepository;
        _trinhDoChuyenMonRepository = trinhDoChuyenMonRepository;
        _loaiHinhNhiemVuRepository = loaiHinhNhiemVuRepository;
        _capQuanLyRepository = capQuanLyRepository;
        _linhVucNghienCuuRepository = linhVucNghienCuuRepository;
        _capQuanLyHTQTRepository = capQuanLyHTQTRepository;
        _linhVucNghienCuuTTQTRepository = linhVucNghienCuuTTQTRepository;
        _nguonCapKPEnumRepository = nguonCapKPEnumRepository;
        _loaiHinhToChucRepository = loaiHinhToChucRepository;

        _tieuChuanRepository = tieuChuanRepository;
    }

    #region Tham Quyen Thanh Lap
    public async Task<(IEnumerable<ThamQuyenThanhLap>?, int)> GetThamQuyenThanhLaps(ThamQuyenThanhLapFilter model)
    {
        var query = _thamQuyenThanhLapRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<ThamQuyenThanhLap> GetThamQuyenThanhLap(long id, bool isTracking = false)
    {
        var query = _thamQuyenThanhLapRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy thẩm quyền thành lập!");
        return item;
    }

    public async Task<ThamQuyenThanhLap> TaoThamQuyenThanhLap(ThamQuyenThanhLapDto model, long createdBy)
    {
        var query = _thamQuyenThanhLapRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Thẩm quyền thành lập đã tồn tại!");

        var newItem = _mapper.Map<ThamQuyenThanhLap>(model);
        newItem.NguoiCapNhat = createdBy;
        _thamQuyenThanhLapRepository.Insert(newItem);
        await _thamQuyenThanhLapRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"thẩm quyền thành lập với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "ThamQuyenThanhLap",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatThamQuyenThanhLap(long id, ThamQuyenThanhLapDto model, long updatedBy)
    {
        var query = _thamQuyenThanhLapRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã thẩm quyền thành lập đã tồn tại!");

        _mapper.Map(model, item);
        item.NguoiCapNhat = updatedBy;
        _thamQuyenThanhLapRepository.Update(item);
        await _thamQuyenThanhLapRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"thẩm quyền thành lập với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "ThamQuyenThanhLap",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaThamQuyenThanhLap(long id, long deteledBy)
    {
        var item = await _thamQuyenThanhLapRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _thamQuyenThanhLapRepository.Delete(item);
            await _thamQuyenThanhLapRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"thẩm quyền thành lập với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "ThamQuyenThanhLap",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region HinhThucChuyenGiao
    public async Task<(IEnumerable<HinhThucChuyenGiao>?, int)> GetHinhThucChuyenGiaos(HinhThucChuyenGiaoFilter model)
    {
        var query = _hinhThucChuyenGiaoRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<HinhThucChuyenGiao> GetHinhThucChuyenGiao(long id, bool isTracking = false)
    {
        var query = _hinhThucChuyenGiaoRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy hình thức chuyển giao.");
        return item;
    }

    public async Task<HinhThucChuyenGiao> TaoHinhThucChuyenGiao(HinhThucChuyenGiaoDto model, long createdBy)
    {
        var query = _hinhThucChuyenGiaoRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Hình thức chuyển giao đã tồn tại!");

        var newItem = _mapper.Map<HinhThucChuyenGiao>(model);
        _hinhThucChuyenGiaoRepository.Insert(newItem);
        await _hinhThucChuyenGiaoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"hình thức chuyển giao với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "HinhThucChuyenGiao",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatHinhThucChuyenGiao(long id, HinhThucChuyenGiaoDto model, long updatedBy)
    {
        var query = _hinhThucChuyenGiaoRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã hình thức chuyển giao đã tồn tại!");

        _mapper.Map(model, item);
        _hinhThucChuyenGiaoRepository.Update(item);
        await _hinhThucChuyenGiaoRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"hình thức chuyển giao với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "HinhThucChuyenGiao",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaHinhThucChuyenGiao(long id, long deteledBy)
    {
        var item = await _hinhThucChuyenGiaoRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _hinhThucChuyenGiaoRepository.Delete(item);
            await _hinhThucChuyenGiaoRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"hình thức chuyển giao với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "HinhThucChuyenGiao",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region HinhThucThanhLap
   
    public async Task<(IEnumerable<HinhThucThanhLap>?, int)> GetHinhThucThanhLaps(HinhThucThanhLapFilter model)
    {
        var query = _hinhThucThanhLapRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<HinhThucThanhLap> GetHinhThucThanhLap(long id, bool isTracking = false)
    {
        var query = _hinhThucThanhLapRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy hình thức thành lập.");
        return item;
    }

    public async Task<HinhThucThanhLap> TaoHinhThucThanhLap(HinhThucThanhLapDto model, long createdBy)
    {
        var query = _hinhThucThanhLapRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Hình thức thành lập đã tồn tại!");

        var newItem = _mapper.Map<HinhThucThanhLap>(model);
        _hinhThucThanhLapRepository.Insert(newItem);
        await _hinhThucThanhLapRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Hình thức thành lập với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "HinhThucThanhLap",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatHinhThucThanhLap(long id, HinhThucThanhLapDto model, long updatedBy)
    {
        var query = _hinhThucThanhLapRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã hình thức thành lập đã tồn tại!");

        _mapper.Map(model, item);
        _hinhThucThanhLapRepository.Update(item);
        await _hinhThucThanhLapRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"hình thức thành lập với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "HinhThucThanhLap",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaHinhThucThanhLap(long id, long deteledBy)
    {
        var item = await _hinhThucThanhLapRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
           _hinhThucThanhLapRepository.Delete(item);
            await _hinhThucThanhLapRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"hình thức thành lập với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "HinhThucThanhLap",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region DoTuoi

    public async Task<(IEnumerable<DoTuoi>?, int)> GetDoTuois(DoTuoiFilter model)
    {
        var query = _doTuoiRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<DoTuoi> GetDoTuoi(long id, bool isTracking = false)
    {
        var query = _doTuoiRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy độ tuổi.");
        return item;
    }

    public async Task<DoTuoi> TaoDoTuoi(DoTuoiDto model, long createdBy)
    {
        var query = _doTuoiRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Độ tuổi đã tồn tại!");

        var newItem = _mapper.Map<DoTuoi>(model);
       _doTuoiRepository.Insert(newItem);
        await _doTuoiRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"độ tuổi với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "DoTuoi",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }
    
    public async Task CapNhatDoTuoi(long id, DoTuoiDto model, long updatedBy)
    {
        var query = _doTuoiRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã độ tuổi đã tồn tại!");

        _mapper.Map(model, item);
       _doTuoiRepository.Update(item);
        await _doTuoiRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"độ tuổi với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "DoTuoi",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaDoTuoi(long id, long deteledBy)
    {
        var item = await _doTuoiRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
           _doTuoiRepository.Delete(item);
            await _doTuoiRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"độ tuổi với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "DoTuoi",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region LinhVucDaoTaoKHCN

    public async Task<(IEnumerable<LinhVucDaoTaoKHCN>?, int)> GetLinhVucDaoTaoKHCNs(LinhVucDaoTaoKHCNFilter model)
    {
        var query = _linhVucDaoTaoKHCNRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucDaoTaoKHCN> GetLinhVucDaoTaoKHCN(long id, bool isTracking = false)
    {
        var query = _linhVucDaoTaoKHCNRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy lĩch vực đào tạo KHCN.");
        return item;
    }

    public async Task<LinhVucDaoTaoKHCN> TaoLinhVucDaoTaoKHCN(LinhVucDaoTaoKHCNDto model, long createdBy)
    {
        var query = _linhVucDaoTaoKHCNRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Lĩnh vực đào tạo KHCN đã tồn tại!");

        var newItem = _mapper.Map<LinhVucDaoTaoKHCN>(model);
       _linhVucDaoTaoKHCNRepository.Insert(newItem);
        await _linhVucDaoTaoKHCNRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Lĩnh vực đào tạo KHCN với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucDaoTaoKHCN",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucDaoTaoKHCN(long id, LinhVucDaoTaoKHCNDto model, long updatedBy)
    {
        var query = _linhVucDaoTaoKHCNRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã lĩch vực đào tạo KHCN đã tồn tại!");

        _mapper.Map(model, item);
       _linhVucDaoTaoKHCNRepository.Update(item);
        await _linhVucDaoTaoKHCNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"Lĩnh vực đào tạo KHCN với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucDaoTaoKHCN",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucDaoTaoKHCN(long id, long deteledBy)
    {
        var item = await _linhVucDaoTaoKHCNRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _linhVucDaoTaoKHCNRepository.Delete(item);
            await _linhVucDaoTaoKHCNRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"Lĩnh vực đào tạo KHCN với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucDaoTaoKHCN",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region LinhVucKHCN
    public async Task<(IEnumerable<LinhVucKHCN>?, int)> GetLinhVucKHCNs(LinhVucKHCNFilter model)
    {
        var query = _linhVucKHCNRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucKHCN> GetLinhVucKHCN(long id, bool isTracking = false)
    {
        var query =_linhVucKHCNRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LinhVucKHCN> TaoLinhVucKHCN(LinhVucKHCNDto model, long createdBy)
    {
        var query = _linhVucKHCNRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Lĩnh vực KHCN đã tồn tại!");

        var newItem = _mapper.Map<LinhVucKHCN>(model);
       _linhVucKHCNRepository.Insert(newItem);
        await _linhVucKHCNRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Lĩnh vực KHCN với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucKHCN",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucKHCN(long id, LinhVucKHCNDto model, long updatedBy)
    {
        var query = _linhVucKHCNRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
       _linhVucKHCNRepository.Update(item);
        await _linhVucKHCNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"Lĩnh vực KHCN với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucKHCN",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucKHCN(long id, long deteledBy)
    {
        var item = await _linhVucKHCNRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
           _linhVucKHCNRepository.Delete(item);
            await _linhVucKHCNRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"Lĩnh vực KHCN với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucKHCN",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region LoaiHinhToChucDN

    public async Task<(IEnumerable<LoaiHinhToChucDN>?, int)> GetLoaiHinhToChucDNs(LoaiHinhToChucDNFilter model)
    {
        var query = _loaiHinhToChucDNRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LoaiHinhToChucDN> GetLoaiHinhToChucDN(long id, bool isTracking = false)
    {
        var query = _loaiHinhToChucDNRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm loại hình tổ chức doanh nghiệp.");
        return item;
    }

    public async Task<LoaiHinhToChucDN> TaoLoaiHinhToChucDN(LoaiHinhToChucDNDto model, long createdBy)
    {
        var query = _loaiHinhToChucDNRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Loại hình tổ chức DN đã tồn tại!");

        var newItem = _mapper.Map<LoaiHinhToChucDN>(model);
        _loaiHinhToChucDNRepository.Insert(newItem);
        await _loaiHinhToChucDNRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Loại hình tổ chức DN với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LoaiHinhToChucDN",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLoaiHinhToChucDN(long id, LoaiHinhToChucDNDto model, long updatedBy)
    {
        var query = _loaiHinhToChucDNRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã loại hình tổ chức doanh nghiệp đã tồn tại!");

        _mapper.Map(model, item);
       _loaiHinhToChucDNRepository.Update(item);
        await _loaiHinhToChucDNRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"Loại hình tổ chức DN với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LoaiHinhToChucDN",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLoaiHinhToChucDN(long id, long deteledBy)
    {
        var item = await _loaiHinhToChucDNRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _loaiHinhToChucDNRepository.Delete(item);
            await _loaiHinhToChucDNRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"Loại hình tổ chức DN với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LoaiHinhToChucDN",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region MucTieuKTXH

    public async Task<(IEnumerable<MucTieuKTXH>?, int)> GetMucTieuKTXHs(MucTieuKTXHFilter model)
    {
        var query = _mucTieuKTXHRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<MucTieuKTXH> GetMucTieuKTXH(long id, bool isTracking = false)
    {
        var query = _mucTieuKTXHRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<MucTieuKTXH> TaoMucTieuKTXH(MucTieuKTXHDto model, long createdBy)
    {
        var query = _mucTieuKTXHRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("Mục tiêu KTXH đã tồn tại!");

        var newItem = _mapper.Map<MucTieuKTXH>(model);
        _mucTieuKTXHRepository.Insert(newItem);
        await _mucTieuKTXHRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"Mục tiêu KTXH với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "MucTieuKTXH",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatMucTieuKTXH(long id, MucTieuKTXHDto model, long updatedBy)
    {
        var query = _mucTieuKTXHRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _mucTieuKTXHRepository.Update(item);
        await _mucTieuKTXHRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"Mục tiêu KTXH với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "MucTieuKTXH",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaMucTieuKTXH(long id, long deteledBy)
    {
        var item = await _mucTieuKTXHRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _mucTieuKTXHRepository.Delete(item);
            await _mucTieuKTXHRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"Mục tiêu KTXH với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "MucTieuKTXH",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region QuyChuanKT
    public async Task<(IEnumerable<QuyChuanKT>?, int)> GetQuyChuanKTs(QuyChuanKTFilter model)
    {
        var query = _quyChuanKTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<QuyChuanKT> GetQuyChuanKT(long id, bool isTracking = false)
    {
        var query = _quyChuanKTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy quy chuẩn kỹ thuật.");
        return item;
    }

    public async Task<QuyChuanKT> TaoQuyChuanKT(QuyChuanKTDto model, long createdBy)
    {
        var query = _quyChuanKTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("quy chuẩn KT đã tồn tại!");

        var newItem = _mapper.Map<QuyChuanKT>(model);
        _quyChuanKTRepository.Insert(newItem);
        await _quyChuanKTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"quy chuẩn KT với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "QuyChuanKT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatQuyChuanKT(long id, QuyChuanKTDto model, long updatedBy)
    {
        var query = _quyChuanKTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _quyChuanKTRepository.Update(item);
        await _quyChuanKTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"quy chuẩn KT với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "QuyChuanKT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaQuyChuanKT(long id, long deteledBy)
    {
        var item = await _quyChuanKTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _quyChuanKTRepository.Delete(item);
            await _quyChuanKTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"quy chuẩn KT với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "QuyChuanKT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region NguonCapKinhPhi

    public async Task<(IEnumerable<NguonCapKinhPhi>?, int)> GetNguonCapKinhPhis(NguonCapKinhPhiFilter model)
    {
        var query = _nguonCapKinhPhiRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<NguonCapKinhPhi> GetNguonCapKinhPhi(long id, bool isTracking = false)
    {
        var query = _nguonCapKinhPhiRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy nguồn cấp kinh phí hợp tác.");
        return item;
    }

    public async Task<NguonCapKinhPhi> TaoNguonCapKinhPhi(NguonCapKinhPhiDto model, long createdBy)
    {
        var query = _nguonCapKinhPhiRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("nguồn cấp kinh phí đã tồn tại!");

        var newItem = _mapper.Map<NguonCapKinhPhi>(model);
        _nguonCapKinhPhiRepository.Insert(newItem);
        await _nguonCapKinhPhiRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nguồn cấp kinh phí với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "NguonCapKinhPhi",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatNguonCapKinhPhi(long id, NguonCapKinhPhiDto model, long updatedBy)
    {
        var query = _nguonCapKinhPhiRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã nguồn cấp kinh phí hợp tác đã tồn tại!");

        _mapper.Map(model, item);
        _nguonCapKinhPhiRepository.Update(item);
        await _nguonCapKinhPhiRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nguồn cấp kinh phí với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "NguonCapKinhPhi",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaNguonCapKinhPhi(long id, long deteledBy)
    {
        var item = await _nguonCapKinhPhiRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _nguonCapKinhPhiRepository.Delete(item);
            await _nguonCapKinhPhiRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"nguồn cấp kinh phí với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "NguonCapKinhPhi",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }

    #endregion

    #region DoiTacQT

    public async Task<(IEnumerable<DoiTacQT>?, int)> GetDoiTacQTs(DoiTacQTFilter model)
    {
        var query = _doiTacQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<DoiTacQT> GetDoiTacQT(long id, bool isTracking = false)
    {
        var query = _doiTacQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy đối tác hợp tác quốc tế!");
        return item;
    }

    public async Task<DoiTacQT> TaoDoiTacQT(DoiTacQTDto model, long createdBy)
    {
        var query = _doiTacQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("đối tác quốc tế đã tồn tại!");

        var newItem = _mapper.Map<DoiTacQT>(model);
        _doiTacQTRepository.Insert(newItem);
        await _doiTacQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"đối tác quốc tế với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "DoiTacQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatDoiTacQT(long id, DoiTacQTDto model, long updatedBy)
    {
        var query = _doiTacQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã đối tác hợp tác quốc t đã tồn tại!");

        _mapper.Map(model, item);
        _doiTacQTRepository.Update(item);
        await _doiTacQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"đối tác quốc tế với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "DoiTacQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaDoiTacQT(long id, long deteledBy)
    {
        var item = await _doiTacQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _doiTacQTRepository.Delete(item);
            await _doiTacQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"đối tác quốc tế với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "DoiTacQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region HinhThucHTQT

    public async Task<(IEnumerable<HinhThucHTQT>?, int)> GetHinhThucHTQTs(HinhThucHTQTFilter model)
    {
        var query = _hinhThucHTQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<HinhThucHTQT> GetHinhThucHTQT(long id, bool isTracking = false)
    {
        var query = _hinhThucHTQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<HinhThucHTQT> TaoHinhThucHTQT(HinhThucHTQTDto model, long createdBy)
    {
        var query = _hinhThucHTQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("hình thức hợp tác quốc tế đã tồn tại!");

        var newItem = _mapper.Map<HinhThucHTQT>(model);
        _hinhThucHTQTRepository.Insert(newItem);
        await _hinhThucHTQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"hình thức hợp tác quốc tế phí với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "HinhThucHTQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatHinhThucHTQT(long id,HinhThucHTQTDto model, long updatedBy)
    {
        var query = _hinhThucHTQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _hinhThucHTQTRepository.Update(item);
        await _hinhThucHTQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"hình thức hợp tác quốc tế với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "HinhThucHTQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaHinhThucHTQT(long id, long deteledBy)
    {
        var item = await _hinhThucHTQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _hinhThucHTQTRepository.Delete(item);
            await _hinhThucHTQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"hình thức hợp tác quốc tế với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "HinhThucHTQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region NguonKPHTQT 

    public async Task<(IEnumerable<NguonKPHTQT>?, int)> GetNguonKPHTQTs(NguonKPHTQTFilter model)
    {
        var query = _nguonKPHTQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<NguonKPHTQT> GetNguonKPHTQT(long id, bool isTracking = false)
    {
        var query = _nguonKPHTQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<NguonKPHTQT> TaoNguonKPHTQT(NguonKPHTQTDto model, long createdBy)
    {
        var query = _nguonKPHTQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("nguồn kinh phí HTQT đã tồn tại!");

        var newItem = _mapper.Map<NguonKPHTQT>(model);
        _nguonKPHTQTRepository.Insert(newItem);
        await _nguonKPHTQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nguồn kinh phí HTQT với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "NguonKPHTQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatNguonKPHTQT(long id, NguonKPHTQTDto model, long updatedBy)
    {
        var query = _nguonKPHTQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _nguonKPHTQTRepository.Update(item);
        await _nguonKPHTQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nguồn kinh phí HTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "NguonKPHTQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaNguonKPHTQT(long id, long deteledBy)
    {
        var item = await _nguonKPHTQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _nguonKPHTQTRepository.Delete(item);
            await _nguonKPHTQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"nguồn kinh phí HTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "NguonKPHTQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LinhVucNCHTQT
    public async Task<(IEnumerable<LinhVucNCHTQT>?, int)> GetLinhVucNCHTQTs(LinhVucNCHTQTFilter model)
    {
        var query = _linhVucNCHTQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucNCHTQT> GetLinhVucNCHTQT(long id, bool isTracking = false)
    {
        var query = _linhVucNCHTQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LinhVucNCHTQT> TaoLinhVucNCHTQT(LinhVucNCHTQTDto model, long createdBy)
    {
        var query = _linhVucNCHTQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("lĩnh vực NCHTQT đã tồn tại!");

        var newItem = _mapper.Map<LinhVucNCHTQT>(model);
        _linhVucNCHTQTRepository.Insert(newItem);
        await _linhVucNCHTQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực NCHTQT với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucNCHTQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucNCHTQT(long id,LinhVucNCHTQTDto model, long updatedBy)
    {
        var query = _linhVucNCHTQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _linhVucNCHTQTRepository.Update(item);
        await _linhVucNCHTQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực NCHTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucNCHTQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucNCHTQT(long id, long deteledBy)
    {
        var item = await _linhVucNCHTQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _linhVucNCHTQTRepository.Delete(item);
            await _linhVucNCHTQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"lĩnh vực NCHTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucNCHTQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LinhVucUngDung
    public async Task<(IEnumerable<LinhVucUngDung>?, int)> GetLinhVucUngDungs(LinhVucUngDungFilter model)
    {
        var query = _linhVucUngDungRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucUngDung> GetLinhVucUngDung(long id, bool isTracking = false)
    {
        var query = _linhVucUngDungRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LinhVucUngDung> TaoLinhVucUngDung(LinhVucUngDungDto model, long createdBy)
    {
        var query = _linhVucUngDungRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("lĩnh vựcứng dụng đã tồn tại!");

        var newItem = _mapper.Map<LinhVucUngDung>(model);
        _linhVucUngDungRepository.Insert(newItem);
        await _linhVucUngDungRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực ứng dụng với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucUngDung",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucUngDung(long id, LinhVucUngDungDto model, long updatedBy)
    {
        var query = _linhVucUngDungRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _linhVucUngDungRepository.Update(item);
        await _linhVucUngDungRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực ứng dụng với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucUngDung",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucUngDung(long id, long deteledBy)
    {
        var item = await _linhVucUngDungRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _linhVucUngDungRepository.Delete(item);
            await _linhVucUngDungRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"lĩnh vực ứng dụng với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucUngDung",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region MauPhuongTienDo
    public async Task<(IEnumerable<MauPhuongTienDo>?, int)> GetMauPhuongTienDos(MauPhuongTienDoFilter model)
    {
        var query = _mauPhuongTienDoRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<MauPhuongTienDo> GetMauPhuongTienDo(long id, bool isTracking = false)
    {
        var query = _mauPhuongTienDoRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<MauPhuongTienDo> TaoMauPhuongTienDo(MauPhuongTienDoDto model, long createdBy)
    {
        var query = _mauPhuongTienDoRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("mẫu phương tiện đo đã tồn tại!");

        var newItem = _mapper.Map<MauPhuongTienDo>(model);
        _mauPhuongTienDoRepository.Insert(newItem);
        await _mauPhuongTienDoRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"mẫu phương tiện đo với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "MauPhuongTienDo",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatMauPhuongTienDo(long id,MauPhuongTienDoDto model, long updatedBy)
    {
        var query = _mauPhuongTienDoRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _mauPhuongTienDoRepository.Update(item);
        await _mauPhuongTienDoRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"mẫu phương tiện đo với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "MauPhuongTienDo",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaMauPhuongTienDo(long id, long deteledBy)
    {
        var item = await _mauPhuongTienDoRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _mauPhuongTienDoRepository.Delete(item);
            await _mauPhuongTienDoRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"mẫu phương tiện đo với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "MauPhuongTienDo",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion
    
    #region ChucDanh 
    public async Task<(IEnumerable<ChucDanh>?, int)> GetChucDanhs(ChucDanhFilter model)
    {
        var query = _chucDanhRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<ChucDanh> GetChucDanh(long id, bool isTracking = false)
    {
        var query = _chucDanhRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<ChucDanh> TaoChucDanh(ChucDanhDto model, long createdBy)
    {
        var query = _chucDanhRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("chức danh đã tồn tại!");

        var newItem = _mapper.Map<ChucDanh>(model);
        _chucDanhRepository.Insert(newItem);
        await _chucDanhRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"chức danh với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "ChucDanh",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatChucDanh(long id,ChucDanhDto model, long updatedBy)
    {
        var query = _chucDanhRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _chucDanhRepository.Update(item);
        await _chucDanhRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"chức danh với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "ChucDanh",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaChucDanh(long id, long deteledBy)
    {
        var item = await _chucDanhRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _chucDanhRepository.Delete(item);
            await _chucDanhRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"chức danh với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "ChucDanh",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region QuocTich 
    public async Task<(IEnumerable<QuocTich>?, int)> GetQuocTichs(QuocTichFilter model)
    {
        var query = _quocTichRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<QuocTich> GetQuocTich(long id, bool isTracking = false)
    {
        var query = _quocTichRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<QuocTich> TaoQuocTich(QuocTichDto model, long createdBy)
    {
        var query = _quocTichRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("quốc tịch đã tồn tại!");

        var newItem = _mapper.Map<QuocTich>(model);
        _quocTichRepository.Insert(newItem);
        await _quocTichRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"quốc tịch với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "QuocTich",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatQuocTich(long id, QuocTichDto model, long updatedBy)
    {
        var query = _quocTichRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _quocTichRepository.Update(item);
        await _quocTichRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"quốc tịch với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "QuocTich",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaQuocTich(long id, long deteledBy)
    {
        var item = await _quocTichRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _quocTichRepository.Delete(item);
            await _quocTichRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"quốc tịch với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "QuocTich",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region TrinhDoChuyenMon 
    public async Task<(IEnumerable<TrinhDoChuyenMon>?, int)> GetTrinhDoChuyenMons(TrinhDoChuyenMonFilter model)
    {
        var query = _trinhDoChuyenMonRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<TrinhDoChuyenMon> GetTrinhDoChuyenMon(long id, bool isTracking = false)
    {
        var query = _trinhDoChuyenMonRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<TrinhDoChuyenMon> TaoTrinhDoChuyenMon(TrinhDoChuyenMonDto model, long createdBy)
    {
        var query = _trinhDoChuyenMonRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("trình độ chuyên môn đã tồn tại!");

        var newItem = _mapper.Map<TrinhDoChuyenMon>(model);
        _trinhDoChuyenMonRepository.Insert(newItem);
        await _trinhDoChuyenMonRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"trình độ chuyên môn với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "TrinhDoChuyenMon",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatTrinhDoChuyenMon(long id, TrinhDoChuyenMonDto model, long updatedBy)
    {
        var query = _trinhDoChuyenMonRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _trinhDoChuyenMonRepository.Update(item);
        await _trinhDoChuyenMonRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"trình độ chuyên môn với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "TrinhDoChuyenMon",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaTrinhDoChuyenMon(long id, long deteledBy)
    {
        var item = await _trinhDoChuyenMonRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _trinhDoChuyenMonRepository.Delete(item);
            await _trinhDoChuyenMonRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"trình độ chuyên môn với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "TrinhDoChuyenMon",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LoaiHinhNhiemVu 
    public async Task<(IEnumerable<LoaiHinhNhiemVu>?, int)> GetLoaiHinhNhiemVus(LoaiHinhNhiemVuFilter model)
    {
        var query = _loaiHinhNhiemVuRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LoaiHinhNhiemVu> GetLoaiHinhNhiemVu(long id, bool isTracking = false)
    {
        var query = _loaiHinhNhiemVuRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LoaiHinhNhiemVu> TaoLoaiHinhNhiemVu(LoaiHinhNhiemVuDto model, long createdBy)
    {
        var query = _loaiHinhNhiemVuRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("loại hình nhiệm vụ đã tồn tại!");

        var newItem = _mapper.Map<LoaiHinhNhiemVu>(model);
        _loaiHinhNhiemVuRepository.Insert(newItem);
        await _loaiHinhNhiemVuRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"loại hình nhiệm vụ với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LoaiHinhNhiemVu",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLoaiHinhNhiemVu(long id, LoaiHinhNhiemVuDto model, long updatedBy)
    {
        var query = _loaiHinhNhiemVuRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _loaiHinhNhiemVuRepository.Update(item);
        await _loaiHinhNhiemVuRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"loại hình nhiệm vụ với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LoaiHinhNhiemVu",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLoaiHinhNhiemVu(long id, long deteledBy)
    {
        var item = await _loaiHinhNhiemVuRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _loaiHinhNhiemVuRepository.Delete(item);
            await _loaiHinhNhiemVuRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"loại hình nhiệm vụ với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LoaiHinhNhiemVu",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region CapQuanLy
    public async Task<(IEnumerable<CapQuanLy>?, int)> GetCapQuanLys(CapQuanLyFilter model)
    {
        var query = _capQuanLyRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<CapQuanLy> GetCapQuanLy(long id, bool isTracking = false)
    {
        var query = _capQuanLyRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<CapQuanLy> TaoCapQuanLy(CapQuanLyDto model, long createdBy)
    {
        var query = _capQuanLyRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("cấp quản lý đã tồn tại!");

        var newItem = _mapper.Map<CapQuanLy>(model);
        _capQuanLyRepository.Insert(newItem);
        await _capQuanLyRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"cấp quản lý với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "CapQuanLy",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatCapQuanLy(long id, CapQuanLyDto model, long updatedBy)
    {
        var query = _capQuanLyRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _capQuanLyRepository.Update(item);
        await _capQuanLyRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"cấp quản lý với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "CapQuanLy",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaCapQuanLy(long id, long deteledBy)
    {
        var item = await _capQuanLyRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _capQuanLyRepository.Delete(item);
            await _capQuanLyRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"cấp quản lý với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "CapQuanLy",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LinhVucNghienCuu
    public async Task<(IEnumerable<LinhVucNghienCuu>?, int)> GetLinhVucNghienCuus(LinhVucNghienCuuFilter model)
    {
        var query = _linhVucNghienCuuRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucNghienCuu> GetLinhVucNghienCuu(long id, bool isTracking = false)
    {
        var query = _linhVucNghienCuuRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LinhVucNghienCuu> TaoLinhVucNghienCuu(LinhVucNghienCuuDto model, long createdBy)
    {
        var query = _linhVucNghienCuuRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("lĩnh vực nghiên cứu đã tồn tại!");

        var newItem = _mapper.Map<LinhVucNghienCuu>(model);
        _linhVucNghienCuuRepository.Insert(newItem);
        await _linhVucNghienCuuRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực nghiên cứu với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucNghienCuu",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucNghienCuu(long id,LinhVucNghienCuuDto model, long updatedBy)
    {
        var query = _linhVucNghienCuuRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _linhVucNghienCuuRepository.Update(item);
        await _linhVucNghienCuuRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực nghiên cứu với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucNghienCuu",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucNghienCuu(long id, long deteledBy)
    {
        var item = await _linhVucNghienCuuRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _linhVucNghienCuuRepository.Delete(item);
            await _linhVucNghienCuuRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"lĩnh vực nghiên cứu với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucNghienCuu",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region CapQuanLyHTQT
    public async Task<(IEnumerable<CapQuanLyHTQT>?, int)> GetCapQuanLyHTQTs(CapQuanLyHTQTFilter model)
    {
        var query = _capQuanLyHTQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<CapQuanLyHTQT> GetCapQuanLyHTQT(long id, bool isTracking = false)
    {
        var query = _capQuanLyHTQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<CapQuanLyHTQT> TaoCapQuanLyHTQT(CapQuanLyHTQTDto model, long createdBy)
    {
        var query = _capQuanLyHTQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("cấp quản lý HTQT đã tồn tại!");

        var newItem = _mapper.Map<CapQuanLyHTQT>(model);
        _capQuanLyHTQTRepository.Insert(newItem);
        await _capQuanLyHTQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"cấp quản lý HTQT với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "CapQuanLyHTQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatCapQuanLyHTQT(long id, CapQuanLyHTQTDto model, long updatedBy)
    {
        var query = _capQuanLyHTQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _capQuanLyHTQTRepository.Update(item);
        await _capQuanLyHTQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"cấp quản lý HTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "CapQuanLyHTQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaCapQuanLyHTQT(long id, long deteledBy)
    {
        var item = await _capQuanLyHTQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _capQuanLyHTQTRepository.Delete(item);
            await _capQuanLyHTQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"cấp quản lý HTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "CapQuanLyHTQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LinhVucNghienCuuTTQT
    public async Task<(IEnumerable<LinhVucNghienCuuTTQT>?, int)> GetLinhVucNghienCuuTTQTs(LinhVucNghienCuuTTQTFilter model)
    {
        var query = _linhVucNghienCuuTTQTRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LinhVucNghienCuuTTQT> GetLinhVucNghienCuuTTQT(long id, bool isTracking = false)
    {
        var query = _linhVucNghienCuuTTQTRepository.Select();

        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LinhVucNghienCuuTTQT> TaoLinhVucNghienCuuTTQT(LinhVucNghienCuuTTQTDto model, long createdBy)
    {
        var query = _linhVucNghienCuuTTQTRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("lĩnh vực nghiên cứu TTQT đã tồn tại!");

        var newItem = _mapper.Map<LinhVucNghienCuuTTQT>(model);
        _linhVucNghienCuuTTQTRepository.Insert(newItem);
        await _linhVucNghienCuuTTQTRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực nghiên cứu TTQT với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LinhVucNghienCuuTTQT",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLinhVucNghienCuuTTQT(long id,LinhVucNghienCuuTTQTDto model, long updatedBy)
    {
        var query = _linhVucNghienCuuTTQTRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _linhVucNghienCuuTTQTRepository.Update(item);
        await _linhVucNghienCuuTTQTRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"lĩnh vực nghiên cứu TTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LinhVucNghienCuuTTQT",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLinhVucNghienCuuTTQT(long id, long deteledBy)
    {
        var item = await _linhVucNghienCuuTTQTRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _linhVucNghienCuuTTQTRepository.Delete(item);
            await _linhVucNghienCuuTTQTRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"lĩnh vực nghiên cứu TTQT với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LinhVucNghienCuuTTQT",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region NguonCapKPEnum
    public async Task<(IEnumerable<NguonCapKPEnum>?, int)> GetNguonCapKPEnums(NguonCapKPEnumFilter model)
    {
        var query = _nguonCapKPEnumRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<NguonCapKPEnum> GetNguonCapKPEnum(long id, bool isTracking = false)
    {
        var query = _nguonCapKPEnumRepository.Select();
        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<NguonCapKPEnum> TaoNguonCapKPEnum(NguonCapKPEnumDto model, long createdBy)
    {
        var query = _nguonCapKPEnumRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("nguồn cấp KPEnum đã tồn tại!");

        var newItem = _mapper.Map<NguonCapKPEnum>(model);
        _nguonCapKPEnumRepository.Insert(newItem);
        await _nguonCapKPEnumRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"nguồn cấp KPEnum với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "NguonCapKPEnum",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatNguonCapKPEnum(long id,NguonCapKPEnumDto model, long updatedBy)
    {
        var query = _nguonCapKPEnumRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _nguonCapKPEnumRepository.Update(item);
        await _nguonCapKPEnumRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"nguồn cấp KPEnum với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "NguonCapKPEnum",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaNguonCapKPEnum(long id, long deteledBy)
    {
        var item = await _nguonCapKPEnumRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _nguonCapKPEnumRepository.Delete(item);
            await _nguonCapKPEnumRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"nguồn cấp KPEnum với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "NguonCapKPEnum",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion

    #region LoaiHinhToChuc
    public async Task<(IEnumerable<LoaiHinhToChuc>?, int)> GetLoaiHinhToChucs(LoaiHinhToChucFilter model)
    {
        var query = _loaiHinhToChucRepository.Select();

        query = model.TrangThai.HasValue ? query.Where(p => p.TrangThai == model.TrangThai) : query;
        query = !string.IsNullOrEmpty(model.Ma)
            ? query.Where(p => p.Ma.ToLower().Contains(model.Ma.ToLower())) : query;


        // Search by keyword
        query = !string.IsNullOrEmpty(model.Keyword)
            ? query.Where(p =>
                p.Ten.ToLower().Contains(model.Keyword.ToLower()) ||
                p.Ma != null && p.Ma.ToLower().Contains(model.Keyword.ToLower()))
            : query;

        var validated = new PaginationDto(model.PageNumber, model.PageSize);
        var items = await query
            .OrderByDescending(p => p.Ten)
            .Skip((validated.PageNumber - 1) * validated.PageSize)
            .Take(validated.PageSize)
            .ToListAsync();
        var records = await query.CountAsync();
        return (items, records);
    }

    public async Task<LoaiHinhToChuc> GetLoaiHinhToChuc(long id, bool isTracking = false)
    {
        var query = _loaiHinhToChucRepository.Select();
        if (!isTracking) query = query.AsNoTracking();
        var item = await query.SingleOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Không tìm thấy tổ chức!");
        return item;
    }

    public async Task<LoaiHinhToChuc> TaoLoaiHinhToChuc(LoaiHinhToChucDto model, long createdBy)
    {
        var query = _loaiHinhToChucRepository.Select();

        var item = await query
            .FirstOrDefaultAsync(p =>
                p.Ma.ToLower() == model.Ma.ToLower());
        if (item != null) throw new ArgumentException("loại hình tổ chức đã tồn tại!");

        var newItem = _mapper.Map<LoaiHinhToChuc>(model);
        _loaiHinhToChucRepository.Insert(newItem);
        await _loaiHinhToChucRepository.SaveChangesAsync();

        var log = new ActivityLogDto
        {
            Contents = $"loại hình tổ chức với mã #{newItem.Ma} và tên {newItem.Ten} thành công.",
            Params = newItem.Ma,
            Target = "LoaiHinhToChuc",
            TargetCode = newItem.Ma,
            UserId = createdBy
        };
        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Create);

        return newItem;
    }

    public async Task CapNhatLoaiHinhToChuc(long id, LoaiHinhToChucDto model, long updatedBy)
    {
        var query = _loaiHinhToChucRepository.Select();

        var item = await query.FirstOrDefaultAsync(p => p.Id == id);
        if (item == null) throw new ArgumentException("Mã tổ chức đã tồn tại!");

        _mapper.Map(model, item);
        _loaiHinhToChucRepository.Update(item);
        await _loaiHinhToChucRepository.SaveChangesAsync();
        var log = new ActivityLogDto
        {
            Contents = $"loại hình tổ chức với mã #{item.Ma} và tên {item.Ten} thành công.",
            Params = item.Ma,
            Target = "LoaiHinhToChuc",
            TargetCode = item.Ma,
            UserId = updatedBy
        };
        await _activityLogRepository.SaveLogAsync(log, updatedBy, LogMode.Update);
    }

    public async Task XoaLoaiHinhToChuc(long id, long deteledBy)
    {
        var item = await _loaiHinhToChucRepository.Select().FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            _loaiHinhToChucRepository.Delete(item);
            await _loaiHinhToChucRepository.SaveChangesAsync();
            var log = new ActivityLogDto
            {
                Contents = $"loại hình tổ chức với mã #{item.Ma} và tên {item.Ten} thành công.",
                Params = item.Ma,
                Target = "LoaiHinhToChuc",
                TargetCode = item.Ma,
                UserId = deteledBy
            };
            await _activityLogRepository.SaveLogAsync(log, deteledBy, LogMode.Delete);
        }
    }
    #endregion



    


}
