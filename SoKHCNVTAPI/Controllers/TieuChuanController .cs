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
[Route("/api/v{version:apiVersion}/tieuchuan")]
[ApiVersion("1")]
public class TieuChuanController : BaseController
{
    private readonly ITieuChuanRepository _repo;

    public TieuChuanController(ITieuChuanRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetChuyenGias([FromQuery] TieuChuanFilter model)
    {
        if (!await Can("Xem tiêu chuẩn", "Tiêu chuẩn")) return PermissionMessage();

        var (items, records) = await _repo.FilterAsync(model);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse()
        {
            Message = "Truy xuất danh sách tiêu chuẩn thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetChuyenGia(long id)
    {
        if (!await Can("Xem tiêu chuẩn", "Tiêu chuẩn")) return PermissionMessage();
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
            Message = "Truy xuất tiêu chuẩn thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> TaoChuyenGia([FromBody] TieuChuanDto model)
    {
        if (!await Can("Thêm tiêu chuẩn", "Tiêu chuẩn")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới tiêu chuẩn thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> CapNhatChuyenGia(long id, [FromBody] TieuChuanDto model)
    {
        if (!await Can("Cập nhật tiêu chuẩn", "Tiêu chuẩn")) return PermissionMessage();
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
            Message = "Cập nhật tiêu chuẩn thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteChuyenGia(long id)
    {
        if (!await Can("Xóa tiêu chuẩn", "Tiêu chuẩn")) return PermissionMessage();
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
            Message = "Đã xoá tiêu chuẩn thành công!"
        });
    }

}