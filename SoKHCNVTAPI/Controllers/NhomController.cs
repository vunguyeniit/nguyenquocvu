using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories;
using SoKHCNVTAPI.Models.Base;
using Asp.Versioning;
using System.Security.Claims;
namespace SoKHCNVTAPI.Controllers;
/// <summary>
/// Nhóm
/// </summary>
[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/groups")]
[ApiVersion("1")]
public class NhomController : BaseController
{
    private readonly IGroupRepository _repository;
    public NhomController(IGroupRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository)
    {
        _repository = repository;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll([FromQuery] PaginationDto model)
    //{
    //    var (items, records) = await _repository.PagingAsync(model);
    //    return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
    //    {
    //        Message = "Truy xuất danh sách thành công!",
    //        Meta = new Meta(model, records),
    //        Data = items
    //    });
    //}

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] NhomFilterDto model)
    {
        var (items, records) = await _repository.Filter(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        var item = await _repository.GetByIdAsync(id);
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NhomDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repository.Create(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] NhomDto model)
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
        await _repository.Update(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
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
        await _repository.Delete(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
}