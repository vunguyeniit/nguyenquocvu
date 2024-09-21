using System.Security.Claims;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using System.IO;
using Asp.Versioning;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Repositories.CommonCategories;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/danhmuc")]
[ApiVersion("1")]
public class DanhMucController : BaseController
{
    private readonly ICommonRepository _repo;
    public DanhMucController(ICommonRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) 
    { _repo = repository; }
    #region Tham quyen thanh lap

    [HttpGet("thamquyenthanhlap")]
    public async Task<IActionResult> GetTQTLs([FromQuery] ThamQuyenThanhLapFilter model)
    {
        var (items, records) = await _repo.GetThamQuyenThanhLaps(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thẩm quyền thành lập thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("thamquyenthanhlap/{id:long}")]
    public async Task<IActionResult> GetTQTL(long id)
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
        var item = await _repo.GetThamQuyenThanhLap(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thẩm quyền thành lập thành công!",
            Data = item
        });
    }

    [HttpPost("thamquyenthanhlap")]
    public async Task<IActionResult> TaoTQTL([FromBody] ThamQuyenThanhLapDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoThamQuyenThanhLap(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thẩm quyền thành lập thành công!",
            Data = item
        });
    }

    [HttpPut("thamquyenthanhlap/{id:long}")]
    public async Task<IActionResult> CapNhatTQTL(long id, [FromBody] ThamQuyenThanhLapDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatThamQuyenThanhLap(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thẩm quyền thành lập thành công!"
        });
    }

    [HttpDelete("thamquyenthanhlap/{id:long}")]
    public async Task<IActionResult> XoaTQTL(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaThamQuyenThanhLap(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá thẩm quyền thành lập thành công."
        });
    }

    #endregion

    #region HinhThucChuyenGiao

    [HttpGet("hinhthucchuyengiao")]
    public async Task<IActionResult> GetHinhThucChuyenGiaos([FromQuery] HinhThucChuyenGiaoFilter model)
    {
        var (items, records) = await _repo.GetHinhThucChuyenGiaos(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách hình thức chuyển giao thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("hinhthucchuyengiao/{id:long}")]
    public async Task<IActionResult> GetHinhThucChuyenGiao(long id)
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
        var item = await _repo.GetHinhThucChuyenGiao(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất hình thức chuyển giao thành công!",
            Data = item
        });
    }

    [HttpPost("hinhthucchuyengiao")]
    public async Task<IActionResult> TaoHinhThucChuyenGiao([FromBody] HinhThucChuyenGiaoDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoHinhThucChuyenGiao(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới hình thức chuyển giao thành công!",
            Data = item
        });
    }

    [HttpPut("hinhthucchuyengiao/{id:long}")]
    public async Task<IActionResult> CapNhatHinhThucChuyenGiao(long id, [FromBody] HinhThucChuyenGiaoDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatHinhThucChuyenGiao(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật hình thức chuyển giao thành công!"
        });
    }

    [HttpDelete("hinhthucchuyengiao/{id:long}")]
    public async Task<IActionResult> XoaHinhThucChuyenGiao(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaHinhThucChuyenGiao(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá hình thức chuyển giao thành công."
        });
    }

    #endregion

    #region HinhThucThanhLap

    [HttpGet("hinhthucthanhlap")]
    public async Task<IActionResult> GetHinhThucThanhLaps([FromQuery] HinhThucThanhLapFilter model)
    {
        var (items, records) = await _repo.GetHinhThucThanhLaps(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách hình thức thành lập thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("hinhthucthanhlap/{id:long}")]
    public async Task<IActionResult> GetHinhThucThanhLap(long id)
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
        var item = await _repo.GetHinhThucThanhLap(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất hình thức thành lập thành công!",
            Data = item
        });
    }

    [HttpPost("hinhthucthanhlap")]
    public async Task<IActionResult> TaoHinhThucThanhLap([FromBody] HinhThucThanhLapDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoHinhThucThanhLap(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới hình thức thành lập thành công!",
            Data = item
        });
    }

    [HttpPut("hinhthucthanhlap/{id:long}")]
    public async Task<IActionResult> CapNhatHinhThucThanhLap(long id, [FromBody] HinhThucThanhLapDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatHinhThucThanhLap(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật hình thức thành lập thành công!"
        });
    }

    [HttpDelete("hinhthucthanhlap/{id:long}")]
    public async Task<IActionResult> XoaHinhThucThanhLap(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaHinhThucThanhLap(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá hình thức thành lập thành công."
        });
    }

    #endregion

    #region LinhVucDaoTaoKHCN
   

    [HttpGet("linhvucdaotaokhcn")]
    public async Task<IActionResult> GetLinhVucDaoTaoKHCNs([FromQuery] LinhVucDaoTaoKHCNFilter model)
    {
        var (items, records) = await _repo.GetLinhVucDaoTaoKHCNs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách lĩnh vực đào tạo KHCN thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvucdaotaokhcn/{id:long}")]
    public async Task<IActionResult> GetLinhVucDaoTaoKHCN(long id)
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
        var item = await _repo.GetLinhVucDaoTaoKHCN(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất lĩnh vực đào tạo KHCN  thành công!",
            Data = item
        });
    }

    [HttpPost("linhvucdaotaokhcn")]
    public async Task<IActionResult> TaoLinhVucDaoTaoKHCN([FromBody] LinhVucDaoTaoKHCNDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucDaoTaoKHCN(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới lĩnh vực đào tạo KHCN  thành công!",
            Data = item
        });
    }

    [HttpPut("linhvucdaotaokhcn/{id:long}")]
    public async Task<IActionResult> CapNhatLinhVucDaoTaoKHCN(long id, [FromBody] LinhVucDaoTaoKHCNDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucDaoTaoKHCN(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật lĩnh vực đào tạo KHCN  thành công!"
        });
    }

    [HttpDelete("linhvucdaotaokhcn/{id:long}")]
    public async Task<IActionResult> XoaLinhVucDaoTaoKHCN(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucDaoTaoKHCN(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá lĩnh vực đào tạo KHCN  thành công."
        });
    }

    #endregion

    #region LinhVucKHCN
 
    [HttpGet("linhvuckhcn")]
    public async Task<IActionResult> GetLinhVucKHCNs([FromQuery] LinhVucKHCNFilter model)
    {
        var (items, records) = await _repo.GetLinhVucKHCNs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách lĩnh vực KHCN thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvuckhcn/{id:long}")]
    public async Task<IActionResult> GetLinhVucKHCN(long id)
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
        var item = await _repo.GetLinhVucKHCN(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất lĩnh vực KHCN thành công!",
            Data = item
        });
    }

    [HttpPost("linhvuckhcn")]
    public async Task<IActionResult> TaoLinhVucKHCN([FromBody] LinhVucKHCNDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucKHCN(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới lĩnh vực KHCN thành công!",
            Data = item
        });
    }

    [HttpPut("linhvuckhcn/{id:long}")]
    public async Task<IActionResult> CapNhatLinhVucKHCN(long id, [FromBody] LinhVucKHCNDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucKHCN(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật lĩnh vực KHCN thành công!"
        });
    }

    [HttpDelete("linhvuckhcn/{id:long}")]
    public async Task<IActionResult> XoaLinhVucKHCN(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucKHCN(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá lĩnh vực KHCN thành công."
        });
    }
    #endregion

    #region LoaiHinhToChucDN

    [HttpGet("loaihinhtochucdn")]
    public async Task<IActionResult> GetLoaiHinhToChucDNs([FromQuery] LoaiHinhToChucDNFilter model)
    {
        var (items, records) = await _repo.GetLoaiHinhToChucDNs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách loại hình tổ chức DN thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("loaihinhtochucdn/{id:long}")]
    public async Task<IActionResult> GetLoaiHinhToChucDN(long id)
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
        var item = await _repo.GetLoaiHinhToChucDN(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất loại hình tổ chức DN thành công!",
            Data = item
        });
    }

    [HttpPost("loaihinhtochucdn")]
    public async Task<IActionResult> TaoLoaiHinhToChucDN([FromBody] LoaiHinhToChucDNDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLoaiHinhToChucDN(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới loại hình tổ chức DN thành công!",
            Data = item
        });
    }

    [HttpPut("loaihinhtochucdn/{id:long}")]
    public async Task<IActionResult> CapNhatLoaiHinhToChucDN(long id, [FromBody] LoaiHinhToChucDNDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLoaiHinhToChucDN(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật loại hình tổ chức DN thành công!"
        });
    }

    [HttpDelete("loaihinhtochucdn/{id:long}")]
    public async Task<IActionResult> XoaLoaiHinhToChucDN(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLoaiHinhToChucDN(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá loại hình tổ chức DN thành công."
        });
    }

    #endregion

    #region MucTieuKTXH



    [HttpGet("muctieuktxh")]
    public async Task<IActionResult> GetMucTieuKTXHs([FromQuery] MucTieuKTXHFilter model)
    {
        var (items, records) = await _repo.GetMucTieuKTXHs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách mục tiêu KTXH thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("muctieuktxh/{id:long}")]
    public async Task<IActionResult> GetMucTieuKTXH(long id)
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
        var item = await _repo.GetMucTieuKTXH(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất mục tiêu KTXH thành công!",
            Data = item
        });
    }

    [HttpPost("muctieuktxh")]
    public async Task<IActionResult> TaoMucTieuKTXH([FromBody] MucTieuKTXHDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoMucTieuKTXH(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới mục tiêu KTXH thành công!",
            Data = item
        });
    }

    [HttpPut("muctieuktxh/{id:long}")]
    public async Task<IActionResult> CapNhatMucTieuKTXH(long id, [FromBody] MucTieuKTXHDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatMucTieuKTXH(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật mục tiêu KTXH thành công!"
        });
    }

    [HttpDelete("muctieuktxh/{id:long}")]
    public async Task<IActionResult> XoaMucTieuKTXH(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaMucTieuKTXH(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá mục tiêu KTXH thành công."
        });
    }

    #endregion

    #region QuyChuanKT


    [HttpGet("quychuankt")]
    public async Task<IActionResult> GetQuyChuanKTs([FromQuery]QuyChuanKTFilter model)
    {
        var (items, records) = await _repo.GetQuyChuanKTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách quy chuẩn KT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("quychuankt/{id:long}")]
    public async Task<IActionResult> GetQuyChuanKT(long id)
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
        var item = await _repo.GetQuyChuanKT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất quy chuẩn KT thành công!",
            Data = item
        });
    }

    [HttpPost("quychuankt")]
    public async Task<IActionResult> TaoQuyChuanKT([FromBody]QuyChuanKTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoQuyChuanKT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới  quy chuẩn KT thành công!",
            Data = item
        });
    }

    [HttpPut("quychuankt/{id:long}")]
    public async Task<IActionResult> CapNhatQuyChuanKT(long id, [FromBody] QuyChuanKTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatQuyChuanKT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật  quy chuẩn KT thành công!"
        });
    }

    [HttpDelete("quychuankt/{id:long}")]
    public async Task<IActionResult> XoaQuyChuanKT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaQuyChuanKT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá quy chuẩn KT thành công."
        });
    }
    #endregion

    #region NguonCapKinhPhi

    [HttpGet("nguoncapkinhphi")]
    public async Task<IActionResult> GetNguonCapKinhPhis([FromQuery] NguonCapKinhPhiFilter model)
    {
        var (items, records) = await _repo.GetNguonCapKinhPhis(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách nguồn cấp kinh phí thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("nguoncapkinhphi/{id:long}")]
    public async Task<IActionResult> GetNguonCapKinhPhi(long id)
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
        var item = await _repo.GetNguonCapKinhPhi(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất nguồn cấp kinh phí thành công!",
            Data = item
        });
    }

    [HttpPost("nguoncapkinhphi")]
    public async Task<IActionResult> TaoNguonCapKinhPhi([FromBody] NguonCapKinhPhiDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoNguonCapKinhPhi(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới  nguồn cấp kinh phí thành công!",
            Data = item
        });
    }

    [HttpPut("nguoncapkinhphi/{id:long}")]
    public async Task<IActionResult> CapNhatNguonCapKinhPhi(long id, [FromBody]NguonCapKinhPhiDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatNguonCapKinhPhi(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật  nguồn cấp kinh phí thành công!"
        });
    }
    
    [HttpDelete("nguoncapkinhphi/{id:long}")]
    public async Task<IActionResult> XoaNguonCapKinhPhi(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaNguonCapKinhPhi(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá nguồn cấp kinh phí thành công."
        });
    }
    #endregion

    #region DoiTacQT

    [HttpGet("doitacqt")]
    public async Task<IActionResult> GetDoiTacQTs([FromQuery] DoiTacQTFilter model)
    {
        var (items, records) = await _repo.GetDoiTacQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách đối tác quốc tế thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("doitacqt/{id:long}")]
    public async Task<IActionResult> GetDoiTacQT(long id)
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
        var item = await _repo.GetDoiTacQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất  đối tác quốc tế thành công!",
            Data = item
        });
    }

    [HttpPost("doitacqt")]
    public async Task<IActionResult> TaoDoiTacQT([FromBody] DoiTacQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoDoiTacQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới   đối tác quốc tế thành công!",
            Data = item
        });
    }

    [HttpPut("doitacqt/{id:long}")]
    public async Task<IActionResult> CapNhatDoiTacQT(long id, [FromBody] DoiTacQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatDoiTacQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật đối tác quốc tế thành công!"
        });
    }

    [HttpDelete("doitacqt/{id:long}")]
    public async Task<IActionResult> XoaDoiTacQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaDoiTacQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá đối tác quốc tế thành công."
        });
    }
    #endregion

    #region  HinhThucHTQT

    [HttpGet("hinhthuchtqt")]
    public async Task<IActionResult> GetHinhThucHTQTs([FromQuery] HinhThucHTQTFilter model)
    {
        var (items, records) = await _repo.GetHinhThucHTQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách hình thức HTQT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("hinhthuchtqt/{id:long}")]
    public async Task<IActionResult> GetHinhThucHTQT(long id)
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
        var item = await _repo.GetHinhThucHTQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất hình thức HTQT thành công!",
            Data = item
        });
    }

    [HttpPost("hinhthuchtqt")]
    public async Task<IActionResult> TaoHinhThucHTQT([FromBody] HinhThucHTQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoHinhThucHTQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới hình thức HTQT thành công!",
            Data = item
        });
    }

    [HttpPut("hinhthuchtqt/{id:long}")]
    public async Task<IActionResult> CapNhatHinhThucHTQT(long id, [FromBody] HinhThucHTQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatHinhThucHTQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật hình thức HTQT thành công!"
        });
    }

    [HttpDelete("hinhthuchtqt/{id:long}")]
    public async Task<IActionResult> XoaHinhThucHTQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaHinhThucHTQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá hình thức HTQT thành công."
        });
    }
    #endregion

    #region NguonKPHTQT 
    [HttpGet("nguonkphtqt")]
    public async Task<IActionResult> GetNguonKPHTQTs([FromQuery] NguonKPHTQTFilter model)
    {
        var (items, records) = await _repo.GetNguonKPHTQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách NguonKPHTQT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("nguonkphtqt/{id:long}")]
    public async Task<IActionResult> GetNguonKPHTQT(long id)
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
        var item = await _repo.GetNguonKPHTQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất NguonKPHTQT thành công!",
            Data = item
        });
    }

    [HttpPost("nguonkphtqt")]
    public async Task<IActionResult> TaoNguonKPHTQT([FromBody] NguonKPHTQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoNguonKPHTQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới NguonKPHTQT thành công!",
            Data = item
        });
    }

    [HttpPut("nguonkphtqt/{id:long}")]
    public async Task<IActionResult> CapNhatNguonKPHTQT(long id, [FromBody] NguonKPHTQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatNguonKPHTQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật NguonKPHTQT thành công!"
        });
    }

    [HttpDelete("nguonkphtqt/{id:long}")]
    public async Task<IActionResult> XoaNguonKPHTQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaNguonKPHTQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá NguonKPHTQT thành công."
        });
    }
    #endregion

    #region  LinhVucNCHTQT

    [HttpGet("linhvucnchtqt")]
    public async Task<IActionResult> GetLinhVucNCHTQTs([FromQuery] LinhVucNCHTQTFilter model)
    {
        var (items, records) = await _repo.GetLinhVucNCHTQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách LinhVucNCHTQT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvucnchtqt/{id:long}")]
    public async Task<IActionResult> GetLinhVucNCHTQT(long id)
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
        var item = await _repo.GetLinhVucNCHTQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất LinhVucNCHTQT thành công!",
            Data = item
        });
    }

    [HttpPost("linhvucnchtqt")]
    public async Task<IActionResult> TaoLinhVucNCHTQT([FromBody] LinhVucNCHTQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucNCHTQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới LinhVucNCHTQT thành công!",
            Data = item
        });
    }

    [HttpPut("linhvucnchtqt/{id:long}")]
    public async Task<IActionResult> CapNhatLinhVucNCHTQT(long id, [FromBody] LinhVucNCHTQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucNCHTQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật LinhVucNCHTQT thành công!"
        });
    }

    [HttpDelete("linhvucnchtqt/{id:long}")]
    public async Task<IActionResult> XoaLinhVucNCHTQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucNCHTQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá LinhVucNCHTQT thành công."
        });
    }
    #endregion

    #region  LinhVucUngDung 
    [HttpGet("linhvucungdung")]
    public async Task<IActionResult> GetLinhVucUngDungs([FromQuery]LinhVucUngDungFilter model)
    {
        var (items, records) = await _repo.GetLinhVucUngDungs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách LinhVucUngDung thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvucungdung/{id:long}")]
    public async Task<IActionResult> GetLinhVucUngDung(long id)
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
        var item = await _repo.GetLinhVucUngDung(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất LinhVucNCHTQT thành công!",
            Data = item
        });
    }

    [HttpPost("linhvucungdung")]
    public async Task<IActionResult> TaoLinhVucUngDung([FromBody] LinhVucUngDungDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucUngDung(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới LinhVucUngDung thành công!",
            Data = item
        });
    }

    [HttpPut("linhvucungdung/{id:long}")]
    public async Task<IActionResult> CapNhatLinhVucUngDung(long id, [FromBody] LinhVucUngDungDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucUngDung(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật LinhVucUngDung thành công!"
        });
    }

    [HttpDelete("linhvucungdung/{id:long}")]
    public async Task<IActionResult> XoaLinhVucUngDung(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucUngDung(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá LinhVucUngDung thành công."
        });
    }
    #endregion

    #region MauPhuongTienDo
    [HttpGet("mauphuongtiendo")]
    public async Task<IActionResult> GetMauPhuongTienDos([FromQuery] MauPhuongTienDoFilter model)
    {
        var (items, records) = await _repo.GetMauPhuongTienDos(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách MauPhuongTienDo thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("mauphuongtiendo/{id:long}")]
    public async Task<IActionResult> GetMauPhuongTienDo(long id)
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
        var item = await _repo.GetMauPhuongTienDo(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất MauPhuongTienDo thành công!",
            Data = item
        });
    }

    [HttpPost("mauphuongtiendo")]
    public async Task<IActionResult> TaoMauPhuongTienDo([FromBody]MauPhuongTienDoDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoMauPhuongTienDo(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới MauPhuongTienDo thành công!",
            Data = item
        });
    }

    [HttpPut("mauphuongtiendo/{id:long}")]
    public async Task<IActionResult> CapNhatMauPhuongTienDo(long id, [FromBody] MauPhuongTienDoDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatMauPhuongTienDo(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật MauPhuongTienDo thành công!"
        });
    }

    [HttpDelete("mauphuongtiendo/{id:long}")]
    public async Task<IActionResult> XoaMauPhuongTienDo(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaMauPhuongTienDo(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá MauPhuongTienDo thành công."
        });
    }
    #endregion

    #region ChucDanh
    [HttpGet("chucdanh")]
    public async Task<IActionResult> GetChucDanhs([FromQuery] ChucDanhFilter model)
    {
        var (items, records) = await _repo.GetChucDanhs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách chức danh thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("chucdanh/{id:long}")]
    public async Task<IActionResult> GetChucDanh(long id)
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
        var item = await _repo.GetChucDanh(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất chức danh thành công!",
            Data = item
        });
    }

    [HttpPost("chucdanh")]
    public async Task<IActionResult> TaoChucDanh([FromBody] ChucDanhDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoChucDanh(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới chức danh thành công!",
            Data = item
        });
    }

    [HttpPut("chucdanh/{id:long}")]
    public async Task<IActionResult> CapNhatChucDanh(long id, [FromBody] ChucDanhDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatChucDanh(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật chức danh thành công!"
        });
    }

    [HttpDelete("chucdanh/{id:long}")]
    public async Task<IActionResult> XoaChucDanh(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaChucDanh(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá chức danh thành công."
        });
    }
    #endregion

    #region QuocTich
    [HttpGet("quoctich")]
    public async Task<IActionResult> GetQuocTichs([FromQuery] QuocTichFilter model)
    {
        var (items, records) = await _repo.GetQuocTichs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách quốc tịch thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("quoctich/{id:long}")]
    public async Task<IActionResult> GetQuocTich(long id)
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
        var item = await _repo.GetQuocTich(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất quốc tịch thành công!",
            Data = item
        });
    }

    [HttpPost("quoctich")]
    public async Task<IActionResult> TaoQuocTich([FromBody] QuocTichDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoQuocTich(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới quốc tịch thành công!",
            Data = item
        });
    }

    [HttpPut("quoctich/{id:long}")]
    public async Task<IActionResult> CapNhatQuocTich(long id, [FromBody] QuocTichDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatQuocTich(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật quốc tịch thành công!"
        });
    }
    [HttpDelete("quoctich/{id:long}")]
    public async Task<IActionResult> XoaQuocTich(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaQuocTich(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá quốc tịch thành công."
        });
    }
    #endregion

    #region TrinhDoChuyenMon
    [HttpGet("trinhdochuyenmon")]
    public async Task<IActionResult> GetTrinhDoChuyenMons([FromQuery] TrinhDoChuyenMonFilter model)
    {
        var (items, records) = await _repo.GetTrinhDoChuyenMons(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách trình độ chuyên môn thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("trinhdochuyenmon/{id:long}")]
    public async Task<IActionResult> GetTrinhDoChuyenMon(long id)
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
        var item = await _repo.GetTrinhDoChuyenMon(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất trình độ chuyên môn thành công!",
            Data = item
        });
    }

    [HttpPost("trinhdochuyenmon")]
    public async Task<IActionResult> TaoTrinhDoChuyenMon([FromBody] TrinhDoChuyenMonDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoTrinhDoChuyenMon(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới trình độ chuyên môn thành công!",
            Data = item
        });
    }

    [HttpPut("trinhdochuyenmon/{id:long}")]
    public async Task<IActionResult> CapNhatTrinhDoChuyenMon(long id, [FromBody] TrinhDoChuyenMonDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatTrinhDoChuyenMon(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật trình độ chuyên môn thành công!"
        });
    }

    [HttpDelete("trinhdochuyenmon/{id:long}")]
    public async Task<IActionResult> XoaTrinhDoChuyenMon(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaTrinhDoChuyenMon(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá trình độ chuyên môn thành công."
        });
    }
    #endregion

    #region LoaiHinhNhiemVu
    [HttpGet("loaihinhnhiemvu")]
    public async Task<IActionResult> GetLoaiHinhNhiemVus([FromQuery] LoaiHinhNhiemVuFilter model)
    {
        var (items, records) = await _repo.GetLoaiHinhNhiemVus(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách trình độ chuyên môn thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("loaihinhnhiemvu/{id:long}")]
    public async Task<IActionResult> GetLoaiHinhNhiemVu(long id)
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
        var item = await _repo.GetLoaiHinhNhiemVu(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất trình độ chuyên môn thành công!",
            Data = item
        });
    }

    [HttpPost("loaihinhnhiemvu")]
    public async Task<IActionResult> TaoLoaiHinhNhiemVu([FromBody] LoaiHinhNhiemVuDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLoaiHinhNhiemVu(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới trình độ chuyên môn thành công!",
            Data = item
        });
    }

    [HttpPut("loaihinhnhiemvu/{id:long}")]
    public async Task<IActionResult> CapNhatLoaiHinhNhiemVu(long id, [FromBody] LoaiHinhNhiemVuDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLoaiHinhNhiemVu(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật trình độ chuyên môn thành công!"
        });
    }

    [HttpDelete("loaihinhnhiemvu/{id:long}")]
    public async Task<IActionResult> XoaLoaiHinhNhiemVu(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLoaiHinhNhiemVu(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá trình độ chuyên môn thành công."
        });
    }
    #endregion

    #region CapQuanLy
    [HttpGet("capquanly")]
    public async Task<IActionResult> GetCapQuanLys([FromQuery] CapQuanLyFilter model)
    {
        var (items, records) = await _repo.GetCapQuanLys(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách cấp quản lý môn thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("capquanly/{id:long}")]
    public async Task<IActionResult> GetCapQuanLy(long id)
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
        var item = await _repo.GetCapQuanLy(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất cấp quản lý môn thành công!",
            Data = item
        });
    }

    [HttpPost("capquanly")]
    public async Task<IActionResult> TaoCapQuanLy([FromBody] CapQuanLyDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoCapQuanLy(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới cấp quản lý thành công!",
            Data = item
        });
    }

    [HttpPut("capquanly/{id:long}")]
    public async Task<IActionResult> CapCapQuanLy(long id, [FromBody] CapQuanLyDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatCapQuanLy(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật cấp quản lý thành công!"
        });
    }

    [HttpDelete("capquanly/{id:long}")]
    public async Task<IActionResult> XoaCapQuanLy(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaCapQuanLy(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá cấp quản lý thành công."
        });
    }
    #endregion

    #region  LinhVucNghienCuu
    [HttpGet("linhvucnghiencuu")]
    public async Task<IActionResult> GetLinhVucNghienCuus([FromQuery] LinhVucNghienCuuFilter model)
    {
        var (items, records) = await _repo.GetLinhVucNghienCuus(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách lĩnh vực nghiên cứu  thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvucnghiencuu/{id:long}")]
    public async Task<IActionResult> GetLinhVucNghienCuu(long id)
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
        var item = await _repo.GetLinhVucNghienCuu(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất lĩnh vực nghiên cứu  thành công!",
            Data = item
        });
    }

    [HttpPost("linhvucnghiencuu")]
    public async Task<IActionResult> TaoLinhVucNghienCuu([FromBody] LinhVucNghienCuuDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucNghienCuu(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới lĩnh vực nghiên cứu thành công!",
            Data = item
        });
    }

    [HttpPut("linhvucnghiencuu/{id:long}")]
    public async Task<IActionResult> CapLinhVucNghienCuu(long id, [FromBody] LinhVucNghienCuuDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucNghienCuu(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật lĩnh vực nghiên cứu thành công!"
        });
    }

    [HttpDelete("linhvucnghiencuu/{id:long}")]
    public async Task<IActionResult> XoaLinhVucNghienCuu(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucNghienCuu(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá lĩnh vực nghiên cứu thành công."
        });
    }
    #endregion

    #region  CapQuanLyHTQT
    [HttpGet("capquanlyhtqt")]
    public async Task<IActionResult> GetCapQuanLyHTQTs([FromQuery] CapQuanLyHTQTFilter model)
    {
        var (items, records) = await _repo.GetCapQuanLyHTQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách cấp quản lý HTQT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("capquanlyhtqt/{id:long}")]
    public async Task<IActionResult> GetCapQuanLyHTQT(long id)
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
        var item = await _repo.GetCapQuanLyHTQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất cấp quản lý HTQT thành công!",
            Data = item
        });
    }

    [HttpPost("capquanlyhtqt")]
    public async Task<IActionResult> TaoCapQuanLyHTQT([FromBody] CapQuanLyHTQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoCapQuanLyHTQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới cấp quản lý HTQT thành công!",
            Data = item
        });
    }

    [HttpPut("capquanlyhtqt/{id:long}")]
    public async Task<IActionResult> CapCapQuanLyHTQT(long id, [FromBody] CapQuanLyHTQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatCapQuanLyHTQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật cấp quản lý HTQT thành công!"
        });
    }

    [HttpDelete("capquanlyhtqt/{id:long}")]
    public async Task<IActionResult> XoaCapQuanLyHTQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaCapQuanLyHTQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá cấp quản lý HTQT thành công."
        });
    }
    #endregion

    #region  LinhVucNghienCuuTTQT
    [HttpGet("linhvucnghiencuuttqt")]
    public async Task<IActionResult> GetLinhVucNghienCuuTTQTs([FromQuery] LinhVucNghienCuuTTQTFilter model)
    {
        var (items, records) = await _repo.GetLinhVucNghienCuuTTQTs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách lĩnh vực nghiên cứu TTQT thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("linhvucnghiencuuttqt/{id:long}")]
    public async Task<IActionResult> GetLinhVucNghienCuuTTQT(long id)
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
        var item = await _repo.GetLinhVucNghienCuuTTQT(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất lĩnh vực nghiên cứu TTQT thành công!",
            Data = item
        });
    }

    [HttpPost("linhvucnghiencuuttqt")]
    public async Task<IActionResult> TaoLinhVucNghienCuuTTQT([FromBody] LinhVucNghienCuuTTQTDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLinhVucNghienCuuTTQT(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới lĩnh vực nghiên cứu TTQT thành công!",
            Data = item
        });
    }

    [HttpPut("linhvucnghiencuuttqt/{id:long}")]
    public async Task<IActionResult> CapLinhVucNghienCuuTTQT(long id, [FromBody] LinhVucNghienCuuTTQTDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLinhVucNghienCuuTTQT(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật lĩnh vực nghiên cứu TTQT thành công!"
        });
    }

    [HttpDelete("linhvucnghiencuuttqt/{id:long}")]
    public async Task<IActionResult> XoaLinhVucNghienCuuTTQT(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLinhVucNghienCuuTTQT(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá lĩnh vực nghiên cứu TTQT thành công."
        });
    }
    #endregion

    #region  NguonCapKPEnum
    [HttpGet("nguoncapkpenum")]
    public async Task<IActionResult> GetNguonCapKPEnums([FromQuery] NguonCapKPEnumFilter model)
    {
        var (items, records) = await _repo.GetNguonCapKPEnums(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách nguồn cấp KPEnum thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("nguoncapkpenum/{id:long}")]
    public async Task<IActionResult> GetNguonCapKPEnum(long id)
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
        var item = await _repo.GetNguonCapKPEnum(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất nguồn cấp KPEnum thành công!",
            Data = item
        });
    }

    [HttpPost("nguoncapkpenum")]
    public async Task<IActionResult> TaoNguonCapKPEnum([FromBody] NguonCapKPEnumDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoNguonCapKPEnum(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới nguồn cấp KPEnum thành công!",
            Data = item
        });
    }

    [HttpPut("nguoncapkpenum/{id:long}")]
    public async Task<IActionResult> CapNguonCapKPEnum(long id, [FromBody] NguonCapKPEnumDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatNguonCapKPEnum(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật lnguồn cấp KPEnum thành công!"
        });
    }

    [HttpDelete("nguoncapkpenum/{id:long}")]
    public async Task<IActionResult> XoaNguonCapKPEnum(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaNguonCapKPEnum(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá nguồn cấp KPEnum thành công."
        });
    }
    #endregion

    #region  LoaiHinhToChuc
    [HttpGet("loaihinhtochuc")]
    public async Task<IActionResult> GetLoaiHinhToChucs([FromQuery] LoaiHinhToChucFilter model)
    {
        var (items, records) = await _repo.GetLoaiHinhToChucs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách loại hình tổ chức thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("loaihinhtochuc/{id:long}")]
    public async Task<IActionResult> GetLoaiHinhToChuc(long id)
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
        var item = await _repo.GetLoaiHinhToChuc(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất loại hình tổ chức thành công!",
            Data = item
        });
    }

    [HttpPost("loaihinhtochuc")]
    public async Task<IActionResult> TaoLoaiHinhToChuc([FromBody] LoaiHinhToChucDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.TaoLoaiHinhToChuc(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới loại hình tổ chức thành công!",
            Data = item
        });
    }

    [HttpPut("loaihinhtochuc/{id:long}")]
    public async Task<IActionResult> CapLoaiHinhToChuc(long id, [FromBody] LoaiHinhToChucDto model)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CapNhatLoaiHinhToChuc(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật loại hình tổ chức thành công!"
        });
    }

    [HttpDelete("loaihinhtochuc/{id:long}")]
    public async Task<IActionResult> XoaLoaiHinhToChuc(long id)
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
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.XoaLoaiHinhToChuc(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá loại hình tổ chức thành công."
        });
    }
    #endregion
}
