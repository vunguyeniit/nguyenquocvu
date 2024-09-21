using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using SoKHCNVTAPI.Repositories.CommonCategories;

namespace SoKHCNVTAPI.Controllers.Catalogs;
/// <summary>
/// Loại công bố
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/publication-types")]
[ApiVersion("1")]
public class LoaiCongBoController : BaseController
{
    private readonly ILoaiHinhCongBoRepository _repo;

    public LoaiCongBoController(ILoaiHinhCongBoRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] LoaiCongBoFilter model)
    {
        var (items, records) = await _repo.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách loại công bố thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất loại công bố thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] LoaiCongBoDto model)
    {
        if (!await Can("Thêm cấu hình", "Cấu hình")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới loại công bố thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] LoaiCongBoDto model)
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
            Message = "Cập nhật loại công bố thành công!"
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
            Message = "Đã xoá loại công bố !"
        });
    }
}