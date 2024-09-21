using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
using SoKHCNVTAPI.Migrations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/users")]
[ApiVersion("1")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCachingService _iMemoryCachingService;
    private readonly IRepository<GiaoDien> _giaoDienRepository;
    string module = "Tài khoản";

    public UserController(IUserRepository repository, IRepository<GiaoDien> giaoDienRepository,
    IRepository<Permission> permissionRepository, IMemoryCachingService iMemoryCachingService,
        IRepository<Role> rolePermission, IUserRepository userRepository) : base(permissionRepository, rolePermission, userRepository) 
    {
        _userRepository = repository;
        _giaoDienRepository = giaoDienRepository;
        _iMemoryCachingService = iMemoryCachingService;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll([FromQuery] PaginationDto model)
    // {
    //     var (items, records) = await _repository.PagingAsync(model);
    //     return StatusCode(StatusCodes.Status200OK, new PaginationResponse<UserResponse>
    //     {
    //         Message = "Truy xuất danh sách thành công!",
    //         Meta = new Meta(model, records),
    //         Data = items
    //     });
    // }
        [HttpGet("nguoidung/danhsach")]
        public async Task<IActionResult> GetUser([FromQuery] UserFilterDto model)
        {
            var (items, records) = await _userRepository.FilterUser(model);
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Truy xuất danh sách thành công!",
                Meta = new Meta(model, records),
                Data = items
            });
        }
   
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UserFilterDto model)
    {
        if (!await Can("Xem tài khoản", module))
        {
            return PermissionMessage();


                }
        var (items, records) = await _userRepository.Filter(model);
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
        if (!await Can("Xem tài khoản", module)) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _userRepository.GetByIdAsync(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto model)
    {

        if (!await Can("Thêm tài khoản", module)) return PermissionMessage();
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //UserResponse? user = await _userRepository.GetByIdAsync(userId);
        //string fullname = "";
        //if (user != null)
        //{
        //    fullname = user.FullName != null ? user.FullName : "";
        //}
        await _userRepository.Create(model, userId);

        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!"
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] UserUpdateDto model)
    {
        if (!await Can("Cập nhật tài khoản", module)) return PermissionMessage();
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
        await _userRepository.Update(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpPost("MatKhau/{id:long}")]
    public async Task<IActionResult> UpdatePassword(long id, [FromBody] UserUpdatePasswordDto model)
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
        if(model.Password.IsNullOrEmpty())
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mật khẩu không hợp lệ!",
                ErrorCode = 2,
                Success = false
            });
        }
        
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _userRepository.UpdatePassword(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (!await Can("Xóa tài khoản", module)) return PermissionMessage();
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        await _userRepository.Delete(id);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá thành công."
        });
    }

    //[HttpGet]
    //[Route("Nhom")]
    //public async Task<IActionResult> GetNhoms(NhomFilter model)
    //{
    //    long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    //    if (long.IsNegative(userId))
    //    {
    //        return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //        {
    //            Message = "Không quyền truy cập",
    //            ErrorCode = 1,
    //            Success = false
    //        });
    //    }

    //     var nhoms = await _userRepository.GetNhoms(model);

    //    return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //    {
    //        Message = "Truy xuất nhóm quyền thành công",
    //        Data = nhoms,
    //        Success = true,
    //        ErrorCode = 0
    //    });
        
      
    //}

    //[HttpGet]
    //[Route("Nhom/{id:long}")]
    //public async Task<IActionResult> GetNhom([FromQuery] long id)
    //{

    //    long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    //    if (long.IsNegative(userId))
    //    {
    //        return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //        {
    //            Message = "Không quyền truy cập",
    //            ErrorCode = 1,
    //            Success = false
    //        });
    //    }

    //    var nhom = await _userRepository.GetNhom(id);

    //    return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //    {
    //        Message = "Truy xuất nhóm quyền thành công",
    //        Data = nhom,
    //        Success = true,
    //        ErrorCode = 0
    //    });
    //}

    //[HttpPost]
    //[Route("Nhom")]
    //public async Task<IActionResult> TaoNhom(NhomDto model)
    //{

    //    long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    //    if (long.IsNegative(userId))
    //    {
    //        return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //        {
    //            Message = "Không quyền truy cập",
    //            ErrorCode = 1,
    //            Success = false
    //        });
    //    }

    //    await _userRepository.TaoNhom(model, userId);

    //    return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //    {
    //        Message = "Tạo nhóm quyền thành công",
    //        Success = true,
    //        ErrorCode = 0
    //    });
    //}

    //[HttpPut]
    //[Route("Nhom/{id:long}")]
    //public async Task<IActionResult> CapNhatNhom(long id, NhomDto model)
    //{

    //    long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    //    if (long.IsNegative(userId))
    //    {
    //        return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //        {
    //            Message = "Không quyền truy cập",
    //            ErrorCode = 1,
    //            Success = false
    //        });
    //    }

    //    await _userRepository.CapNhatNhom(id, model, userId);

    //    return StatusCode(StatusCodes.Status200OK, new ApiResponse
    //    {
    //        Message = "Cập nhật nhóm quyền thành công",
    //        Success = true,
    //        ErrorCode = 0
    //    });
    //}

    [HttpGet]
    [Route("GiaoDien/{Ma}")]
    public async Task<IActionResult> GetGiaoDien(string Ma)
    {
        long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (long.IsNegative(userId))
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không quyền truy cập",
                ErrorCode = 1,
                Success = false
            });
        }

        GiaoDien? giaodien = _iMemoryCachingService.Get<GiaoDien>("GIAO_DIEN_" + userId);

        if (giaodien == null)
        {
            giaodien = await _userRepository.GetGiaoDien(Ma, userId);
        }

        if(giaodien.Id > 0)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Truy xuất giao diện thành công",
                Data = giaodien,
                Success = true,
                ErrorCode = 0
            });
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Giao diện không tìm thấy.",
            Success = false,
            ErrorCode = 1
        });
    }

    [HttpGet]
    [Route("GiaoDien")]
    public async Task<IActionResult> GetGiaoDiens([FromQuery] GiaoDienFilter model)
    {
       
        long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (long.IsNegative(userId))
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không quyền truy cập",
                ErrorCode = 1,
                Success = false
            });
        }

        List<GiaoDien>? giaodiens = await _userRepository.GetGiaoDiens(model);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất giao diện thành công",
            Data = giaodiens,
            Success = true,
            ErrorCode = 0
        });
    }

    [HttpPost]
    [Route("GiaoDien")]
    public async Task<IActionResult> LuuGiaoDien(GiaoDienModel model)
    {
        
        long userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (long.IsNegative(userId))
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không quyền truy cập",
                ErrorCode = 1,
                Success = false
            });
        }


        GiaoDien? giaodien = await _userRepository.CreateUpdateGiaoDien(model, userId);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất giao diện thành công",
            Data = giaodien,
            Success = true,
            ErrorCode = 0
        });
    }

    [HttpGet("accessKey")]
    public async Task<IActionResult> GetTokens()
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (userId <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Không quyền tải tập tin",
                Success = false,
                ErrorCode = 2
            });
        }

        //if (!await Can("Cập nhật cấu hình", "Cấu hình")) return PermissionMessage();
        if (!await Can("Xem quản lý API ", "Quản lý API")) return PermissionMessage();

        string finalSubDirectory = Path.Combine("Assets", "APIKeys.json");
        if (System.IO.File.Exists(finalSubDirectory))
        {
            string json = await System.IO.File.ReadAllTextAsync(finalSubDirectory);
            List<APIKeyModel>? _apiKeyList = JsonConvert.DeserializeObject<List<APIKeyModel>>(json);
            if (_apiKeyList != null)
            {
                if (_apiKeyList.Count > 0)
                {
                    var ret = new List<APIKeyModel>();
                    //if (!model.TuKhoa.IsNullOrEmpty())
                    //{
                    //    foreach (APIKeyModel apikey in _apiKeyList)
                    //    {
                    //        if (apikey.Name.Contains(model.TuKhoa) || apikey.Key.Contains(model.TuKhoa))
                    //        {
                    //            ret.Add(apikey);
                    //        }
                    //    }
                    //} else
                    //{
                    ret = _apiKeyList;
                    //}
                    return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
                    {
                        Message = "Tải ảnh lên thành công!",
                        Success = true,
                        Data = ret
                    });
                }
            }
        }


        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Không tìm thấy dữ liệu mã truy cập API!",
            Success = false,
            ErrorCode = 1
        });
    }

    [HttpPost("accessKey")]
    public async Task<IActionResult> SaveTokens([FromBody] APIKeyModel model)
    {
        if (model == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã truy cập API không chính xác.",
                Success = false,
                ErrorCode = 1
            });
        }

        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //if (userId <= 0)
        //{
        //    return StatusCode(StatusCodes.Status200OK, new BaseResponse
        //    {
        //        Message = "Không quyền tải tập tin",
        //        Success = false,
        //        ErrorCode = 2
        //    });
        //}
        //if (!await Can("Cập nhật cấu hình", "Cấu hình")) return PermissionMessage();
        if (!await Can("Thêm quản lý API", "Quản lý API")) return PermissionMessage();
        bool isFound = false;
        List<APIKeyModel>? _apiKeyList = new List<APIKeyModel>();
        string finalSubDirectory = Path.Combine("Assets", "APIKeys.json");
        if (System.IO.File.Exists(finalSubDirectory))
        {
            string json = await System.IO.File.ReadAllTextAsync(finalSubDirectory);
            if (json != null)
            {
                _apiKeyList = JsonConvert.DeserializeObject<List<APIKeyModel>>(json);
                if (_apiKeyList != null)
                {
                    if (_apiKeyList.Count > 0)
                    {
                        for (int i = 0; i < _apiKeyList.Count; i++)
                        {
                            APIKeyModel apikey = _apiKeyList[i];
                            if (apikey.Code == model.Code)
                            {
                                _apiKeyList[i].Name = model.Name;
                                _apiKeyList[i].Key = model.Key;
                                _apiKeyList[i].IP = model.IP;
                                _apiKeyList[i].Rate = model.Rate;
                                _apiKeyList[i].Status = model.Status;
                                _apiKeyList[i].TimeStamp = model.TimeStamp;
                                isFound = true;
                                break;
                            }
                        }
                    }
                }
            }


            if (isFound == false)
            {
                if (_apiKeyList == null)
                {
                    _apiKeyList = new List<APIKeyModel>();
                }

                _apiKeyList.Add(model);
            }

            string updatedJson = JsonConvert.SerializeObject(_apiKeyList);

            await System.IO.File.WriteAllTextAsync(finalSubDirectory, updatedJson);
            return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
            {
                Message = "Lưu mã truy xuất API thành công!",
                Success = true,
                ErrorCode = 0
            });

        }

        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Không tìm thấy dữ liệu mã truy cập API!",
            Success = false,
            ErrorCode = 1
        });
    }
}