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
/// Thông tin
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/xquang")]
[ApiVersion("1")]
public class XQuangController : BaseController
{
    private readonly IXQuangRepository _repo;

    public XQuangController(IXQuangRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetChuyenGias([FromQuery] XQuangFilter model)
    {
        //if (!await Can("Xem x quang", "X quang")) return PermissionMessage();
        if (!await Can("Xem bức xạ", "Bức xạ")) return PermissionMessage();

        var (items, records) = await _repo.FilterAsync(model);

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse()
        {
            Message = "Truy xuất x quang thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetXQuang(long id)
    {
        //if (!await Can("Xem x quang", "X quang")) return PermissionMessage();
        if (!await Can("Xem bức xạ", "Bức xạ")) return PermissionMessage();
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
                Message = "x quang không tìm thấy",
                Success = false,
                ErrorCode = 2,
            });
        }

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất x quang thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> TaoChuyenGia([FromBody] XQuangDto model)
    {
        //if (!await Can("Thêm x quang", "X quang")) return PermissionMessage();
        if (!await Can("Thêm bức xạ", "Bức xạ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới x quang thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> CapNhatChuyenGia(long id, [FromBody] XQuangDto model)
    {
        //if (!await Can("Cập nhật x quang", "X quang")) return PermissionMessage();

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
        if (!await Can("Cập nhật bức xạ", "Bức xạ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thông tin thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteChuyenGia(long id)
    {
        //if (!await Can("Xóa x quang", "X quang")) return PermissionMessage();
        if (!await Can("Xóa bức xạ", "Bức xạ")) return PermissionMessage();
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
            Message = "Đã xoá x quang thành công!"
        });
    }
}