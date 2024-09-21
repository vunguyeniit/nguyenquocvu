using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories.CommonCategories;
using Asp.Versioning;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Repositories;
namespace SoKHCNVTAPI.Controllers;

/// <summary>
/// Định danh chuyên gia
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/chuyengia")]
[ApiVersion("1")]
public class ChuyenGiaController : BaseController
{
    private readonly IChuyenGiaRepository _repo;
    string module = "Chuyên gia";
    public ChuyenGiaController(IChuyenGiaRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetChuyenGias([FromQuery] ExpertFilter model)
    {
       // if (!await Can("Xem chuyên gia", "Chuyên gia")) return PermissionMessage();
        var (items, records) = await _repo.GetExperts(model);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse(){ 
            Message = "Truy xuất danh sách chuyên gia thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChuyenGia(long id)
    {
        //if (!await Can("Xem chuyên gia", "Chuyên gia")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repo.GetExpert(id);
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
            Message = "Truy xuất chuyên gia thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> TaoChuyenGia([FromBody] ExpertDto model)
    {
        if (!await Can("Thêm chuyên gia",module)) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateExpert(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới chuyên gia thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> CapNhatChuyenGia(long id, [FromBody] ExpertDto model)
    {
        //if (!await Can("Cập nhật chuyên gia", "Chuyên gia")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        if (!await Can("Cập nhật chuyên gia", module)) return PermissionMessage();
        //if (!await Can("Cập nhật cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _repo.UpdateExpert(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật chuyên gia thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteChuyenGia(long id)
    {
        if (!await Can("Xóa chuyên gia", module)) return PermissionMessage();
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

        await _repo.DeleteExpert(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá chuyên gia thành công!"
        });
    }

    [HttpGet("dinhdanh")]
    public async Task<IActionResult> GetAll([FromQuery] ExpertIdentifierFilter model)
    {
        var (items, records) = await _repo.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách định danh chuyên gia thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("dinhdanh/{id}")]
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
        var item = await _repo.GetByIdAsync(id);
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
            Message = "Truy xuất định danh chuyên gia thành công!",
            Data = item
        });
    }

    [HttpPost("dinhdanh")]
    public async Task<IActionResult> CreateAsync([FromBody] ExpertIdentifierDto model)
    {
        if (!await Can("Thêm cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới định danh chuyên gia thành công!",
        });
    }

    [HttpPut("dinhdanh/{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] ExpertIdentifierDto model)
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
        if (!await Can("Cập nhật cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật định danh chuyên gia thành công!"
        });
    }

    [HttpDelete("dinhdanh/{id:long}")]
    public async Task<IActionResult> Delete(long id)
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

        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá định danh chuyên gia thành công!"
        });
    }
}