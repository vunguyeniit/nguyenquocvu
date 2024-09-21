using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories;
using SoKHCNVTAPI.Models.Base;
using Asp.Versioning;
using DocumentFormat.OpenXml.Office2010.Excel;
using SoKHCNVTAPI.Migrations;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/missions")]
[ApiVersion("1")]
public class NhiemVuController : BaseController
{
    private readonly INhiemVuRepository _repo;
    string module = "Nhiệm vụ";

    public NhiemVuController(INhiemVuRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository)
    { 
        _repo = repository; 
    }
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CommonFilterDto model)
    {
        //if (!await Can("Xem nhiệm vụ", "Nhiệm vụ")) return PermissionMessage();
        var (items, records) = await _repo.SearchAsync(model);
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
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("info")]
    public async Task<IActionResult> Info([FromBody] MissionInformationDto model)
    {
        if (!await Can("Thêm nhiệm vụ", module)) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var returnId = await _repo.Info(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = new
            {
                InfoId = returnId
            }
        });
    }

    [HttpPut("info/{misionId:long}")]
    public async Task<IActionResult> InfoUpdateAsync(long misionId, [FromBody] MissionInformationDto model)
    {
        if (!await Can("Cập nhật nhiệm vụ", module)) return PermissionMessage();
        if (misionId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.UpdateInfoAsync(misionId, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpPost("processing/{id:long}")]
    public async Task<IActionResult> Processing(long id, [FromBody] MissionProcessingDto model)
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

        await _repo.Processing(id, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật nhiệm vụ thành công!"
        });
    }

    [HttpPut("processing/{missionId:long}")]
    public async Task<IActionResult> ProcessingUpdateAsync(long missionId, [FromBody] MissionProcessingDto model)
    {
        if (missionId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.UpdateProcessingAsync(missionId, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới nhiệm vụ thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    [HttpPost("result/{id:long}")]
    public async Task<IActionResult> Result(long id, [FromBody] MissionResultDto model)
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
       
        await _repo.Result(id, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật nhiệm vụ thành công!"
        });
    }

    [HttpPut("result/{missionId:long}")]
    public async Task<IActionResult> ResultUpdateAsync(long missionId, [FromBody] MissionResultDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.UpdateResultAsync(missionId, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật kết quả nhiệm vụ thành công!"
        });
    }

    [HttpPost("application/{id:long}")]
    public async Task<IActionResult> Application(long id, [FromBody] MissionApplicationDto model)
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

        await _repo.Application(id, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpPut("application/{missionId:long}")]
    public async Task<IActionResult> ResultUpdateAsync(long missionId, [FromBody] MissionApplicationDto model)
    {
        if (missionId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.UpdateApplicationAsync(missionId, model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!await Can("Xóa nhiệm vụ", module)) return PermissionMessage();
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
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Xoá nhiệm vụ thành công!"
        });
    }
}