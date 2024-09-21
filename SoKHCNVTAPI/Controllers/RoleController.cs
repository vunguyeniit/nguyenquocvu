using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using DocumentFormat.OpenXml.Math;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/roles")]
[ApiVersion("1")]
public class RoleController : BaseController
{
    private readonly IRoleRepository _repo;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Role> _rolePermission;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IMemoryCachingService _iMemoryCachingService;

    public RoleController(IRoleRepository repo, 
    IUserRepository userRepository,
    IRepository<Permission> permissionRepository,
    IRepository<Role> rolePermission, IMemoryCachingService iMemoryCachingService) : base(permissionRepository, rolePermission, userRepository)
	{
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _rolePermission = rolePermission;
        _repo = repo;
        _iMemoryCachingService = iMemoryCachingService;

    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RoleFilter model)
    {
        var (items, records) = await _repo.FilterAsync(model);
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
        var item = await _repo.GetByIdAsync(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoleModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        _iMemoryCachingService.Remove("ROLE_MODULE");
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] RoleModel model)
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
        await _repo.UpdateAsync(id, model, userId);
        _iMemoryCachingService.Remove("ROLE_MODULE");
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

        _iMemoryCachingService.Remove("ROLE_MODULE");

        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá vai trò!"
        });
    }
    [HttpGet("module")]
    public async Task<IActionResult> GetRoleByModule([FromQuery] string module="", int isCache = 1)
    {
        IEnumerable<RoleModuleDto>? ret = null;
        if (isCache == 1)
        {
            ret = _iMemoryCachingService.Get<IEnumerable<RoleModuleDto>>("ROLE_MODULE");
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if(ret == null)
        {
            ret = await _repo.GetRoleByModule(module, userId);
            _iMemoryCachingService.Set<IEnumerable<RoleModuleDto>>("ROLE_MODULE", ret, 60 * 1);
        }
       
        if (ret != null)
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Danh sách quyền theo module thành công!",
                Success = true,
                ErrorCode = 0,
                Data = ret
            });
        }
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Danh sách quyền theo module lỗi!",
            Success = false,
            ErrorCode = 1
        });
    }

    [HttpGet("user/{id:long}")]
    public async Task<IActionResult> GetRoleByUser(long id)
    {
        if(id <= 0)
        {
            id = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        var ret = await GetRoleList(id, 0);
        if (ret != null)
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Danh sách quyền theo tài khoản thành công!",
                Success = true,
                ErrorCode = 0,
                Data = ret
            });
        }
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Danh sách quyền theo tài khoản lỗi!",
            Success = false,
            ErrorCode = 1
        });
    }

    [HttpGet("group/{id:long}")]
    public async Task<IActionResult> GetRoleByGroup(long id)
    {
        if (id <= 0)
        {
            id = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }
        var ret = await GetRoleList(0, id);
        if (ret != null)
        {
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Danh sách quyền theo tài khoản thành công!",
                Success = true,
                ErrorCode = 0,
                Data = ret
            });
        }
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Danh sách quyền theo tài khoản lỗi!",
            Success = false,
            ErrorCode = 1
        });
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignUser(RoleUserModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var ret = await _repo.AssignRoleByUser(model, userId);
        if(ret)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Gán quyền thành công!",
            });
        }
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Gán quyền lỗi!",
            Success = false,
            ErrorCode = 1
        });
    }
}