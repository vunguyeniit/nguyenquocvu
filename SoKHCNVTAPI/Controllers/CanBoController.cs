using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using SoKHCNVTAPI.Migrations;
namespace SoKHCNVTAPI.Controllers;
/// <summary>
/// Cán bộ
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/canbo")]
[ApiVersion("1")]
public class CanBoController : BaseController
{
    private readonly ICanBoRepository _canboRepository;
    string module = "Cán bộ";

    //
    public CanBoController(ICanBoRepository officerRepository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _canboRepository = officerRepository;   }

    #region CanBo
    [HttpGet("danhsach")]
    public async Task<IActionResult> GetAll([FromQuery] CanBoFilter model)
    {
        //if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        var (items, records) = await _canboRepository.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        //if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _canboRepository.GetByIdAsync(id);
        if (item == null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không tìm thấy",
                Success = false,
                ErrorCode = 2,
            });
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất tổ chức thành công!",
            Data = item
        });
    }
 
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CanBoDto model)
    {
        if (!await Can("Thêm cán bộ", module)) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _canboRepository.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] CanBoDto model)
    {
        if (!await Can("Cập nhật cán bộ", module)) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        //if (!await Can("Cập nhật cán bộ", "cán bộ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _canboRepository.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật cán bộ thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        if (!await Can("Xóa cán bộ", module)) return PermissionMessage();
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
        await _canboRepository.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá cán bộ thành công."
        });
    }
    #endregion

    #region OfficerPublication

    [HttpPost("congbokhoahoc")]
    public async Task<IActionResult> CreatePublication([FromBody] CongBoKHCNDto model)
    {
        if (!await Can("Thêm cán bộ", "Cán bộ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _canboRepository.CreatePublicationAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }
    [HttpPut("congbokhoahoc/{id:long}")]
    public async Task<IActionResult> UpdatePublication(long id, [FromBody] CongBoKHCNDto model)
    {
        if (!await Can("Cập nhật cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.UpdatePublicationAsync(id, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }
    [HttpDelete("congbokhoahoc/{id:long}")]
    public async Task<IActionResult> DeletePublication(long id)
    {
        if (!await Can("Xóa cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.DeletePublicationAsync(id, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }

    [HttpGet("congbokhoahoc")]
    public async Task<IActionResult> GetPublications(string? keyword, [FromQuery] CongBoKHCNFilter model)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        var (items, records) = await _canboRepository.FilterPublicationAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("congbokhoahoc/{id:long}")]
    public async Task<IActionResult> GetPublication(long id)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _canboRepository.GetPublicationByIdAsync(id);
        if (item == null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không tìm thấy",
                Success = false,
                ErrorCode = 2,
            });
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }
    #endregion

    #region HocVanCanBo

    [HttpGet("hocvancanbo")]
    public async Task<IActionResult> GetHocVanCanBos([FromQuery] HocVanCanBoFilter model)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        var (items, records) = await _canboRepository.HocVanCanBoPagingAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("hocvancanbo/{id:long}")]
    public async Task<IActionResult> GetHocVanCanBo(long id)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _canboRepository.GetHocVanCanBo(id);
        if (item == null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không tìm thấy",
                Success = false,
                ErrorCode = 2,
            });
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("hocvancanbo")]
    public async Task<IActionResult> Create([FromBody] HocVanCanBoDto model)
    {
        if (!await Can("Thêm cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _canboRepository.TaoHocVanCanBo(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("hocvancanbo/{id:long}")]
    public async Task<IActionResult> CapNhatHocVanCanBo(long id, [FromBody] HocVanCanBoDto model)
    {
        if (!await Can("Cập nhật cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.CapNhatHocVanCanBo(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!",
        });
    }

    [HttpDelete("hocvancanbo/{id:long}")]
    public async Task<IActionResult> XoaHocVanCanBo(long id)
    {
        if (!await Can("Xóa cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.XoaHocVanCanBo(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
    #endregion

    #region NhiemVuCanBo
    [HttpGet("nhiemvucanbo")]
    public async Task<IActionResult> GetNhiemVuCanBos([FromQuery] NhiemVuCanBoFilter model)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        var (items, records) = await _canboRepository.NhiemVuCanBoPagingAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách nhiệm vụ thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("nhiemvucanbo/{id:long}")]
    public async Task<IActionResult> GetNhiemVuCanBo(long id)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _canboRepository.GetNhiemVuCanBo(id);
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("nhiemvucanbo")]
    public async Task<IActionResult> Create([FromBody] NhiemVuCanBoDto model)
    {
        if (!await Can("Thêm cán bộ", "Cán bộ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _canboRepository.TaoNhiemVuCanBo(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("nhiemvucanbo/{id:long}")]
    public async Task<IActionResult> CapNhatNhiemVuCanBo(long id, [FromBody] NhiemVuCanBoDto model)
    {
        if (!await Can("Cập nhật cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.CapNhatNhiemVuCanBo(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!",
        });
    }

    [HttpDelete("nhiemvucanbo/{id:long}")]
    public async Task<IActionResult> XoaNhiemVuCanBo(long id)
    {
        if (!await Can("Xóa cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.XoaNhiemVuCanBo(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
    #endregion

    #region CanBoCongTac
    [HttpGet("canbocongtac")]
    public async Task<IActionResult> GetAll([FromQuery] CanBoCongTacFilter model)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        var (items, records) = await _canboRepository.CanBoCongTacPagingAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("canbocongtac/{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        if (!await Can("Xem cán bộ", "Cán bộ")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _canboRepository.GetCanBoCongTac(id);
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("canbocongtac")]
    public async Task<IActionResult> Create([FromBody] CanBoCongTacDto model)
    {
        if (!await Can("Thêm cán bộ", "Cán bộ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _canboRepository.TaoCanBoCongTac(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("canbocongtac/{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] CanBoCongTacDto model)
    {
        if (!await Can("Cập nhật cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.CapNhatCanBoCongTac(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!",
        });
    }

    [HttpDelete("canbocongtac/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!await Can("Xóa cán bộ", "Cán bộ")) return PermissionMessage();
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
        await _canboRepository.XoaCanBoCongTac(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
    #endregion 

}