using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Attributes;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using SoKHCNVTAPI.Helpers;
using System.Security.Claims;
using ClosedXML.Excel;
using System.Diagnostics;
using RestSharp;
using DocumentFormat.OpenXml.InkML;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.CommonCategories;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore;
using SoKHCNVTAPI.Migrations;
using System.Globalization;
using SoKHCNVTAPI.Configurations;
using SkiaSharp;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace SoKHCNVTAPI.Controllers;
[ApiController]
[Route("/api/v{version:apiVersion}/taptin")]
[ApiVersion("1")]
public class TapTinController : BaseController
{
    //private readonly IUserRepository _userRepository;
    private readonly IRepository<DinhDanhToChuc> _dinhDanhToChucRepository;
    private readonly IRepository<LGSP> _lgsgRepository;
    private readonly IRepository<ChuyenGia> _chuyenGiaRepository;
    private readonly IRepository<CanBo> _canBoRepository;
    private readonly IRepository<DoanhNghiep> _doanhNghiepRepository;
    private readonly IRepository<ToChuc> _toChucRepository;
    private readonly IRepository<DinhDanhChuyenGia> _dinhDanhChuyeGiaRepository;
    private readonly IRepository<LinhVucNghienCuu> _lichVucNghienCucRepository;
    private readonly IRepository<BucXa> _bucXaRepository;
    private readonly IRepository<XQuang> _xQuangRepository;
    private readonly IRepository<ThongTin> _thongTinRepository;
    private readonly IRepository<CongBo> _congBoRepository;
    private readonly IRepository<SoHuuTriTue> _soHuuChiTueRepository;
    private readonly IRepository<NhiemVu> _nhiemVuRepository;
    private readonly IRepository<DinhDanhNhiemVu> _dinhDanhNhiemVuRepository;
    private readonly IRepository<CapDoNhiemVu> _capDoNhiemVuRepository;
    private readonly IRepository<LoaiDuAn> _loaiDuAnRepository;
    private readonly IRepository<DonVi> _donViRepository;
    private readonly IRepository<LoaiNhiemVu> _loaiNhiemVuRepository;
    private readonly IRepository<LoaiHinhToChuc> _loaiHinhToChucRepository;
    private readonly IRepository<Title> _titleRepository;
    private readonly IRepository<QuocGia> _quocGiaRepository;
    private readonly IRepository<Degree> _hocViRepository;
    private readonly IRepository<TinhThanh> _tinhThanhRepository;
    private readonly IRepository<DinhDanhCanBo> _dinhDanhCanBoRepository;
    private readonly IRepository<HinhThucSoHuu> _hinhThucSoHuuRepository;
    private readonly IRepository<TrinhDoDaoTao> _trinhDoDaoTaoRepository;
    private readonly IRepository<ActivityLog> _activityLogRepository;
    private readonly IRepository<ActivityLogUser> _activityLogUserRepository;

    //private const string Module = "document";
    //private readonly IFileService _fileService;
    private const string UploadsSubDirectory = "Assets/Uploads";
    private readonly IEnumerable<string> allowedExtensions = new List<string> { ".zip", ".rar", ".bmp", ".png", ".jpg", ".jpge", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".xml" };

    public TapTinController(IUserRepository userRepository,
      
        IRepository<DinhDanhChuyenGia> dinhDanhChuyeGiaRepository,
        IRepository<Permission> permissionRepository,  
        IRepository<LGSP> lgsgRepository,
        IRepository<ChuyenGia> chuyenGiaRepository,
        IRepository<CanBo> canBoRepository,
        IRepository<ToChuc> toChucRepository,
        IRepository<DoanhNghiep> doanhNghiepRepository,
        IRepository<DinhDanhToChuc> dinhDanhToChucRepository,
        IRepository<LinhVucNghienCuu> lichVucNghienCucRepository,
        IRepository<BucXa> bucXaRepository,
        IRepository<XQuang> xQuangRepository,
        IRepository<ThongTin> thongTinRepository,
        IRepository<CongBo> congBoRepository,
        IRepository<SoHuuTriTue> soHuuChiTueRepository,
        IRepository<NhiemVu> nhiemVuRepository,
        IRepository<DinhDanhNhiemVu> dinhDanhNhiemVuRepository,
        IRepository<CapDoNhiemVu> capDoNhiemVuRepository,
        IRepository<LoaiDuAn> loaiDuAnRepository,
        IRepository<DonVi> donViRepository,
        IRepository<LoaiNhiemVu> loaiNhiemVuRepository,
        IRepository<LoaiHinhToChuc> loaiHinhToChucRepository,
        IRepository<Title> titleRepository,
        IRepository<QuocGia> quocGiaRepository,
        IRepository<Degree> hocViRepository,
        IRepository<TinhThanh> tinhThanhRepository,
        IRepository<DinhDanhCanBo> dinhDanhCanBoRepository,
        IRepository<HinhThucSoHuu> hinhThucSoHuuRepository,
        IRepository<TrinhDoDaoTao> trinhDoDaoTaoRepository,
        IRepository<ActivityLog> activityLogRepository,
        IRepository<ActivityLogUser> activityLogUserRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository)
    {
        _dinhDanhChuyeGiaRepository = dinhDanhChuyeGiaRepository;
        _lgsgRepository = lgsgRepository;
        _chuyenGiaRepository = chuyenGiaRepository;
        _canBoRepository = canBoRepository;
        _toChucRepository = toChucRepository;
        _dinhDanhToChucRepository = dinhDanhToChucRepository;
        _doanhNghiepRepository = doanhNghiepRepository;
        _lichVucNghienCucRepository = lichVucNghienCucRepository;
        _bucXaRepository = bucXaRepository;
        _xQuangRepository = xQuangRepository;
        _thongTinRepository = thongTinRepository;
        _congBoRepository = congBoRepository;
        _soHuuChiTueRepository = soHuuChiTueRepository;
        _nhiemVuRepository = nhiemVuRepository;
        _dinhDanhNhiemVuRepository = dinhDanhNhiemVuRepository;
        _capDoNhiemVuRepository = capDoNhiemVuRepository;
        _loaiDuAnRepository = loaiDuAnRepository;
        _donViRepository = donViRepository;
        _loaiNhiemVuRepository = loaiNhiemVuRepository;
        _loaiHinhToChucRepository = loaiHinhToChucRepository;
        _titleRepository = titleRepository;
        _quocGiaRepository = quocGiaRepository;
        _hocViRepository = hocViRepository;
        _tinhThanhRepository = tinhThanhRepository;
        _dinhDanhCanBoRepository = dinhDanhCanBoRepository;
        _hinhThucSoHuuRepository = hinhThucSoHuuRepository;
        _trinhDoDaoTaoRepository = trinhDoDaoTaoRepository;
        _activityLogRepository = activityLogRepository;
        _activityLogUserRepository = activityLogUserRepository;

    }
    
    [Authorize]
    [HttpPost("upload")]
    //[ProducesResponseType(StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [MultipartFormData]
    //[DisableFormValueModelBinding]
    public async Task<IActionResult> Upload(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        //var fileUploadSummary = await _fileService.UploadFileAsync(file, type);
        //return CreatedAtAction(nameof(Upload), fileUploadSummary);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            Data = filePath.Replace(UploadsSubDirectory + "/", "")
        });
    }

    [Authorize]
    [HttpPost("import/lgsp")]
    [MultipartFormData]
    public async Task<IActionResult> ImportLGSP(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                IList<LGSP> lGSPs = new List<LGSP>();
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                //using (TransactionScope scope = new TransactionScope())
                //{
                    foreach (var dataRow in nonEmptyDataRows)
                    {
                        //for row number check
                        if (dataRow.RowNumber() >= 2)
                        {
                            int ci = 1;
                        //to get column # 3's data
                        var stt = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var ky_bao_cao = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var ky_hd_moi = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var kiem_tra_tien_do_theo_ky = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var nghiem_thu_va_thanh_ly = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var don_dang_ky_so_huu_tri_tue = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cong_tac_tham_dinh = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cap_phep_buc_xa_cap_moi = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cap_phep_buc_xa_gia_han = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cap_phep_buc_xa_sua_doi = 0.0;
                        if (!dataRow.Cell(ci).Value.IsBlank)
                        {
                            cap_phep_buc_xa_sua_doi = dataRow.Cell(ci).Value.GetNumber();
                        }
                        ci++;
                        var so_luong_gian_hang = 0.0;
                        if (!dataRow.Cell(ci).Value.IsBlank)
                        {
                            so_luong_gian_hang = dataRow.Cell(ci).Value.GetNumber();
                        }
                        ci++;
                        var cap_phep_buc_xa_bo_sung = 0.0;
                        if (!dataRow.Cell(ci).Value.IsBlank)
                        {
                            cap_phep_buc_xa_bo_sung = dataRow.Cell(ci).Value.GetNumber();
                        }
                        ci++;
                        var so_luong_san_pham = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var so_luong_ho_tro = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cong_bo_hop_chuan = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var cong_bo_hop_quy = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var kiem_dinh = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var hieu_chuan = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var thu_nghiem = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var thoi_gian_bao_cao = dataRow.Cell(ci).Value.GetText();

                        var checkLgsp = await _lgsgRepository
                           .Select().FirstOrDefaultAsync(p =>
                           p.ky_bao_cao.ToLower().ToLower() == ky_bao_cao.ToLower());

                        if (checkLgsp == null)
                        {
                            var lgsp = new LGSP();
                            lgsp.ky_bao_cao = ky_bao_cao;
                            lgsp.ky_hd_moi = ((long)ky_hd_moi);
                            lgsp.kiem_tra_tien_do_theo_ky = ((long)kiem_tra_tien_do_theo_ky);
                            lgsp.nghiem_thu_va_thanh_ly = ((long)nghiem_thu_va_thanh_ly);
                            lgsp.so_luong_gian_hang = ((long)so_luong_gian_hang);
                            lgsp.cong_tac_tham_dinh = ((long)cong_tac_tham_dinh);
                            lgsp.cap_phep_buc_xa_cap_moi = ((long)cap_phep_buc_xa_cap_moi);
                            lgsp.cap_phep_buc_xa_gia_han = ((long)cap_phep_buc_xa_gia_han);
                            lgsp.cap_phep_buc_xa_sua_doi = ((long)cap_phep_buc_xa_sua_doi);
                            lgsp.cap_phep_buc_xa_bo_sung = ((long)cap_phep_buc_xa_bo_sung);
                            lgsp.so_luong_san_pham = ((long)so_luong_san_pham);
                            lgsp.don_dang_ky_so_huu_tri_tue = ((long)don_dang_ky_so_huu_tri_tue);
                            lgsp.so_luong_ho_tro = ((long)so_luong_ho_tro);
                            lgsp.cong_bo_hop_chuan = ((long)cong_bo_hop_chuan);
                            lgsp.cong_bo_hop_quy = ((long)cong_bo_hop_quy);
                            lgsp.kiem_dinh = ((long)kiem_dinh);
                            lgsp.hieu_chuan = ((long)hieu_chuan);
                            lgsp.thu_nghiem = ((long)thu_nghiem);
                            lgsp.NgayCapNhat = Utils.getCurrentDate();
                            lgsp.NgayTao = Utils.getCurrentDate();
                            if (thoi_gian_bao_cao != null && thoi_gian_bao_cao != "")
                            {
                                lgsp.thoi_gian_bao_cao = thoi_gian_bao_cao;
                            }
                            _lgsgRepository.Insert(lgsp);
                            lGSPs.Add(lgsp);
                            await _lgsgRepository.SaveChangesAsync();
                        }
                            //scope.Complete();
                        }
                    }                  
                //}
                // XOa file
                System.IO.File.Delete(filePath);
                // Call API
                await CallSyncAPI(lGSPs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/chuyengia")]
    [MultipartFormData]
    public async Task<IActionResult> ImportChuyenGia(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;

                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ma = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var maDinhDanh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var hoVaTen = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var sdt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var email = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngaySinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var quocTich = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var hocVi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var bangCap = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var lvChuyenMon = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var thongTinLienHe = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var diaChi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var hinhAnh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var video = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var thongTinTaiChinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var chiPhiDiLai = dataRow.Cell(ci).GetValue<string>().Trim();

                        //check mã 
                        var chuyengia = await _chuyenGiaRepository
                         .Select().FirstOrDefaultAsync(p =>
                                    p.Ma.ToLower().ToLower() == ma.ToLower());
                        if (chuyengia == null)
                        {
                            //throw new ArgumentException($"Mã  đã tồn tại! ");

                            chuyengia = new ChuyenGia() { Ma = ma };

                            var dinhDanhChuyenGia = _dinhDanhChuyeGiaRepository.Select().Where(p => p.Code.Trim().ToLower() == maDinhDanh.ToLower()).FirstOrDefault();
                            if (dinhDanhChuyenGia != null)
                            {
                                chuyengia.ExpertIdentifierId = dinhDanhChuyenGia.Id;
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                                {
                                    Message = "Mã định danh chuyên gia không tìm thấy với mã " + maDinhDanh,
                                    Success = false,
                                    ErrorCode = 1
                                });
                            }
                            chuyengia.HovaTen = hoVaTen;
                            chuyengia.SDT = sdt;
                            chuyengia.Email = email;
                            chuyengia.NgaySinh = ngaySinh;                        
                            chuyengia.QuocTich = quocTich;                         
                            chuyengia.HocVi = hocVi;                         
                            chuyengia.BangCap = bangCap;               
                            chuyengia.LVChuyenMon = lvChuyenMon;
                            chuyengia.ThongTinLienHe = thongTinLienHe;
                            chuyengia.DiaChi = diaChi;
                            chuyengia.HinhAnh = hinhAnh;
                            chuyengia.Video = video;
                            chuyengia.ThongTinTaiChinh = thongTinTaiChinh;
                            chuyengia.ChiPhiDiLai = chiPhiDiLai;
                            chuyengia.CreatedAt = DateTime.UtcNow;
                            _chuyenGiaRepository.Insert(chuyengia);
                            await SyncUtils.SyncImportChuyenGia(chuyengia);
                            await _chuyenGiaRepository.SaveChangesAsync();
                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }   
    
    /// <summary>
    ///
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    ///
    [Authorize]
    [HttpPost("import/tochuc")]
    [MultipartFormData]
    public async Task<IActionResult> ImportToChuc(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }
    
        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        //to get column # 3's data

                        var stt = dataRow.Cell(ci).Value.GetNumber();
                        ci++;
                        var MaDinhDanhToChuc = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var ma = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var TenToChuc = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var TenTiengAnh = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var DiaChi = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var TinhThanh = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var DienThoai = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var Email = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var Website = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var MoTa = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var NguoiDungDau = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var CoQuanQuanLyTrucTiep = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var CoQuanChuQuan = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var LoaiHinhToChuc = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        //var HinhThucSoHuu = dataRow.Cell(ci).Value.GetText();
                        var HinhThucSoHuu = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var LinhVucNC = dataRow.Cell(ci).Value.GetText();
                        ci++;
                        var ChuyenDoiCoCheTuChu = dataRow.Cell(ci).Value.GetText();


                        //Check Mã
                        var tochuc = await _toChucRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.Ma.ToLower().ToLower() == ma.ToLower());
                        if (tochuc == null)
                        {

                            //throw new ArgumentException($"Mã đã tồn tại! ");
                            
                            tochuc = new ToChuc()
                            {
                                Ma = ma,
                                TenToChuc = TenToChuc,
                                DienThoai = DienThoai
                            };

                            var dinhDanhToChuc = _dinhDanhToChucRepository.Select().Where(p => p.Code.Trim().ToLower() == MaDinhDanhToChuc.ToLower()).FirstOrDefault();
                            if (dinhDanhToChuc != null)
                            {
                                tochuc.OrganizationIdentifierId = dinhDanhToChuc.Id;
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                                {
                                    Message = "Mã định danh không tìm thấy với mã " + MaDinhDanhToChuc,
                                    Success = false,
                                    ErrorCode = 1
                                });
                            }

                            tochuc.TenTiengAnh = TenTiengAnh;
                            tochuc.DiaChi = DiaChi;
                            tochuc.TinhThanh = TinhThanh;
                            tochuc.DienThoai = DienThoai;
                            tochuc.Email = Email;
                            tochuc.Website = Website;
                            tochuc.MoTa = MoTa;
                            tochuc.NguoiDungDau = NguoiDungDau;
                           
                            //Kiểm tra cơ quan chủ quản trên tồn tại không
                            if (string.IsNullOrEmpty(CoQuanChuQuan))
                            {
                                tochuc.CoQuanChuQuan = null;
                            }
                            else
                            {
                               var coquanchuquan = await _donViRepository.Select()
                                     .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == CoQuanChuQuan.Trim().ToLower());
                                if (coquanchuquan == null) throw new ArgumentException("Không tìm thấy cơ quan chủ quản");
                                tochuc.CoQuanChuQuan = coquanchuquan.Id.ToString();
                            }

    
                            //Kiểm tra cơ quan quan ly truc tiep trên tồn tại không
                            if (string.IsNullOrEmpty(CoQuanQuanLyTrucTiep))
                            {
                                tochuc.CoQuanQuanLyTrucTiep = null;
                            }
                            else
                            {
                                var coquanquanlytructiep = await _toChucRepository.Select()
                                      .FirstOrDefaultAsync(p => p.TenToChuc.Trim().ToLower() == CoQuanQuanLyTrucTiep.Trim().ToLower());
                                if (coquanquanlytructiep == null) throw new ArgumentException("Không tìm thấy cơ quan quản lý trực tiếp");
                                tochuc.CoQuanQuanLyTrucTiep = coquanquanlytructiep.Id.ToString();
                            }
                        
                            //Kiểm tra Loại hình tổ chức có tồn tại không
                            var loaihinhtochuc = await _loaiHinhToChucRepository.Select()
                               .FirstOrDefaultAsync(p => p.Ten.Trim().ToLower() == LoaiHinhToChuc.Trim().ToLower());
                            if (loaihinhtochuc == null) throw new ArgumentException("không tìm thấy loại hình tổ chức");
                            tochuc.LoaiHinhToChuc = loaihinhtochuc.Id.ToString();


                            //tochuc.HinhThucSoHuu = 0;
                            //Kiểm tra Hình thức sở hữu có tồn tại không
                            var hinhthucSH = await _hinhThucSoHuuRepository.Select()
                               .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == HinhThucSoHuu.Trim().ToLower());
                            if (hinhthucSH == null) throw new ArgumentException("không tìm thấy hình thức sở hữu");
                            tochuc.HinhThucSoHuu = hinhthucSH.Id;

                            //-Mã số 1: Công lập
                            //-Mã số 2: Ngoài công lập
                            //- Mã số 3: Có vốn nước ngoài
                            //-Mã số 4 : Theo hình thức sở hữu
                            //if (HinhThucSoHuu.ToLower() == "Công lập".ToLower())
                            //{
                            //    tochuc.HinhThucSoHuu = 1;
                            //}
                            //else if (HinhThucSoHuu.ToLower() == "Ngoài công lập".ToLower())
                            //{
                            //    tochuc.HinhThucSoHuu = 2;
                            //}
                            //else if (HinhThucSoHuu.ToLower() == "Có vốn nước ngoài".ToLower())
                            //{
                            //    tochuc.HinhThucSoHuu = 3;
                            //}
                            //else if (HinhThucSoHuu.ToLower() == "Theo hình thức sở hữu".ToLower())
                            //{
                            //    tochuc.HinhThucSoHuu = 4;
                            //}

                            //Kiểm tra Lĩnh vực nghiên cứu có tồn tại không
                            var linhvucNC = await _lichVucNghienCucRepository.Select()
                               .FirstOrDefaultAsync(p => p.Ten.Trim().ToLower() == LinhVucNC.Trim().ToLower());
                            if (linhvucNC == null) throw new ArgumentException("không tìm thấy lĩnh vực nghiên cứu");
                            tochuc.LinhVucNC = linhvucNC.Id.ToString();

                            // 1: Tự bảo đảm chi thường xuyên và chi đầu tư
                            //-Mã số 2: Tự bảo đảm chi thường xuyên
                            //-Mã số 3: Tự bảo đảm một phần chi thường xuyên
                            //-Mã số 4: Nhà nước bảo đảm chi thường xuyên.
                            tochuc.ChuyenDoiCoCheTuChu = 0;

                            if (ChuyenDoiCoCheTuChu.ToLower() == "Tự đảm bảo chi thường xuyên và chi đầu tư".ToLower())
                            {
                                tochuc.ChuyenDoiCoCheTuChu = 1;
                            }
                            else if (ChuyenDoiCoCheTuChu.ToLower() == "Tự đảm bảo chi thường xuyên".ToLower())
                            {
                                tochuc.ChuyenDoiCoCheTuChu = 2;
                            }
                            else if (ChuyenDoiCoCheTuChu.ToLower() == "Tự đảm bảo một phần chi thường xuyên".ToLower())
                            {
                                tochuc.ChuyenDoiCoCheTuChu = 3;
                            }
                            else if (ChuyenDoiCoCheTuChu.ToLower() == "Nhà nước đảm bảo chi thường xuyên".ToLower())
                            {
                                tochuc.ChuyenDoiCoCheTuChu = 4;
                            }
                            tochuc.NgayTao = DateTime.Now;
                            tochuc.NgayCapNhat = DateTime.Now;
                            _toChucRepository.Insert(tochuc);
                            //call api
                            await SyncUtils.SyncImportToChuc(tochuc);
                            await _toChucRepository.SaveChangesAsync();

                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/canbo")]
    [MultipartFormData]
    public async Task<IActionResult> ImportCanBo(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        //to get column # 3's data
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ma = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var HoVaTen = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NgaySinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var GioiTinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        var GioiTinhValue = GioiTinh.ToLower() == "nam" ? (short)1 : (short)0;
                        ci++;
                        var QuocTich = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CCCD = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NoiOHienNay = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TinhThanh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DienThoai = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var Email = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ChucDanhNgheNghiep = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ChucDanh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NamPhongChucDanh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var HocVi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NamDatHocVi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CoQuanCongTac = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LinhVucNC = dataRow.Cell(ci).GetValue<string>().Trim();

                        //check mã va CCCD
                        var canbo = await _canBoRepository
                         .Select().FirstOrDefaultAsync(p =>
                                     p.CCCD.ToLower().ToLower() == CCCD.ToLower());
                                    
                        if (new[] { HoVaTen,NgaySinh,GioiTinh,QuocTich,CCCD,NoiOHienNay,TinhThanh }.Any(string.IsNullOrEmpty))
                        {
                            throw new ArgumentException("Trường bắt buộc phải nhập vào");
                        }
                        if (canbo == null)
                        {
                          
                            canbo = new CanBo { Ma = "", HoVaTen = HoVaTen, CCCD = CCCD, DienThoai = DienThoai };
                            var dinhDanhCanBo = _dinhDanhCanBoRepository.Select().Where(p => p.Name.Trim().ToLower() == ma.ToLower()).FirstOrDefault();
                            if (dinhDanhCanBo != null)
                            {
                                canbo.Ma = dinhDanhCanBo.Code + '.' + Utils.getCurrentDate().ToString("MMyffff");
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                                {
                                    Message = "Mã định danh can bo không tìm thấy với mã",
                                    Success = false,
                                    ErrorCode = 1
                                });
                            }
                            canbo.NgaySinh = NgaySinh;
                            canbo.GioiTinh = GioiTinhValue;
                            canbo.MoTa = HoVaTen;
                            canbo.NoiOHienNay = NoiOHienNay;
                            canbo.TinhThanh = TinhThanh;
                            canbo.Email = Email;

                            //Kiểm tra Chuc danh nghe nghiep trên tồn tại không
                         
                            if (string.IsNullOrEmpty(ChucDanhNgheNghiep))
                            {
                                canbo.ChucDanhNgheNghiep = null;
                            }
                            else
                            {
                                var chucdanhnghenghiep = await _titleRepository.Select()
                                     .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == ChucDanhNgheNghiep.ToLower());
                                if (chucdanhnghenghiep == null) throw new ArgumentException("Không tìm thấy chức danh nghề nghiệp");
                                canbo.ChucDanhNgheNghiep = chucdanhnghenghiep.Id.ToString();
                            }

                            //Kiểm tra Chuc danh có tồn tại không
                           
                            if (string.IsNullOrEmpty(ChucDanh))
                            {
                                canbo.ChucDanh = null;
                            }
                            else
                            {
                                var chucdanh = await _titleRepository.Select()
                                     .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == ChucDanh.ToLower());
                                if (chucdanh == null) throw new ArgumentException("Không tìm thấy chức danh");
                                canbo.ChucDanh = chucdanh.Id.ToString();
                            }
                            canbo.NamPhongChucDanh = NamPhongChucDanh;                      
                            //Kiểm tra hoc vi có tồn tại không
                            var hocvi = await _hocViRepository.Select()
                               .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == HocVi.Trim().ToLower());
                            if (hocvi == null) throw new ArgumentException("không tìm thấy học vị");
                            canbo.HocVi = hocvi.Id.ToString();


                            canbo.NamDatHocVi = NamDatHocVi;
                         
                            //Kiểm tra cơ quan công tác có tồn tại không
                            var coquancongtac = await _toChucRepository.Select()
                               .FirstOrDefaultAsync(p => p.TenToChuc.Trim().ToLower() == CoQuanCongTac.Trim().ToLower());
                            if (coquancongtac == null) throw new ArgumentException("không tìm thấy cơ quan công tác");
                            canbo.CoQuanCongTac = coquancongtac.Id.ToString();

                            canbo.LinhVucNC = "";
                            if (LinhVucNC != null)
                            {

                                string[] lvncs = LinhVucNC.Split(',');
                                IList<int> lvnnIds = new List<int>();
                                foreach (string s in lvncs)
                                {
                                    var linhvucngiencuu = await _lichVucNghienCucRepository.Select().Where(p => p.Ma.ToLower() == s.ToLower() || p.Ten.ToLower() == s.ToLower()).FirstOrDefaultAsync();
                                    if (linhvucngiencuu != null)
                                    {
                                        lvnnIds.Add((int)linhvucngiencuu.Id);
                                    }
                                }
                                canbo.LinhVucNC = String.Join(",", lvnnIds);
                            }                      
                            //Kiểm tra quốc tịch có tồn tại không
                            var quoctich = await _quocGiaRepository.Select()
                               .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == QuocTich.Trim().ToLower());
                            if (quoctich == null) throw new ArgumentException("không tìm thấy quốc tịch");
                            canbo.QuocTich = quoctich.Id.ToString();

                            canbo.NgayTao = Utils.getCurrentDate();
                            canbo.NgayThamGia = Utils.getCurrentDate();
                            _canBoRepository.Insert(canbo);
                             await SyncUtils.SyncImportCanBo(canbo);
                            await _canBoRepository.SaveChangesAsync();
                        }
                    }
                    
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }
    //
    [Authorize]
    [HttpPost("import/doanhnghiep")]
    [MultipartFormData]
    public async Task<IActionResult> ImportDoanhNghiep(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }
        
        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        //var ma = dataRow.Cell(ci).GetValue<string>().Trim();
                        //ci++;
                        var Ten = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TenVietTat = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TenTiengAnh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NgayThanhLap = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var MaSoThue = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DienThoai = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var Email = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var Website = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DiaChi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TinhThanh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThuTruong = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CoQuanChuQuan = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LoaiHinhToChuc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LinhVucNghienCuu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var VonDieuLe = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DTHangNam = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DoanhThuTT = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KinhPhiHangNam = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var HoatDongNCKH = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KQNC = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ChuyenGiaoCN = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var SPDVKHCN = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TaiSanTriTue = dataRow.Cell(ci).GetValue<string>().Trim();

                        //Check mã số thuế
                        var doanhnghiep = await _doanhNghiepRepository
                            .Select().FirstOrDefaultAsync(p => p.MaSoThue != null &&
                            p.MaSoThue.ToLower().ToLower() == MaSoThue.ToLower());
                        //check trường kh đc rỗng
                        if (new[] { Ten, TenTiengAnh, TenVietTat, NgayThanhLap,DienThoai,Email,Website,DiaChi,TinhThanh,ThuTruong }.Any(string.IsNullOrEmpty))
                        {
                            throw new ArgumentException("Trường bắt buộc phải nhập vào");
                        }
                        if (doanhnghiep == null)
                        {
                            //throw new ArgumentException($"Mã hoặc mã số thuế đã tồn tại! ");                     
                            doanhnghiep = new DoanhNghiep() { Ma =  "DN" + '.' + Utils.getCurrentDate().ToString("MMyffff"), };                          
                                doanhnghiep.MaSoThue = MaSoThue;
                                doanhnghiep.Ten = Ten;
                                doanhnghiep.DiaChi = DiaChi;
                                doanhnghiep.ThuTruong = ThuTruong;
                            //Kiểm tra Cơ quan chủ quản tồn tại không
                            if (string.IsNullOrEmpty(CoQuanChuQuan))
                            {
                                doanhnghiep.CoQuanChuQuan = null;
                            }
                            else
                            {
                                var coquanchuquan = await _donViRepository.Select()
                                .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == CoQuanChuQuan.Trim().ToLower());
                                if (coquanchuquan == null) throw new ArgumentException("Không tìm thấy cơ quan chủ quản ");
                                doanhnghiep.CoQuanChuQuan = coquanchuquan.Id.ToString();
                            }                        
                                doanhnghiep.TinhThanh = TinhThanh;
                                doanhnghiep.TenVietTat = TenVietTat;
                                doanhnghiep.TenTiengAnh = TenTiengAnh;
                                doanhnghiep.NgayThanhLap = NgayThanhLap;                      
                                doanhnghiep.DienThoai = DienThoai;
                                doanhnghiep.Email = Email;
                                doanhnghiep.Website = Website;
                            
                            //Kiểm tra Loại hình tổ chức tòn tại không
                            if (string.IsNullOrEmpty(LoaiHinhToChuc))
                            {
                                doanhnghiep.LoaiHinhToChuc = null;
                            }
                            else
                            {
                                var loaihinhtochuc = await _loaiHinhToChucRepository.Select()
                                .FirstOrDefaultAsync(p => p.Ten.Trim().ToLower() == LoaiHinhToChuc.Trim().ToLower());
                                if (loaihinhtochuc == null) throw new ArgumentException("Không tìm thấy loại hình tổ chức ");
                                doanhnghiep.LoaiHinhToChuc = loaihinhtochuc.Id.ToString();
                            }
                            //Kiểm tra lĩnh vực nghiên cứu không                              
                            if (string.IsNullOrEmpty(LinhVucNghienCuu))
                            {
                                doanhnghiep.LinhVucNghienCuu = null;
                            }
                            else
                            {
                                var linhvucnghiencuu = await _lichVucNghienCucRepository.Select()
                                .FirstOrDefaultAsync(p => p.Ten.Trim().ToLower() == LinhVucNghienCuu.Trim().ToLower());
                                if (linhvucnghiencuu == null) throw new ArgumentException("Không tìm thấy lĩnh vực nghiên cứu ");
                                doanhnghiep.LinhVucNghienCuu = linhvucnghiencuu.Id.ToString();
                            }
                                doanhnghiep.VonDieuLe = VonDieuLe;
                                doanhnghiep.DTHangNam = DTHangNam;
                                doanhnghiep.DoanhThuTT = DoanhThuTT;
                                doanhnghiep.KinhPhiHangNam = KinhPhiHangNam;
                                doanhnghiep.HoatDongNCKH = HoatDongNCKH;
                                doanhnghiep.KQNC = KQNC;
                                doanhnghiep.ChuyenGiaoCN = ChuyenGiaoCN;
                                doanhnghiep.SPDVKHCN = SPDVKHCN;
                                doanhnghiep.TaiSanTriTue = TaiSanTriTue;
                                //CreatedAt = DateTime.UtcNow,
                            
                            _doanhNghiepRepository.Insert(doanhnghiep);
                            //call api
                            await SyncUtils.SyncImportDoanhNghiep(doanhnghiep);
                            await _doanhNghiepRepository.SaveChangesAsync();
                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/bucxa")]
    [MultipartFormData]
    public async Task<IActionResult> ImportBucXa(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {               
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ho_va_ten = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_sinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var so_cccd = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var so_chung_chi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_cap = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ghi_chu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var thoi_gian = dataRow.Cell(ci).GetValue<string>().Trim();

                      
                         var checkBucXa = await _bucXaRepository
                        .Select()
                        .FirstOrDefaultAsync(p => p.so_cccd.ToLower() == so_cccd.ToLower());

                        if (checkBucXa != null)
                        {
                            // Update the existing entity
                            checkBucXa.ho_va_ten = ho_va_ten;
                            checkBucXa.ngay_sinh = ngay_sinh;
                            checkBucXa.so_chung_chi = so_chung_chi;
                            checkBucXa.ngay_cap = ngay_cap;
                            checkBucXa.ghi_chu = ghi_chu;
                            checkBucXa.thoi_gian = thoi_gian;

                            _bucXaRepository.Update(checkBucXa);
                        }
                        else
                        {
                            // Create a new entity
                            var bucxa = new BucXa
                            {
                                ho_va_ten = ho_va_ten,
                                ngay_sinh = ngay_sinh,
                                so_cccd = so_cccd,
                                so_chung_chi = so_chung_chi,
                                ngay_cap = ngay_cap,
                                ghi_chu = ghi_chu,
                                thoi_gian = thoi_gian
                            };

                            _bucXaRepository.Insert(bucxa);
                        }

                        await _bucXaRepository.SaveChangesAsync();

                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/xquang")]
    [MultipartFormData]
    public async Task<IActionResult> ImportXQuang(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {

                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ten_co_so = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var lien_he = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var loai_thiet_bi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var serial_number = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var hang_san_xuat = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var nuoc_san_xuat = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var nam_san_xuat = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var so_giay_phep_nam_truoc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_cap_nam_truoc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_het_han_nam_truoc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var so_giay_phep_trong_nam = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_cap_trong_nam = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ngay_het_han_trong_nam = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var chua_dat_dieu_kien_cap_phep = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var chua_lam_thu_tuc_cap_phep = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var tong_so_nhan_vien_buc_xa = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ghi_chu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var thoi_gian = dataRow.Cell(ci).GetValue<string>().Trim();


                        //Check 
                        var xQuang = await _xQuangRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.serial_number.ToLower().ToLower() == serial_number.ToLower());
                        if (xQuang != null)
                        {

                            xQuang.ten_co_so = ten_co_so;
                            xQuang.lien_he = lien_he;
                            xQuang.loai_thiet_bi = loai_thiet_bi;
                            xQuang.serial_number = serial_number;
                            xQuang.hang_san_xuat = hang_san_xuat;
                            xQuang.nuoc_san_xuat = nuoc_san_xuat;
                            xQuang.nam_san_xuat = nam_san_xuat;
                            xQuang.so_giay_phep_nam_truoc = so_giay_phep_nam_truoc;
                            xQuang.ngay_cap_nam_truoc = ngay_cap_nam_truoc;
                            xQuang.ngay_het_han_nam_truoc = ngay_het_han_nam_truoc;
                            xQuang.so_giay_phep_trong_nam = so_giay_phep_trong_nam;
                            xQuang.ngay_cap_trong_nam = ngay_cap_trong_nam;
                            xQuang.ngay_het_han_trong_nam = ngay_het_han_trong_nam;
                            xQuang.chua_dat_dieu_kien_cap_phep = chua_dat_dieu_kien_cap_phep;
                            xQuang.chua_lam_thu_tuc_cap_phep = chua_lam_thu_tuc_cap_phep;
                            xQuang.tong_so_nhan_vien_buc_xa = tong_so_nhan_vien_buc_xa;
                            xQuang.ghi_chu = ghi_chu;
                            xQuang.thoi_gian = thoi_gian;
                            _xQuangRepository.Update(xQuang);
                        }
                        else
                        {
                            // Create a new entity
                            var xquang = new XQuang
                            {
                                ten_co_so = ten_co_so,
                                lien_he = lien_he,
                                loai_thiet_bi = loai_thiet_bi,
                                serial_number = serial_number,
                                hang_san_xuat = hang_san_xuat,
                                nuoc_san_xuat = nuoc_san_xuat,
                                nam_san_xuat = nam_san_xuat,
                                so_giay_phep_nam_truoc = so_giay_phep_nam_truoc,
                                ngay_cap_nam_truoc = ngay_cap_nam_truoc,
                                ngay_het_han_nam_truoc = ngay_het_han_nam_truoc,
                                so_giay_phep_trong_nam = so_giay_phep_trong_nam,
                                ngay_cap_trong_nam = ngay_cap_trong_nam,
                                ngay_het_han_trong_nam = ngay_het_han_trong_nam,
                                chua_dat_dieu_kien_cap_phep = chua_dat_dieu_kien_cap_phep,
                                chua_lam_thu_tuc_cap_phep = chua_lam_thu_tuc_cap_phep,
                                tong_so_nhan_vien_buc_xa = tong_so_nhan_vien_buc_xa,
                                ghi_chu = ghi_chu,
                                thoi_gian = thoi_gian,                            
                            };
                            _xQuangRepository.Insert(xquang);
                        }
                        await _xQuangRepository.SaveChangesAsync();

                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/thongtin")]
    [MultipartFormData]
    public async Task<IActionResult> ImportThongTinKHCN(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {

                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var MaSoQuocGia = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TenQuocGia = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThoiGian = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongKeNhanLuc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongKeKinhPhi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongKeKetQua = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongKeHoatDong = dataRow.Cell(ci).GetValue<string>().Trim();
                       
                        var checkMa = await _thongTinRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.MaSoQuocGia.ToLower() == MaSoQuocGia.ToLower()|| p.TenQuocGia.ToLower() == TenQuocGia.ToLower());

                        if (checkMa == null)
                        {
                            
                            DateTime dateTime = DateTime.Parse(ThoiGian);
                            var thongtin = new ThongTin()
                            {
                                MaSoQuocGia = MaSoQuocGia,
                                TenQuocGia = TenQuocGia,                               
                                ThoiGian=dateTime,
                                ThongKeNhanLuc = ThongKeNhanLuc,
                                ThongKeKinhPhi = ThongKeKinhPhi,
                                ThongKeKetQua = ThongKeKetQua,
                                ThongKeHoatDong = ThongKeHoatDong,                            
                            };

                        _thongTinRepository.Insert(thongtin);
                            await SyncUtils.SyncImportThongTinKHCN(thongtin);
                            await _thongTinRepository.SaveChangesAsync();
                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/congbo")]
    [MultipartFormData]
    public async Task<IActionResult> ImportCongBo(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {

                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ChiSoDeMuc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LinhVucNghienCuu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var DangTaiLieu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TacGia = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NhanDe = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NguonTrich = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NamXuatBan = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var So = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var Trang = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ISSN = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TuKhoa = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TomTat = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KyHieuKho = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var XemToanVan = dataRow.Cell(ci).GetValue<string>().Trim();
            
                        var checkMa = await _congBoRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.ChiSoDeMuc.ToLower() == ChiSoDeMuc.ToLower());

                        if (new[] { ChiSoDeMuc,NhanDe }.Any(string.IsNullOrEmpty))
                        {
                            throw new ArgumentException("Trường bắt buộc phải nhập vào");
                        }
                        if (checkMa == null)
                        {
                            /* throw new ArgumentException($"số cccd đã tồn tại! ");*/

                            var congbo = new CongBo()
                            {
                                ChiSoDeMuc = ChiSoDeMuc,
                                LinhVucNghienCuu = LinhVucNghienCuu,
                                DangTaiLieu = DangTaiLieu,
                                TacGia = TacGia,
                                NhanDe = NhanDe,
                                NguonTrich = NguonTrich,
                                NamXuatBan = NamXuatBan,
                                So = So,
                                Trang = Trang,
                                ISSN = ISSN,
                                TuKhoa = TuKhoa,
                                TomTat = TomTat,
                                KyHieuKho = KyHieuKho,
                                XemToanVan = XemToanVan,
                            };

                            _congBoRepository.Insert(congbo);
                            await SyncUtils.SyncImportCongBo(congbo);
                            await _congBoRepository.SaveChangesAsync();
                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/sohuutritue")]
    [MultipartFormData]
    public async Task<IActionResult> ImportSoHuuChiTue(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }
        

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }

        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
               var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TenSangChe = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LoaiSoHuu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var PhanLoai = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var SoBang = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ChuBang = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongTinSoHuu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var SangCheToanVan = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ToChucDaiDien = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NguoiDaiDien = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ToChucDuDieuKien = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CaNhanDuDieuKien = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var NgayCap = dataRow.Cell(ci).GetValue<string>().Trim();
                        
                        var checkMa = await _soHuuChiTueRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.TenSangChe.ToLower() == TenSangChe.ToLower());
                        if (checkMa == null)
                        {                        
                            var sohuutritue = new SoHuuTriTue()
                            {
                                TenSangChe = TenSangChe,
                                LoaiSoHuu = LoaiSoHuu,
                                PhanLoai = PhanLoai,
                                SoBang = SoBang,
                                ChuBang = ChuBang,
                                ThongTinSoHuu = ThongTinSoHuu,
                                SangCheToanVan = SangCheToanVan,
                                ToChucDaiDien = ToChucDaiDien,
                                NguoiDaiDien = NguoiDaiDien,
                                ToChucDuDieuKien = ToChucDuDieuKien,
                                CaNhanDuDieuKien = CaNhanDuDieuKien,
                                NgayCap = NgayCap,
                                NgayTao = DateTime.Now,
                                NgayCapNhat = DateTime.Now
                            };

                            _soHuuChiTueRepository.Insert(sohuutritue);
                            await SyncUtils.SyncImportSoHuuTriTue(sohuutritue);
                            await _soHuuChiTueRepository.SaveChangesAsync();
                        }                       
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [Authorize]
    [HttpPost("import/nhiemvu")]
    [MultipartFormData]
    public async Task<IActionResult> ImportNhiemVu(FileModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }
        //if (!await Can("Upload file", Module)) return PermissionMessage();

        if (model.file.Length <= 0 || model.file.ContentType == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        var extension = Path.GetExtension(model.file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Loại tập tin không đúng",
                Success = false,
                ErrorCode = 1
            });
        }

        string finalSubDirectory = Path.Combine(UploadsSubDirectory, model.type, Utils.GetUUID());
        Directory.CreateDirectory(finalSubDirectory);

        var filePath = Path.Combine(finalSubDirectory, model.file.FileName);

        using (var stream = System.IO.File.Create(filePath))
        {
            // The file is saved in a buffer before being processed
            await model.file.CopyToAsync(stream);
        }
        
        using (var excelWorkbook = new XLWorkbook(filePath))
        {
            try
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();
                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() >= 2)
                    {
                        int ci = 1;
                        var stt = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TenNhiemVu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var MaSoNhiemVu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var MaDinhDanhNhiemVu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CapNhiemVu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThuocChuongTrinh = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CoQuanQuanLy = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ToChucChuTri = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CoQuanCapTren = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var CoQuanChuQuan = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThongTinCanBo = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LinhVucNC = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var LoaiNhiemVu = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TongThoiGian = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThoiGianBatDau = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var ThoiGianKetThuc = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TongKinhPhi = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KinhPhiNganSach = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KinhPhiTuCo = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var KinhPhiKhac = dataRow.Cell(ci).GetValue<string>().Trim();
                        ci++;
                        var TuKhoa = dataRow.Cell(ci).GetValue<string>().Trim();

                        //Check Mã
                        var nhiemvu = await _nhiemVuRepository
                            .Select().FirstOrDefaultAsync(p =>
                            p.MissionNumber.ToLower().ToLower() == MaSoNhiemVu.ToLower());
                            if(nhiemvu!=null) throw new ArgumentException("Mã đã tồn tại!");


                        if (nhiemvu == null)
                        {
                            nhiemvu = new NhiemVu()
                            {
                                Code = "",
                                Name = "",
                                MissionNumber = MaSoNhiemVu
                            };
                            if (TenNhiemVu == null) throw new ArgumentException("Tên nhiệm vụ bắt buộc!");
                                nhiemvu.Name = TenNhiemVu; 
                           

                            //Kiểm Tra tên nhiệm vụ tồn tại không                       
                            var dinhDanhNhiemVu = await _dinhDanhNhiemVuRepository.Select()
                                  .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == MaDinhDanhNhiemVu.Trim().ToLower());                           
                            if (dinhDanhNhiemVu == null) throw new ArgumentException("Không tìm thấy mã định danh");                      
                                nhiemvu.Code = dinhDanhNhiemVu.Code + "." + Utils.getCurrentDate().ToString("MMyffff");
                                nhiemvu.MissionIdentifyId = dinhDanhNhiemVu.Id;
                            
                            
                            //Kiểm tra cấp nhiệm vụ tồn tại không
                            var capDoNhiemVu = await _capDoNhiemVuRepository.Select()
                               .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == CapNhiemVu.Trim().ToLower());                          
                            if (capDoNhiemVu == null) throw new ArgumentException("Không tìm thấy cấp nhiệm vụ");
                                nhiemvu.MissionLevelId = capDoNhiemVu.Id;
                            
                        
                            //Kiểm Tô chức chủ chì tồn tại không
                            var tochucchuchi = await _toChucRepository.Select()
                            .FirstOrDefaultAsync(p => p.TenToChuc.Trim().ToLower() == ToChucChuTri.Trim().ToLower());                         
                            if (tochucchuchi == null) throw new ArgumentException("Không tìm thấy chủ chì");
                            nhiemvu.ToChucChuTri = tochucchuchi.Id;
                            
             
                            //Kiểm tra cơ quan cấp trên tồn tại không
                            if (string.IsNullOrEmpty(CoQuanCapTren))
                            {
                                nhiemvu.CoQuanCapTren = null;
                            }
                            else {
                                var coquancaptren = await _toChucRepository.Select()
                                     .FirstOrDefaultAsync(p => p.TenToChuc.Trim().ToLower() == CoQuanCapTren.Trim().ToLower());                               
                                if (coquancaptren == null) throw new ArgumentException("Không tìm thấy cơ quan cấp trên");        
                                    nhiemvu.CoQuanCapTren = coquancaptren.Id;                           
                            }
                            
                            //Cơ quan chủ quản
                            if (string.IsNullOrEmpty(CoQuanChuQuan))
                            {
                                nhiemvu.OrganizationId = null;
                            }
                            else
                            {
                                var coquanchuquan = await _toChucRepository.Select()
                                  .FirstOrDefaultAsync(p => p.TenToChuc.Trim().ToLower() == CoQuanChuQuan.Trim().ToLower());                              
                                if (coquanchuquan == null) throw new ArgumentException("không tìm thấy chủ quản");                               
                                    nhiemvu.OrganizationId = coquanchuquan.Id;                         
                            }

                            //Cán bộ tham gia
                            if (string.IsNullOrEmpty(ThongTinCanBo)) 
                            { nhiemvu.CanBo = null; }
                            else
                            {
                                var thongtincanbo = await _canBoRepository.Select()
                                .FirstOrDefaultAsync(p => p.HoVaTen.Trim().ToLower() == ThongTinCanBo.Trim().ToLower());
                                //.ToList().Where(p => p.HoVaTen.Trim().ToLower() == ThongTinCanBo.ToLower()).FirstOrDefault();
                                if (thongtincanbo == null)throw new ArgumentException("không tìm thấy cán bộ");                  
                                    nhiemvu.CanBo = thongtincanbo.Id.ToString();
                            }

                            //projectType 
                            if (string.IsNullOrEmpty(ThuocChuongTrinh))
                            {
                                nhiemvu.ProjectTypeId = null;
                            }
                            else
                            {
                                var thuocchuongtrinh = await _loaiDuAnRepository.Select()
                                     .FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == ThuocChuongTrinh.Trim().ToLower());           
                                if (thuocchuongtrinh == null) throw new ArgumentException("không tìm thấy đơn vị");
                                nhiemvu.ProjectTypeId = thuocchuongtrinh.Id;
                           
                            }
                            //CoQuanQuanLy                          
                            if (string.IsNullOrEmpty(CoQuanQuanLy))
                            {
                                nhiemvu.CoQuanQuanLyKinhPhiNV = null;
                            }
                            else
                            {
                                var donvi =  _donViRepository.Select()                                  
                                    .ToList().FirstOrDefault(p => p.Name.Trim().ToLower() == CoQuanQuanLy.Trim().ToLower());                                                         
                                if (donvi == null) throw new ArgumentException("không tìm thấy cơ quan quản lý");                              
                                    nhiemvu.CoQuanQuanLyKinhPhiNV = donvi.Id.ToString();              
                            }
                       

                            //lĩnh vực nghiên cứu
                            var linhvucNC = _lichVucNghienCucRepository.Select()
                               .ToList().FirstOrDefault(p => p.Ten.Trim().ToLower() == LinhVucNC.Trim().ToLower());               
                            if (linhvucNC == null) throw new ArgumentException("không tìm thấy lĩnh vực nghiên cứu");
                                nhiemvu.LinhVucNghienCuuId = linhvucNC.Id;
                     

                            //loai nhiem vu
                            var loaiNV = _loaiNhiemVuRepository.Select()
                                  .ToList().FirstOrDefault(p => p.Name.Trim().ToLower() == LoaiNhiemVu.Trim().ToLower());                     
                            if (loaiNV == null) throw new ArgumentException("Không tìm thấy loại nhiệm vụ");
                               nhiemvu.LoaiHinhNhiemVu = loaiNV.Id;
                                                       
                            nhiemvu.TotalTimeWithMonth = Int32.Parse(TongThoiGian);
                            DateTime dateTime = DateTime.Parse(ThoiGianBatDau);
                            nhiemvu.StartTime = dateTime;
                            DateTime dateTime1 = DateTime.Parse(ThoiGianKetThuc);
                            nhiemvu.EndTime = dateTime1;
                            nhiemvu.TotalExpenses = decimal.Parse(TongKinhPhi);
                            nhiemvu.GovernmentExpenses = decimal.Parse(KinhPhiNganSach);
                            nhiemvu.SelfExpenses = decimal.Parse(KinhPhiTuCo);
                            nhiemvu.OtherExpenses = decimal.Parse(KinhPhiKhac);
                            nhiemvu.Keywords = TuKhoa;
                            _nhiemVuRepository.Insert(nhiemvu);
                            //call api
                            await SyncUtils.SyncImportNhiemVu(nhiemvu);
                            await _nhiemVuRepository.SaveChangesAsync();
                        }
                    }
                }
                // Xoa file
                System.IO.File.Delete(filePath);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                {
                    Message = "Tải lên lỗi: " + ex.Message,
                    Success = false,
                    ErrorCode = 1
                });
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Tải lên thành công!",
            Success = true,
            ErrorCode = 0
        });
    }
    
    protected async Task<bool> CallSyncAPI(IList<LGSP> lgsp)
    {
        var options = new RestClientOptions("https://rm-v3.systems.vn");
        var client = new RestClient(options);
        var request = new RestRequest("/api/data?action=import&slug=lgsp-bao-cao-khcn", Method.Post);
        request.AddHeader("Client-Id", "666808304cb2529fca02e755");
        request.AddHeader("Client-Secret", "c3ad4c415e4c48c98652e7e194450f7e");
        request.AddHeader("Content-Type", "application/json");
        var body = JsonConvert.SerializeObject(lgsp);
        request.AddStringBody(body, DataFormat.Json);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);

        if (response.IsSuccessful)
        {
            return true;
        }
        else
        {
            Console.WriteLine(response.ErrorMessage);
            return false;
        }
    }

    [HttpGet("file/{path}/{subpath}/{filename}")]
    public async Task<FileStreamResult?> DownloadFile(string path, string subpath, string filename)
    {
        string finalSubDirectory = Path.Combine(UploadsSubDirectory, path, subpath, filename);

        if (System.IO.File.Exists(finalSubDirectory))
        {
            string absolute = Path.GetFullPath(finalSubDirectory);
            var extension = Path.GetExtension(finalSubDirectory);
           // string file_name = Path.GetFileName(finalSubDirectory);
            string contentType = "application/force-download";
            if (extension != null)
            {
                if (extension == ".pdf")
                {
                    contentType = "Application/pdf";
                }
                else if (extension == ".zip" || extension == ".rar")
                {
                    contentType = "application/zip";
                }
                else if (extension == ".docx")
                {
                    contentType = "application/vnd.ms-word";
                }
                else if (extension == ".doc")
                {
                    contentType = "application/vnd.ms-word";
                }
                else if (extension == ".xlsx")
                {
                    contentType = "application/octet-stream";
                }
                else if (extension == ".xls")
                {
                    contentType = "application/octet-stream";
                }
                else if (extension == ".png" || extension == ".jpg" || extension == ".jpeg")
                {
                    contentType = "image/" + extension.Replace(".", "");
                }
            }
            var stream = new MemoryStream(await System.IO.File.ReadAllBytesAsync(absolute));
            var response = File(stream, contentType);

            return response;
        }
        return new FileStreamResult(null, "application/uinknow");
    }

    [HttpPost("export/activity-log")]
    public async Task<IActionResult> ExportActivitylog([FromQuery] string startDate, [FromQuery] string endDate)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không có quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }

        DateTime start_Date, end_Date;

        if (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_Date) ||
            !DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_Date))
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Ngày bắt đầu hoặc ngày kết thúc không hợp lệ",
                Success = false,
                ErrorCode = 2
            });
        }

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ActivityLog");
            worksheet.Cell(1, 1).Value = "Ngày tạo";
            worksheet.Cell(1, 2).Value = "Họ và Tên";
            worksheet.Cell(1, 3).Value = "Nội dung";

            int row = 2;
            int pageNumber = 1;
            int pageSize = 10000;
            bool hasMoreData = true;

            while (hasMoreData)
            {
                var records = await _activityLogRepository.Select()
                    .Where(p => p.CreatedAt.HasValue && p.CreatedAt.Value.Date >= start_Date.Date && p.CreatedAt.Value.Date <= end_Date.Date)
                    .AsNoTracking()
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new { p.Id, p.Contents, p.CreatedAt, p.FullName })
                    .ToListAsync();

                if (records.Count == 0)
                {
                    hasMoreData = false;
                }
                else
                {
                    foreach (var record in records)
                    {
                        worksheet.Cell(row, 1).Value = record.CreatedAt?.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(row, 2).Value = record.FullName;
                        worksheet.Cell(row, 3).Value = record.Contents;
                        row++;
                    }

                    pageNumber++;
                }
            }

            worksheet.Columns().AdjustToContents();
            var range = worksheet.Range(1, 1, row - 1, 3);
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;
                var filename = $"Nhat_Ky_He_Thong_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
        }
    }
    [HttpPost("export/activity-log-user")]
    public async Task<IActionResult> ExportActivityUserlog([FromQuery] string startDate, [FromQuery] string endDate)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không có quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }

        DateTime start_Date, end_Date;
        IEnumerable<dynamic> records = Enumerable.Empty<dynamic>();

        if (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out start_Date) ||
            !DateTime.TryParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out end_Date))
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Ngày bắt đầu hoặc ngày kết thúc không hợp lệ",
                Success = false,
                ErrorCode = 2
            });
        }

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("ActivityLogUser");
            worksheet.Cell(1, 1).Value = "Ngày tạo";
            worksheet.Cell(1, 2).Value = "Họ và Tên";
            worksheet.Cell(1, 3).Value = "Nội dung";

            int row = 2;
            int pageNumber = 1;
            int pageSize = 10000;
            bool hasMoreData = true;
         
            while (hasMoreData)
            {
                 records = await _activityLogUserRepository.Select()
                    .Where(p => p.CreatedAt.HasValue && p.CreatedAt.Value.Date >= start_Date.Date && p.CreatedAt.Value.Date <= end_Date.Date)
                    .AsNoTracking()
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)                   
                    .ToListAsync();
                //
                if (role.ToLower() != "sa")
                {
                    records = records.Where(p => (p.UserId) == userId);
                }

                if (records.Count() == 0)
                {
                    hasMoreData = false;
                }
                else
                {
                    foreach (var record in records)
                    {
                        worksheet.Cell(row, 1).Value = record.CreatedAt?.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(row, 2).Value = record.FullName;
                        worksheet.Cell(row, 3).Value = record.Contents;
                        row++;
                    }

                    pageNumber++;
                }
            }

            worksheet.Columns().AdjustToContents();
            var range = worksheet.Range(1, 1, row - 1, 3);
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;
                var filename = $"Nhat_Ky_Nguoi_Dung_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
        }
    }
}
