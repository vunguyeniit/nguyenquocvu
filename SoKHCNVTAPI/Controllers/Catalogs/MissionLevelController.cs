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
namespace SoKHCNVTAPI.Controllers.Catalogs;
/// <summary>
/// Cấp độ nhiệm vụ
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/mission-levels")]
[ApiVersion("1")]
public class MissionLevelController : BaseController
{
    private readonly ICapDoNhiemVuRepository _repo;

    public MissionLevelController(ICapDoNhiemVuRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MissionLevelFilter model)
    {
        var (items, records) = await _repo.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách cấp độ nhiệm vụ thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không tìm thấy",
                ErrorCode = 1,
                Success = false
            });
        };
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] MissionLevelDto model)
    {
        if (!await Can("Thêm cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] MissionLevelDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
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
        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
}