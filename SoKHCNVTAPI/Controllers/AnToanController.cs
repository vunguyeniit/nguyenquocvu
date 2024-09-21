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
/// AnToan
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/antoan")]
[ApiVersion("1")]
public class AnToanController : BaseController
{
    private readonly IAnToanRepository _repo;

    public AnToanController(IAnToanRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) 
    { 
        _repo = repository; 
    }

    [HttpGet]
    public async Task<IActionResult> GetAnToans([FromQuery] AnToanFilter model)
    {
        if (!await Can("Xem an toàn", "An toàn")) return PermissionMessage();

        var (items, records) = await _repo.FilterAsync(model);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse()
        {
            Message = "Truy xuất thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetThongTin(long id)
    {
        if (!await Can("Xem an toàn", "An toàn")) return PermissionMessage();
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
        if(item == null) {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Thông tin không tìm thấy",
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

    [HttpPost]
    public async Task<IActionResult> TaoAnToan([FromBody] AnToanDto model)
    {
        if (!await Can("Thêm an toàn", "An toàn")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> CapNhatAnToan(long id, [FromBody] AnToanDto model)
    {
        if (!await Can("Cập nhật an toàn", "An toàn")) return PermissionMessage();
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
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAnToan(long id)
    {
        if (!await Can("Xóa an toàn", "An toàn")) return PermissionMessage();
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
            Message = "Đã xoá thành công!"
        });
    }
}