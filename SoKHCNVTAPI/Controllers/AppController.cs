using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Services;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Configurations;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DocumentFormat.OpenXml.Office.CustomUI;
using DocumentFormat.OpenXml.Spreadsheet;
using SoKHCNVTAPI.Repositories.CommonCategories;



namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Route("api/")]
//[ApiVersion("1")]
public class AppController : BaseController
{
    private readonly IRepository<NhanSuToChuc> _organizationStaffRepository;
    private readonly INhiemVuRepository _iMissionRepository;
    private readonly ICanBoRepository _canBoRepository;
    private readonly IDoanhNghiepRepository _doanhNghiepRepository;
    private readonly IChuyenGiaRepository _chuyenGiaRepository;
    private readonly ISoHuuTriTueRepository _soHuuTriTueRepository;
    private readonly IThongTinRepository _thongTinRepository;
    private readonly ICongBoRepository _congBoRepository;
    private readonly ITokenService _tokenService;
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IToChucRepository _iOrganizationRepository;

    public AppController(IUserRepository userRepository, ITokenService tokenService, DataContext context, INhiemVuRepository iMissionRepository, IRepository<NhanSuToChuc> organizationStaffRepository,
         IToChucRepository iOrganizationRepository, IThongTinRepository thongTinRepository, ICongBoRepository congBoRepository, ISoHuuTriTueRepository soHuuTriTueRepository, IDoanhNghiepRepository doanhNghiepRepository, IChuyenGiaRepository chuyenGiaRepository, ICanBoRepository canBoRepository, IRepository<Permission> permissionRepository, IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _context = context;
        _iMissionRepository = iMissionRepository;
        _iOrganizationRepository = iOrganizationRepository;
        _organizationStaffRepository = organizationStaffRepository;
        _canBoRepository = canBoRepository;
        _chuyenGiaRepository = chuyenGiaRepository;
        _doanhNghiepRepository = doanhNghiepRepository;
        _soHuuTriTueRepository = soHuuTriTueRepository;
        _congBoRepository = congBoRepository;
        _thongTinRepository = thongTinRepository;

    }

    [HttpGet]
    [ApiKey]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpPost("token")]
    //[AllowAnonymous]
    [ApiKey]
    public async Task<IActionResult> GetTokenAsync([FromBody] SignInDto model)
    {
        var isEmail = Utils.IsValidEmail(model.Username);
        var isPhoneNumber = Utils.IsValidPhoneNumber(model.Username);

        if (isEmail == false && isPhoneNumber == false)
        {
            //throw new ArgumentException("Email hoặc số điện thoại không hợp lệ.");
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Thông tin đăng nhập không chính xác.",
                Success = false,
                ErrorCode = 1
            });
        }

        var searchParam = isEmail ? model.Username : Utils.FormatPhoneNumber(model.Username);

        var user = await _context.Set<User>().SingleOrDefaultAsync(p => p.Email == searchParam || p.Phone == searchParam);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Thông tin tài khoản không tìm thấy.",
                Success = false,
                ErrorCode = 404
            });
        }

        if (user.Password != null)
        {
            // check password
            var isPass = PasswordBuilder.VerifyBCrypt(model.Password, user.Password);
            if (!isPass)
            {
                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Thông tin đăng nhập không chính xác.",
                    Success = false,
                    ErrorCode = 1
                });
            }

            if (user.Status == (short)UserStatus.DeActivate)
            {
                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Tài khoản chưa kích hoạt",
                    Success = false,
                    ErrorCode = 2
                });
            }
            else if (user.Status == (short)UserStatus.Locked)
            {
                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Tài khoản đã bị khóa",
                    Success = false,
                    ErrorCode = 3
                });
            }

            // jwt
            var token = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user);

            // Update User
            user.LastLogin = Utils.getCurrentDate();
            user.Token = token;
            user.RefreshToken = refreshToken;
            await _context.SaveChangesAsync();

            // Update Token
            //TODO: 

            UserResponseAPP userResponseAPP = new UserResponseAPP
            {
                Ma = user.Code,
                HoTen = user.Fullname,
                DienThoai = user.Phone,
                Email = user.Email,
                DiaChi = user.Address,
                Tinh = user.Province,
                Phuong = user.Ward,
                Quan = user.District,
                ViTri = user.Position,
                NgayCapNhat = user.UpdatedAt,
                NgayTao = user.CreatedAt
            };

            return StatusCode(StatusCodes.Status200OK, new SignInAPPResponse
            {
                Message = "Đăng nhập thành công!",
                Success = true,
                ErrorCode = 0,
                Token = token,
                RefreshToken = refreshToken,
                User = userResponseAPP
            });
        }
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Thông tin tài khoản không chính xác.",
            Success = false,
            ErrorCode = 1
        });
    }
    [HttpGet("nhiemvu")]
    public async Task<IActionResult>GetDsNhiemVu([FromQuery] CommonFilterDto model)
    {
        var (items, records) = await _iMissionRepository.SearchAsync(model);

        var apiNhiemVus = new List<NhiemVuAPI>();

        foreach (var item in items)
        {
            if (item != null)
            {
                var nhiemVuAPI = new NhiemVuAPI();
                nhiemVuAPI.Id = item.Id;
                nhiemVuAPI.tennhiemvu = item.Name;
                nhiemVuAPI.madinhdanhnhiemvuId = item.MissionIdentifyId;
                nhiemVuAPI.madinhdanhnhiemvu = item.MissionIdentify;
                nhiemVuAPI.capnhiemvuId = item.MissionLevelId;
                nhiemVuAPI.capnhiemvu = item.MissionLevel;
                nhiemVuAPI.tochucchutriId = item.OrganizationId;
                nhiemVuAPI.tochucchutri = item.Organization;
                nhiemVuAPI.linhvucId = item.LinhVucNghienCuuId;
                nhiemVuAPI.linhvuc = item.LinhVucNghienCuu;
                nhiemVuAPI.loaihinhnhiemvu = item.LoaiHinhNhiemVu;
                nhiemVuAPI.thoigianthuchien = item.TotalTimeWithMonth;
                nhiemVuAPI.thoigianbatdau = item.StartTime;
                nhiemVuAPI.thoigianketthuc = item.EndTime;
                nhiemVuAPI.tongkinhphi = item.TotalExpenses;
                nhiemVuAPI.trangthai = item.Status;
                apiNhiemVus.Add(nhiemVuAPI);
            }
        }


        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách nhiệm vụ thành công!",
            ErrorCode = 0,
            Success = true,
            Meta = new Meta(model, records),
            Data = apiNhiemVus
        });
    }

    [HttpGet("nhiemvu/{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _iMissionRepository.GetByIdAsync(id);

           var apiNhiemVus = new List<NhiemVuAPI>();     
        if (item != null)
        {
            var nhiemVuAPI = new NhiemVuAPI();
            nhiemVuAPI.Id = item.Id;
            nhiemVuAPI.tennhiemvu = item.Name;
            nhiemVuAPI.madinhdanhnhiemvuId = item.MissionIdentifyId;
            nhiemVuAPI.madinhdanhnhiemvu = item.MissionIdentify;
            nhiemVuAPI.capnhiemvuId = item.MissionLevelId;
            nhiemVuAPI.capnhiemvu = item.MissionLevel;
            nhiemVuAPI.tochucchutriId = item.OrganizationId;
            nhiemVuAPI.tochucchutri = item.Organization;
            nhiemVuAPI.linhvucId = item.LinhVucNghienCuuId;
            nhiemVuAPI.linhvuc = item.LinhVucNghienCuu;
            nhiemVuAPI.loaihinhnhiemvu = item.LoaiHinhNhiemVu;
            nhiemVuAPI.thoigianthuchien = item.TotalTimeWithMonth;
            nhiemVuAPI.thoigianbatdau = item.StartTime;
            nhiemVuAPI.thoigianketthuc = item.EndTime;
            nhiemVuAPI.tongkinhphi = item.TotalExpenses;
            nhiemVuAPI.trangthai = item.Status;
            apiNhiemVus.Add(nhiemVuAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = apiNhiemVus
        });
    }
    //-----------

    [HttpGet("tochuc")]
    public async Task<IActionResult> GetTochuckhcn([FromQuery] ToChucFilter model)
    {
        var (items, records) = await _iOrganizationRepository.FilterAsync(model);
        var apiToChucs = new List<ToChucAPI>();
        foreach (var item in items)
        {
            var toChucAPI = new ToChucAPI();
            toChucAPI.Id = item.Id;
            toChucAPI.tentochuc = item.TenToChuc;       
            toChucAPI.madinhdanhtochuc = item.OrganizationIdentifierId;     
            toChucAPI.diachi = item.DiaChi;       
            toChucAPI.tinhthanh = item.TinhThanh;        
            toChucAPI.dienthoai = item.DienThoai;
            toChucAPI.website = item.Website;
            toChucAPI.nguoidungdau = item.NguoiDungDau;
            toChucAPI.loaihinhtochuc = item.LoaiHinhToChuc;
            toChucAPI.hinhthucsohuu = item.HinhThucSoHuu;
            toChucAPI.linhvucnc = item.LinhVucNC;
            apiToChucs.Add(toChucAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách tổ chức thành công!",
            Meta = new Meta(model, records),
            Data = apiToChucs
        });
    }

    [HttpGet("tochuc/{id:long}")]
    public async Task<IActionResult> GetCTTochuckhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _iOrganizationRepository.GetByIdAsync(id);
               
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiToChucs = new List<ToChucAPI>();
        if (item != null)
        {
            var toChucAPI = new ToChucAPI();
            toChucAPI.Id = item.Id;
            toChucAPI.tentochuc = item.TenToChuc;
            toChucAPI.madinhdanhtochuc = item.OrganizationIdentifierId;
            toChucAPI.diachi = item.DiaChi;
            toChucAPI.tinhthanh = item.TinhThanh;
            toChucAPI.dienthoai = item.DienThoai;
            toChucAPI.website = item.Website;
            toChucAPI.nguoidungdau = item.NguoiDungDau;
            toChucAPI.loaihinhtochuc = item.LoaiHinhToChuc;
            toChucAPI.hinhthucsohuu = item.HinhThucSoHuu;
            toChucAPI.linhvucnc = item.LinhVucNC;
            apiToChucs.Add(toChucAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất tổ chức thành công!",
            Data = apiToChucs
        });
    }
    //--------------
    [HttpGet("canbo")]
    public async Task<IActionResult> GetCanBokhcn([FromQuery] CanBoFilter model)
    {
        var (items, records) = await _canBoRepository.FilterAsync(model);
        var apiCanBos = new List<CanBoAPI>();
        foreach (var item in items)
        {
            var canBoAPI = new CanBoAPI();
            canBoAPI.Id = item.Id;
            canBoAPI.hovaten = item.HoVaTen;
            canBoAPI.madinhdanhcanbo = item.Ma;
            canBoAPI.ngaysinh=item.NgaySinh ;
            canBoAPI.gioitinh = item.GioiTinh;
            canBoAPI.quoctich = item.QuocTich;
            canBoAPI.cccd = item.CCCD;
            canBoAPI.noiohiennay = item.NoiOHienNay;
            canBoAPI.tinhthanh = item.TinhThanh;
            canBoAPI.dienthoai = item.DienThoai;
            canBoAPI.email = item.Email;
            canBoAPI.chucdanhnghenghiep = item.ChucDanhNgheNghiep;
            canBoAPI.chucdanh = item.ChucDanh;
            canBoAPI.namphongchucdanh = item.NamPhongChucDanh;
            canBoAPI.hocvi = item.HocVi;
            canBoAPI.namdathocvi = item.NamDatHocVi;
            canBoAPI.coquancongtac = item.CoQuanCongTac;
            canBoAPI.linhvucnc = item.LinhVucNC;
          
            apiCanBos.Add(canBoAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách cán bộ tổ chức thành công!",
            Meta = new Meta(model, records),
            Data = apiCanBos
        });
    }

    [HttpGet("canbo/{id:long}")]
    public async Task<IActionResult> GetCTCanBokhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _canBoRepository.GetByIdAsync(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiCanBos = new List<CanBoAPI>();
        if (item != null)
        {
            var canBoAPI = new CanBoAPI();
            canBoAPI.Id = item.Id;
            canBoAPI.hovaten = item.HoVaTen;
            canBoAPI.madinhdanhcanbo = item.Ma;
            canBoAPI.ngaysinh = item.NgaySinh;
            canBoAPI.gioitinh = item.GioiTinh;
            canBoAPI.quoctich = item.QuocTich;
            canBoAPI.cccd = item.CCCD;
            canBoAPI.noiohiennay = item.NoiOHienNay;
            canBoAPI.tinhthanh = item.TinhThanh;
            canBoAPI.dienthoai = item.DienThoai;
            canBoAPI.email = item.Email;
            canBoAPI.chucdanhnghenghiep = item.ChucDanhNgheNghiep;
            canBoAPI.chucdanh = item.ChucDanh;
            canBoAPI.namphongchucdanh = item.NamPhongChucDanh;
            canBoAPI.hocvi = item.HocVi;
            canBoAPI.namdathocvi = item.NamDatHocVi;
            canBoAPI.coquancongtac = item.CoQuanCongTac;
            canBoAPI.linhvucnc = item.LinhVucNC;
            canBoAPI.LinhVucNghienCuus = item.LinhVucNghienCuus;
            apiCanBos.Add(canBoAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất cán bộ thành công!",
            Data = apiCanBos
        });
    }

    //--------------------
    [HttpGet("chuyengia")]
    public async Task<IActionResult> GetChuyenGiakhcn([FromQuery] ExpertFilter model)
    {
        var (items, records) = await _chuyenGiaRepository.GetExperts(model);
        var apiChuyenGias = new List<ChuyenGiaAPI>();
        foreach (var item in items)
        {
            var ChuyenGiaAPI = new ChuyenGiaAPI();
            ChuyenGiaAPI.Id = item.Id;
            ChuyenGiaAPI.hovaten = item.HovaTen;
            ChuyenGiaAPI.ngaysinh = item.NgaySinh;
            ChuyenGiaAPI.quoctich = item.QuocTich;
            ChuyenGiaAPI.hocvi = item.HocVi;
            ChuyenGiaAPI.bangcap = item.BangCap;
            ChuyenGiaAPI.linhvucchuyenmon = item.LVChuyenMon;
            ChuyenGiaAPI.thongtinlienhe = item.ThongTinLienHe;
            ChuyenGiaAPI.sodienthoai = item.SDT;
            ChuyenGiaAPI.email = item.Email;
            ChuyenGiaAPI.diachi = item.DiaChi;


            apiChuyenGias.Add(ChuyenGiaAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách chuyên gia thành công!",
            Meta = new Meta(model, records),
            Data = apiChuyenGias
        });
    }

    [HttpGet("chuyengia/{id:long}")]
    public async Task<IActionResult> GetCTChuyenGiakhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _chuyenGiaRepository.GetExpert(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
            var apiChuyenGias = new List<ChuyenGiaAPI>();
                if (item != null)
                {
                    var ChuyenGiaAPI = new ChuyenGiaAPI();
                    ChuyenGiaAPI.Id = item.Id;
                    ChuyenGiaAPI.hovaten = item.HovaTen;
                    ChuyenGiaAPI.ngaysinh = item.NgaySinh;
                    ChuyenGiaAPI.quoctich = item.QuocTich;
                    ChuyenGiaAPI.hocvi = item.HocVi;
                    ChuyenGiaAPI.bangcap = item.BangCap;
                    ChuyenGiaAPI.linhvucchuyenmon = item.LVChuyenMon;
                    ChuyenGiaAPI.thongtinlienhe = item.ThongTinLienHe;
                    ChuyenGiaAPI.sodienthoai = item.SDT;
                    ChuyenGiaAPI.email = item.Email;
                    ChuyenGiaAPI.diachi = item.DiaChi;
                    apiChuyenGias.Add(ChuyenGiaAPI);
                }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất chuyên gia thành công!",
            Data = apiChuyenGias
        });
    }
    //--------------------
    [HttpGet("doanhnghiep")]
    public async Task<IActionResult> GetDoanhNghiepkhcn([FromQuery] DoanhNghiepFilter model)
    {
        var (items, records) = await _doanhNghiepRepository.FilterAsync(model);
        var apiDoanhNghieps = new List<DoanhNghiepAPI>();
        foreach (var item in items)
        {
            var doanhNghiepAPI = new DoanhNghiepAPI();
            doanhNghiepAPI.Id = item.Id;
            doanhNghiepAPI.tendoanhnghiep = item.Ten;
            doanhNghiepAPI.tenviettat = item.TenVietTat;
            doanhNghiepAPI.ngaythanhlap = item.NgayThanhLap;
            doanhNghiepAPI.masothue = item.MaSoThue;
            doanhNghiepAPI.dienthoai = item.DienThoai;
            doanhNghiepAPI.email = item.Email;
            doanhNghiepAPI.website = item.Website;
            doanhNghiepAPI.diachitrusochinh = item.DiaChi;
            doanhNghiepAPI.loaihinhtochuc = item.LoaiHinhToChuc;
            doanhNghiepAPI.linvucnc = item.LinhVucNghienCuu;
            doanhNghiepAPI.tinhthanh = item.TinhThanh;
            doanhNghiepAPI.loaihinh = item.LoaiHinh;


            apiDoanhNghieps.Add(doanhNghiepAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách doanh nghiệp thành công!",
            Meta = new Meta(model, records),
            Data = apiDoanhNghieps
        });
    }

    [HttpGet("doanhnghiep/{id:long}")]
    public async Task<IActionResult> GetCTDoanhNghiepkhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _doanhNghiepRepository.GetByIdAsync(id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiDoanhNghieps = new List<DoanhNghiepAPI>();
        if (item != null)
        {
            var doanhNghiepAPI = new DoanhNghiepAPI();
            doanhNghiepAPI.Id = item.Id;
            doanhNghiepAPI.tendoanhnghiep = item.Ten;
            doanhNghiepAPI.tenviettat = item.TenVietTat;
            doanhNghiepAPI.ngaythanhlap = item.NgayThanhLap;
            doanhNghiepAPI.masothue = item.MaSoThue;
            doanhNghiepAPI.dienthoai = item.DienThoai;
            doanhNghiepAPI.email = item.Email;
            doanhNghiepAPI.website = item.Website;
            doanhNghiepAPI.diachitrusochinh = item.DiaChi;
            doanhNghiepAPI.loaihinhtochuc = item.LoaiHinhToChuc;
            doanhNghiepAPI.linvucnc = item.LinhVucNghienCuu;
            doanhNghiepAPI.tinhthanh = item.TinhThanh;
            doanhNghiepAPI.loaihinh = item.LoaiHinh;

            apiDoanhNghieps.Add(doanhNghiepAPI);
        }
       
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất doanh nghiệp thành công!",
            Data = apiDoanhNghieps
        });
    }
    //--------------

    [HttpGet("sohuutritue")]
    public async Task<IActionResult> GetSoHuuTriTuekhcn([FromQuery] SoHuuTriTueFilter model)
    {
        var (items, records) = await _soHuuTriTueRepository.FilterAsync(model);
        var apiSoHuuTriTues = new List<SoHuuTriTueAPI>();
        foreach (var item in items)
        {
            var soHuuTriTueAPI = new SoHuuTriTueAPI();
            soHuuTriTueAPI.Id = item.Id;
            soHuuTriTueAPI.tensangche = item.TenSangChe;
            soHuuTriTueAPI.loaisohuu = item.LoaiSoHuu;
            soHuuTriTueAPI.phanloai = item.PhanLoai;
            soHuuTriTueAPI.sobang = item.SoBang;
            soHuuTriTueAPI.chubang = item.ChuBang;
            soHuuTriTueAPI.thongtinSoHuu = item.ThongTinSoHuu;
            soHuuTriTueAPI.sangchetoanvan = item.SangCheToanVan;
            soHuuTriTueAPI.tochucdaidien = item.ToChucDaiDien;
            soHuuTriTueAPI.nguoidaidien = item.NguoiDaiDien;
            soHuuTriTueAPI.tochucdudieukien = item.ToChucDuDieuKien;
            soHuuTriTueAPI.canhandudieukien = item.CaNhanDuDieuKien;
            apiSoHuuTriTues.Add(soHuuTriTueAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách sở hữu trí tuệ thành công!",
            Meta = new Meta(model, records),
            Data = apiSoHuuTriTues
        });
    }
    [HttpGet("sohuutritue/{id:long}")]
    public async Task<IActionResult> GetCTSoHuuTriTuekhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _soHuuTriTueRepository.GetByIdAsync(id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiSoHuuTriTues = new List<SoHuuTriTueAPI>();

        if (item != null)
        {
            var soHuuTriTueAPI = new SoHuuTriTueAPI();
            soHuuTriTueAPI.Id = item.Id;
            soHuuTriTueAPI.tensangche = item.TenSangChe;
            soHuuTriTueAPI.loaisohuu = item.LoaiSoHuu;
            soHuuTriTueAPI.phanloai = item.PhanLoai;
            soHuuTriTueAPI.sobang = item.SoBang;
            soHuuTriTueAPI.chubang = item.ChuBang;
            soHuuTriTueAPI.thongtinSoHuu = item.ThongTinSoHuu;
            soHuuTriTueAPI.sangchetoanvan = item.SangCheToanVan;
            soHuuTriTueAPI.tochucdaidien = item.ToChucDaiDien;
            soHuuTriTueAPI.nguoidaidien = item.NguoiDaiDien;
            soHuuTriTueAPI.tochucdudieukien = item.ToChucDuDieuKien;
            soHuuTriTueAPI.canhandudieukien = item.CaNhanDuDieuKien;
            apiSoHuuTriTues.Add(soHuuTriTueAPI);
        }

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất sở hữu trí tuệ thành công!",
            Data = apiSoHuuTriTues
        });
    }

    //--------------
    [HttpGet("congbo")]
    public async Task<IActionResult> GetCongBokhcn([FromQuery] CongBoFilter model)
    {
        var (items, records) = await _congBoRepository.FilterAsync(model);
        var apiCongBos = new List<CongBoKHAPI>();
        foreach (var item in items)
        {
            var congBoAPI = new CongBoKHAPI();
            congBoAPI.Id = item.Id;
            congBoAPI.chisodemuc = item.ChiSoDeMuc;
            congBoAPI.linhvucnghiencuu = item.LinhVucNghienCuu;
            congBoAPI.dangtailieu = item.DangTaiLieu;
            congBoAPI.tacgia = item.TacGia;
            congBoAPI.nhande = item.NhanDe;
            congBoAPI.nguontrich = item.NguonTrich;
            congBoAPI.namxuatban = item.NamXuatBan;
            congBoAPI.so = item.So;
            congBoAPI.trang = item.Trang;
            congBoAPI.issn = item.ISSN;
            congBoAPI.tukhoa = item.TuKhoa;
            congBoAPI.tomtat = item.TomTat;
            congBoAPI.kyhieukho = item.KyHieuKho;
            congBoAPI.xemtoanvan = item.XemToanVan;
            apiCongBos.Add(congBoAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách công bố thành công!",
            Meta = new Meta(model, records),
            Data = apiCongBos
        });
    }

    [HttpGet("congbo/{id:long}")]
    public async Task<IActionResult> GetCTCongBokhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _congBoRepository.GetByIdAsync(id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiCongBos = new List<CongBoKHAPI>();

        if (item != null)
        {
            var congBoAPI = new CongBoKHAPI();
            congBoAPI.Id = item.Id;
            congBoAPI.chisodemuc = item.ChiSoDeMuc;
            congBoAPI.linhvucnghiencuu = item.LinhVucNghienCuu;
            congBoAPI.dangtailieu = item.DangTaiLieu;
            congBoAPI.tacgia = item.TacGia;
            congBoAPI.nhande = item.NhanDe;
            congBoAPI.nguontrich = item.NguonTrich;
            congBoAPI.namxuatban = item.NamXuatBan;
            congBoAPI.so = item.So;
            congBoAPI.trang = item.Trang;
            congBoAPI.issn = item.ISSN;
            congBoAPI.tukhoa = item.TuKhoa;
            congBoAPI.tomtat = item.TomTat;
            congBoAPI.kyhieukho = item.KyHieuKho;
            congBoAPI.xemtoanvan = item.XemToanVan;
            apiCongBos.Add(congBoAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất công bố thành công!",
            Data = apiCongBos
        });
    }

    //--------------
    [HttpGet("thongtin")]
    public async Task<IActionResult> GetThongTinkhcn([FromQuery] ThongTinFilter model)
    {
        var (items, records) = await _thongTinRepository.FilterAsync(model);
        var apiThongTins = new List<ThongTinAPI>();

        foreach (var item in items)
        {
            var thongTinAPI = new ThongTinAPI();
            thongTinAPI.Id = item.Id;
            thongTinAPI.masoquocgia = item.MaSoQuocGia;
            thongTinAPI.thoigian = item.ThoiGian;
            thongTinAPI.tenquocgia = item.TenQuocGia;
            thongTinAPI.thongkenhanluc = item.ThongKeNhanLuc;
            thongTinAPI.thongkekinhphi = item.ThongKeKinhPhi;
            thongTinAPI.thongkeketqua = item.ThongKeKetQua;

            apiThongTins.Add(thongTinAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thông tin KHCN khu vực thành công!",
            Meta = new Meta(model, records),
            Data = apiThongTins
        });
    }

    [HttpGet("thongtin/{id:long}")]
    public async Task<IActionResult> GetCTThongTinkhcn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _thongTinRepository.GetByIdAsync(id);

        if (item == null) throw new ArgumentException("Không tìm thấy!");
        var apiThongTins = new List<ThongTinAPI>();

        if (item != null)
        {
            var thongTinAPI = new ThongTinAPI();
            thongTinAPI.Id = item.Id;
            thongTinAPI.masoquocgia = item.MaSoQuocGia;
            thongTinAPI.thoigian = item.ThoiGian;
            thongTinAPI.tenquocgia = item.TenQuocGia;
            thongTinAPI.thongkenhanluc = item.ThongKeNhanLuc;
            thongTinAPI.thongkekinhphi = item.ThongKeKinhPhi;
            thongTinAPI.thongkeketqua = item.ThongKeKetQua;

            apiThongTins.Add(thongTinAPI);
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thông tin thành công!",
            Data = apiThongTins
        });
    }


    //[HttpGet("tochuckhcn/chart/yearly")]
    //public async Task<IActionResult> GetTochuckhcnChartYearly([FromQuery] string fromYear, string toYear)
    //{
    //    var (items, records) = await _iOrganizationRepository.FilterAsync(model);
    //    return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
    //    {
    //        Message = "Truy xuất danh sách tổ chức thành công!",
    //        Meta = new Meta(model, records),
    //        Data = items
    //    });
    //}
}