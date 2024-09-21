using System.Security.Claims;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using System.IO;
using Asp.Versioning;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Entities.CommonCategories;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/tochuc")]
[ApiVersion("1")]
public class ToChucController : BaseController
{
    private readonly IToChucRepository _repo;
    string module = "Tổ chức";

    public ToChucController(IToChucRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository) { _repo = repository; }

    #region Tổ chức

    [HttpGet("danhsach")]
    public async Task<IActionResult> GetAll([FromQuery] ToChucFilter model)
    {
        //if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        var (items, records) = await _repo.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách tổ chức thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetAsync(long id)
    {
        //if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
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
            Message = "Truy xuất tổ chức thành công!",
            Data = item
        });
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        //if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        var item = await _repo.GetByCode(code);
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất tổ chức thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ToChucDto model)
    {
        if (!await Can("Thêm tổ chức", module)) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới tổ chức thành công!",
            Data = item
        });
    }
    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] ToChucDto model)
    {
        if (!await Can("Cập nhật tổ chức", module)) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        if (!await Can("Cập nhật tổ chức", "Tổ chức")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật tổ chức thành công!"
        });
    }
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!await Can("Xóa tổ chức", module)) return PermissionMessage();
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
            Message = "Đã xoá tổ chức thành công!"
        });
    }

    [HttpPost("import")]
    public Task<IActionResult> Import(IFormFile file)
    {
        if (file is null || file.Length < 0)
        {
            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status400BadRequest, new ApiResponse
            {
                Message = "Tập tin tải lên bắt buộc!",
                Success = false,
                ErrorCode = 1
            }));
        }

        string[] extensionFileList = { ".xls", ".xlsx" };

        string fileExtension = Path.GetExtension(file.FileName);

        int isCheckValidExtension = Array.IndexOf(extensionFileList, fileExtension);

        // Không tìm thấy đuôi file hợp lệ

        if (isCheckValidExtension == -1)
        {
            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status400BadRequest, new ApiResponse
            {
                Message = "Định dạng tập tin không hợp lệ",
                Success = false,
                ErrorCode = 2
            }));
        }

        return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Nhập danh sách mới thành công!",
            Success = true,
            ErrorCode = 0
        }));
    }
    #endregion

    // Partner
    #region Đối tác

    [HttpGet("doitac")]
    public async Task<IActionResult> GetPartners([FromQuery] DoiTacToChucFilter model)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        var (items, records) = await _repo.FilterPartnerAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách đối tác thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("doitac/{id:long}")]
    public async Task<IActionResult> GetPartner(long id)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        var item = await _repo.GetPartner(id);
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
            Message = "Truy xuất đối tác thành công!",
            Data = item
        });
    }

    [HttpPost("doitac")]
    public async Task<IActionResult> CreatePartner([FromBody] DoiTacToChucDto model)
    {
        if (!await Can("Thêm tổ chức", "Tổ chức")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        _ = await _repo.CreatePartner(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới đối tác thành công!"
        });
    }

    [HttpPut("doitac/{id:long}")]
    public async Task<IActionResult> EditPartner(long id, [FromBody] DoiTacToChucDto model)
    {
        if (!await Can("Cập nhật tổ chức", "Tổ chức")) return PermissionMessage();
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
        await _repo.UpdatePartner(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật đối tác thành công!"
        });
    }

    [HttpDelete("doitac/{id:long}")]
    public async Task<IActionResult> DeletePartner(long id)
    {
        if (!await Can("Xóa tổ chức", "Tổ chức")) return PermissionMessage();
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
        await _repo.DeletePartner(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá đối tác thành công!",
            Success = true,
            ErrorCode = 0
        });
    }

    #endregion

    // Partner

    #region STAFF Nhân sự

    [HttpGet("nhansu")]
    public async Task<IActionResult> GetStaffs([FromQuery] NhanSuToChucFilter model)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        var (items, records) = await _repo.FilterStaffAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách nhân sự thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("nhansu/{id:long}")]
    public async Task<IActionResult> GetStaff(long id)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repo.GetStaff(id);
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
            Message = "Truy xuất nhân sự thành công!",
            Data = item
        });
    }

    [HttpPost("nhansu")]
    public async Task<IActionResult> CreateStaff([FromBody] NhanSuToChucDto model)
    {
        if (!await Can("Tạo tổ chức", "Tổ chức")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.CreateStaff(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới nhân sự thành công!",
            Data = item
        });
    }

    [HttpPut("nhansu/{id:long}")]
    public async Task<IActionResult> EditStaff(long id, [FromBody] NhanSuToChucDto model)
    {
        if (!await Can("Cập nhật tổ chức", "Tổ chức")) return PermissionMessage();
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
        await _repo.UpdateStaff(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật nhân sự thành công!"
        });
    }

    [HttpDelete("staff/{id:long}")]
    public async Task<IActionResult> DeleteStaff(long id)
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
        await _repo.DeleteStaff(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá nhân sự thành công."
        });
    }

    #endregion

    #region Ket qua hoat dong

    [HttpGet("ketqua")]
    public async Task<IActionResult> GetTQTLs([FromQuery] KQHDToChucFilter model)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        var (items, records) = await _repo.GetKQHDs(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách kết quả hoạt động thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("ketqua/{id:long}")]
    public async Task<IActionResult> GetTQTL(long id)
    {
        if (!await Can("Xem tổ chức", "Tổ chức")) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repo.GetKQHD(id);
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
            Message = "Truy xuất kết quả hoạt động thành công!",
            Data = item
        });
    }

    [HttpPost("ketqua")]
    public async Task<IActionResult> TaoTQTL([FromBody] KQHDToChucDto model)
    {
        if (!await Can("Thêm tổ chức", "Tổ chức")) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repo.CreateKQHD(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới kết quả hoạt động thành công!",
            Data = item
        });
    }

    [HttpPut("ketqua/{id:long}")]
    public async Task<IActionResult> CapNhatTQTL(long id, [FromBody] KQHDToChucDto model)
    {
        if (!await Can("Cập nhật tổ chức", "Tổ chức")) return PermissionMessage();
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
        await _repo.UpdateKQHD(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật kết quả hoạt động thành công!"
        });
    }

    [HttpDelete("ketqua/{id:long}")]
    public async Task<IActionResult> XoaTQTL(long id)
    {
        if (!await Can("Xóa tổ chức", "Tổ chức")) return PermissionMessage();
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
        await _repo.DeleteKQHD(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá kết quả hoạt động thành công."
        });
    }

    #endregion
}