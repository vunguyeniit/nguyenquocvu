using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using DocumentFormat.OpenXml.Spreadsheet;
using SoKHCNVTAPI.Helpers;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/doanhnghiep")]
[ApiVersion("1")]
public class DoanhNghiepController : BaseController
{
    private readonly IDoanhNghiepRepository _repo;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Role> _rolePermission;
    private readonly IRepository<Permission> _permissionRepository;
    string module = "Doanh nghiệp";
    public DoanhNghiepController(IUserRepository userRepository,
    IRepository<Permission> permissionRepository,
    IRepository<Role> rolePermission,
    IDoanhNghiepRepository repository) : base(permissionRepository, rolePermission, userRepository)
    {
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _rolePermission = rolePermission;
        _repo = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DoanhNghiepFilter model)
    {

        //if (!await Can("Xem doanh nghiệp", "Doanh nghiệp")) return PermissionMessage();

        var (items, records) = await _repo.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
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
        
        //if (!await Can("Xem doanh nghiệp", module)) return PermissionMessage();

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
            Message = "Truy xuất doanh nghiệp thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] DoanhNghiepDto model)
    {
        if (!await Can("Thêm doanh nghiệp", module)) return PermissionMessage();

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
      
        await _repo.CreateAsync(model, userId);

        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới doanh nghiệp thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] DoanhNghiepDto model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (!await Can("Cập nhật doanh nghiệp", module)) return PermissionMessage();
       
        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật doanh nghiệp thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (!await Can("Xóa doanh nghiệp", module)) return PermissionMessage();

        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá doanh nghiệp"
        });
    }
}