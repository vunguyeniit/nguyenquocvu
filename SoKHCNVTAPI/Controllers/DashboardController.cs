using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using SoKHCNVTAPI.Entities.CommonCategories;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Bibliography;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/dashboard")]
[ApiVersion("1")]
public class DashboardController : ControllerBase
{
    private readonly IRepository<NhiemVu> _nhiemVuRepository;
    private readonly IRepository<LoaiNhiemVu> _loaiHinhNhiemVuRepository;
    private readonly IRepository<LinhVucNghienCuu> _linhVucNghienCuuRepository;
    private readonly IRepository<LoaiHinhToChuc> _loaiHinhToChucRepository;
    private readonly IRepository<ToChuc> _toChucRepository;
    private readonly IRepository<CanBo> _canBoRepository; 
    private readonly IRepository<DoanhNghiep> _doanhNghiepepository;
    public DashboardController(IRepository<NhiemVu> nhiemVuRepository, IRepository<LoaiNhiemVu> loaiHinhNhiemVuRepository,
            IRepository<LoaiHinhToChuc> loaiHinhToChucRepository, IRepository<CanBo> canBoRepository, 
            IRepository<DoanhNghiep> doanhNghiepepository,
            IRepository<ToChuc> toChucRepository, IRepository<LinhVucNghienCuu> linhVucNghienCuuRepository)
    {
        _nhiemVuRepository = nhiemVuRepository;
        _loaiHinhNhiemVuRepository = loaiHinhNhiemVuRepository;
        _toChucRepository = toChucRepository;
        _loaiHinhToChucRepository = loaiHinhToChucRepository;
        _linhVucNghienCuuRepository = linhVucNghienCuuRepository;
        _doanhNghiepepository = doanhNghiepepository;
        _canBoRepository = canBoRepository;
    }

    [HttpGet("nhiemvu")]
    public async Task<IActionResult> GetBieuDoNhiemVu([FromQuery] BieuDoFilter model)
    {
        var lhnvs = await _loaiHinhNhiemVuRepository.Select().Where(P => P.Status == 1).ToListAsync();
        Dictionary<String, String> dics = new Dictionary<String, String>();
        if(lhnvs != null)
        {
            foreach (var lhnv in lhnvs)
            {
                var _num = _nhiemVuRepository.Select().Where(p => p.LoaiHinhNhiemVu == lhnv.Id ).Count();
                dics.Add(lhnv.Name, _num.ToString());
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất thống kê nhiệm vụ",
            Data = dics
        });
    }


    [HttpGet("nhiemvu/trangthai")]
    public async Task<IActionResult> GetBieuDoNV([FromQuery] BieuDoFilter model)
    {
        Dictionary<int, String> dics = new Dictionary<int, String>();

        for(int m = 1; m<=12; m++)
        {
            var _num = await _nhiemVuRepository.Select().Where(p => p.CreatedAt != null && p.CreatedAt.Value.Month == m && (p.Status == 1 || p.Status == 0)).CountAsync();
            var _num2 = await _nhiemVuRepository.Select().Where(p => p.CreatedAt != null && p.CreatedAt.Value.Month == m && (p.Status == 2)).CountAsync();
            var _num3 = await _nhiemVuRepository.Select().Where(p => p.CreatedAt != null && p.CreatedAt.Value.Month == m && (p.Status == 3 || p.Status == 4)).CountAsync();
            dics.Add(m, _num + "@" +_num2 + "@"+ _num3);
        }
        
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất thống kê nhhiệm vụ",
            Data = dics
        });
    }

    [HttpGet("tochuc")]
    public async Task<IActionResult> GetBieuDoToChuc([FromQuery] BieuDoFilter model)
    {
        var lhnvs = await _loaiHinhToChucRepository.Select().Where(P => P.TrangThai == 1).ToListAsync();
        Dictionary<String, String> dics = new Dictionary<String, String>();
        if (lhnvs != null)
        {
            foreach (var lhnv in lhnvs)
            {
                var _num = _toChucRepository.Select().Where(p => p.LoaiHinhToChuc != null && p.LoaiHinhToChuc.Contains(lhnv.Id.ToString())).Count();
                dics.Add(lhnv.Ten, _num.ToString());
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất thống kê tổ chức",
            Data = dics
        });
    }


    [HttpGet("canbo")]
    public async Task<IActionResult> GetBieuDoCanBo([FromQuery] BieuDoFilter model)
    {
        var lhnvs = await _linhVucNghienCuuRepository.Select().Where(P => P.TrangThai == 1).ToListAsync();
        Dictionary<String, String> dics = new Dictionary<String, String>();
        if (lhnvs != null)
        {
            foreach (var lhnv in lhnvs)
            {
                var _num = _canBoRepository.Select().Where(p => p.LinhVucNC != null && p.LinhVucNC.Contains(lhnv.Id.ToString())).Count();
                dics.Add(lhnv.Ten, _num.ToString());
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất thống kê cán bộ",
            Data = dics
        });
    }

    [HttpGet("doanhnghiep")]
    public async Task<IActionResult> GetBieuDoDoanhNghiep([FromQuery] BieuDoFilter model)
    {
        var lhnvs = await _linhVucNghienCuuRepository.Select().Where(P => P.TrangThai == 1).ToListAsync();
        Dictionary<String, String> dics = new Dictionary<String, String>();
        if (lhnvs != null)
        {
            foreach (var lhnv in lhnvs)
            {
                var _num = _doanhNghiepepository.Select().Where(p => p.LinhVucNghienCuu != null && p.LinhVucNghienCuu.Contains(lhnv.Id.ToString())).Count();
                dics.Add(lhnv.Ten, _num.ToString());
            }
        }
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất thống kê doanh nghiệp",
            Data = dics
        });
    }
}