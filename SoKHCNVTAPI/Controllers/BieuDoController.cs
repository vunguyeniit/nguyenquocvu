using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/bieudo")]
[ApiVersion("1")]
public class BieuDoController : BaseController
{
    private readonly IBieuDoRepository _bieuDoRepository;
    public BieuDoController(IBieuDoRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) {
        _bieuDoRepository = repository; 
    }

    [HttpGet]
    public async Task<IActionResult> GetBieuDos([FromQuery] BieuDoFilter model)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var (items, records) = await _bieuDoRepository.GetBieuDos(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách biểu đồ thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBieuDo(long id)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var item = await _bieuDoRepository.GetBieuDo(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> TaoBieuDo([FromBody] BieuDoDto model)
    {
        if (!await Can("Thêm biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if(userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không có quyền tạo biểu đồ.",
                ErrorCode = 1,
                Success = false
            });
        }
        if(model.BieuDoMauId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID mẫu biểu đồ không hợp lệ.",
                ErrorCode = 2,
                Success = false
            });
        }
        var data = await _bieuDoRepository.TaoBieuDo(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo biểu đồ thành công!",
            Data = data
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] BieuDoDto model)
    {
        if (!await Can("Cập nhật biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã biểu đồ không hợp lệ!");
        await _bieuDoRepository.CapNhatBieuDo(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> XoaBieuDo(long id)
    {
        if (!await Can("Xóa biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã biểu đồ không hợp lệ!");
        await _bieuDoRepository.XoaBieuDo(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá biểu đồ thành công."
        });
    }

    // Bieu do Mau
    [HttpGet("mau")]
    public async Task<IActionResult> GetAll([FromQuery] BieuDoMauFilter model)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var (items, records) = await _bieuDoRepository.GetBieuDoMaus(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách biểu đồ thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("mau/{id}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var item = await _bieuDoRepository.GetBieuDoMau(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("mau")]
    public async Task<IActionResult> CreateAsync([FromBody] BieuDoMauDto model)
    {
        if (!await Can("Thêm biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var data = await _bieuDoRepository.TaoBieuDoMau(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = data
        });
    }

    [HttpPut("mau/{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] BieuDoMauDto model)
    {
        if (!await Can("Cập nhật biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã không hợp lệ!");
        await _bieuDoRepository.CapNhatBieuDoMau(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("mau/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!await Can("Xóa biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã không hợp lệ!");
        await _bieuDoRepository.XoaBieuDoMau(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá biểu đồ mẫu thành công."
        });
    }


    [HttpPost("dulieu")]
    public async Task<IActionResult> TaoDuLieu([FromBody] DuLieuBieuDoDto model)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var data = await _bieuDoRepository.TaoDuLieu(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = data
        });
    }

    [HttpPut("dulieu/{id:long}")]
    public async Task<IActionResult> CapNhatDuLieu(long id, [FromBody] DuLieuBieuDoDto model)
    {
        if (!await Can("Xem biểu đồ", "Biểu đồ")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã không hợp lệ!");
        await _bieuDoRepository.CapNhatDuLieuBieuDo(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }
}