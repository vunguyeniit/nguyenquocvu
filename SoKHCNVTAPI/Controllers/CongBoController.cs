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
/// cong bo
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/congbo")]
[ApiVersion("1")]
public class CongBoController : BaseController
{
    private readonly ICongBoRepository _repo;

    public CongBoController(ICongBoRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetCongBos([FromQuery] CongBoFilter model)
    {
        if (!await Can("Xem công bố khoa học", "Công bố khoa học")) return PermissionMessage();
        var (items, records) = await _repo.FilterAsync(model);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse()
        {
            Message = "Truy xuất danh sách công bố thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCongBo(long id)
    {
        if (!await Can("Xem công bố khoa học", "Công bố khoa học")) return PermissionMessage();
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
            Message = "Truy xuất công bố thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> TaoChuyenGia([FromBody] CongBoDto model)
    {
        if (!await Can("Thêm công bố khoa học", "Công bố khoa học")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới công bố thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> CapNhatCongBo(long id, [FromBody] CongBoDto model)
    {
        if(!await Can("Cập nhật công bố khoa học", "Công bố khoa học")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        //if (!await Can("Cập nhật cấu hình", "Cấu hình")) return PermissionMessage();
        if (!await Can("Cập nhật công bố khoa học", "Công bố khoa học")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật công bố thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteCongBo(long id)
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

        if (!await Can("Xóa công bố khoa học", "Công bố khoa học")) return PermissionMessage();

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá công bố thành công!"
        });
    }

}