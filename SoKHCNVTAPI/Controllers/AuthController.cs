using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Configurations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using SoKHCNVTAPI.Services;

namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/auth")]
[ApiVersion("1")]
public class AuthController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IMemoryCachingService _iMemoryCachingService;



    public AuthController(IMapper mapper, ITokenService tokenService, IRepository<GiaoDien> giaoDienRepository,
    IRepository<Permission> permissionRepository, IMemoryCachingService iMemoryCachingService,
    IRepository<Role> roleRepository, DataContext context, IActivityLogRepository activityLogRepository, IUserRepository userRepository)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _context = context;
        _permissionRepository = permissionRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _activityLogRepository = activityLogRepository;
        _iMemoryCachingService = iMemoryCachingService;

    }

    // [HttpPost]
    // [AllowAnonymous]
    // [Route("signup")]
    // public async Task<IActionResult> SignUp([FromBody] SignUpDto model)
    // {
    //     var isEmail = Utils.IsValidEmail(model.Username);
    //     var isPhoneNumber = Utils.IsValidPhoneNumber(model.Username);
    //
    //     if (isEmail == false && isPhoneNumber == false)
    //     {
    //         throw new ArgumentException("Email hoặc số điện thoại không hợp lệ.");
    //     }
    //
    //     var searchParam = isEmail ? model.Username : Utils.FormatPhoneNumber(model.Username);
    //
    //     var user = await _context
    //         .Set<User>()
    //         .AsNoTracking()
    //         .FirstOrDefaultAsync(p => p.Email == searchParam || p.Phone == searchParam);
    //
    //     if (user != null) throw new ArgumentException("Người dùng đã tồn tại.");
    //
    //     var newUser = new User
    //     {
    //         Password = PasswordBuilder.HashBCrypt(model.Password),
    //     };
    //
    //     if (isEmail) newUser.Email = searchParam;
    //     if (isPhoneNumber) newUser.Phone = searchParam;
    //
    //     _context.Set<User>().Add(newUser);
    //     await _context.SaveChangesAsync();
    //
    //     return StatusCode(StatusCodes.Status201Created, new ResponseBase
    //     {
    //         Message = "Tạo tài khoản thành công",
    //     });
    // }//Captcha

 

    [HttpPost]
    [AllowAnonymous]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto model)
    {
        string? sessionCaptcha = _iMemoryCachingService.Get<string>(model.Session);
        if (sessionCaptcha == null)
        {
            Random random = new Random();
            string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
            string newKey = model.Session + randomNumberInRange;
            _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);


            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Mã không chính xác.",
                Success = false,
                ErrorCode = 2, // Error code for incorrect captcha
                Session = newKey
            });
        }

        string _key = model.Session.Substring(model.Session.Length - 4);
        if (_key != sessionCaptcha)
        {
            Random random = new Random();
            string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
            string newKey = model.Session + randomNumberInRange;
            //Lưu session
            _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Mã không chính xác",
                Success = false,
                ErrorCode = 2,
                Session = newKey
            });
        }

        // Xóa mã captcha khỏi session sau khi kiểm tra
        _iMemoryCachingService.Remove(model.Session);
        ///
        var isEmail = Utils.IsValidEmail(model.Username);
        var isPhoneNumber = Utils.IsValidPhoneNumber(model.Username);

        if (isEmail == false && isPhoneNumber == false)
        {
            Random random = new Random();
            string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
            string newKey = model.Session + randomNumberInRange;
            _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Thông tin đăng nhập không chính xác.",
                Success = false,
                ErrorCode = 1,
                Session = newKey
            });
        }


        var searchParam = isEmail ? model.Username : Utils.FormatPhoneNumber(model.Username);

        var user = await _context.Set<User>().SingleOrDefaultAsync(p => p.Email == searchParam || p.Phone == searchParam);

        int _loginFail = _iMemoryCachingService.Get<int>(searchParam);
        if(_loginFail >= 5)
        {
            Random random = new Random();
            string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
            string newKey = model.Session + randomNumberInRange;
            _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Tài khoản đã bị khóa do nhập sai thông tin quá nhiều lần",
                Success = false,
                ErrorCode = 4,
                Session = newKey
            });
        }

        if (user == null)
        {
            _loginFail = _loginFail + 1;
            _iMemoryCachingService.Set<int>(searchParam, _loginFail, 5);

            Random random = new Random();
            string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
            string newKey = model.Session + randomNumberInRange;
            _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);
            return StatusCode(StatusCodes.Status200OK, new
            {
                Message = "Thông tin tài khoản không tìm thấy.",
                Success = false,
                ErrorCode = 404,
                Session = newKey
            });          
        }
        else
            { 
            // check password
            if (user.Password != null)
            {
                var isPass = PasswordBuilder.VerifyBCrypt(model.Password, user.Password);
                if (!isPass)
                {
                    _loginFail = _loginFail + 1;
                    _iMemoryCachingService.Set<int>(searchParam, _loginFail, 5);

                    //Kiểm tra nhập sai thông tin                
                    //user.LoginFailed = (user.LoginFailed ?? 0) + 1;
                    //_context.Entry(user).State = EntityState.Modified; // Đảm bảo trạng thái entity đã được thay đổi
                    //await _context.SaveChangesAsync();
                    //if (user.LoginFailed >= 5 ) // Va thoi gian < 5 phút
                    //{
                    //    user.Status = (short)UserStatus.Locked;
                    //    await _context.SaveChangesAsync(); // Save changes to lock the account

                    //    return StatusCode(StatusCodes.Status200OK, new BaseResponse
                    //    {
                    //        Message = "Tài khoản đã bị khóa do nhập sai thông tin quá nhiều lần",
                    //        Success = false,
                    //        ErrorCode = 4 // New error code for account locked due to failed attempts
                    //    });
                    //}

                    int nums = 5;
                    if(_loginFail > 5)
                    {
                        _loginFail = 5;
                    }
                    Random random = new Random();
                    string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
                    string newKey = model.Session + randomNumberInRange;
                    _iMemoryCachingService.Set<String>(newKey, randomNumberInRange, 2);
                    return StatusCode(StatusCodes.Status200OK, new
                    {
                        Message = $"Nhập thông tin không chính xác, bạn còn {nums - _loginFail}",
                        Success = false,
                        ErrorCode = 1,
                        Session = newKey
                    });
                    //throw new ArgumentException("Mật khẩu không đúng.");
                }
                //else
                //{                   
                //    // Reset login failed count if password is correct
                //    user.LoginFailed = 0;
                //    _context.Entry(user).State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}         
            }
        }

        // Kiểm tra trạng thái tài khoản
        if (user.Status == (short)UserStatus.DeActivate) {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Tài khoản chưa kích hoạt",
                Success = false,
                ErrorCode = 2
            });
        } else  if(user.Status == (short)UserStatus.Locked) {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message =  "Tài khoản đã bị khóa",
                Success = false,
                ErrorCode = 3
            });
        }
       
        // jwt
        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        // Update User
        user.LastLogin = Utils.getCurrentDate();
        user.Token = token;
     
        user.RefreshToken = refreshToken;

        await _context.SaveChangesAsync();

        // Update Token
        //TODO: 
        var userMapped = _mapper.Map<UserResponse>(user);
        // Get Menu
        var createdBy = user.Id;
        DateTime now = DateTime.Now; // Lấy thời gian hệ thống hiện tại
        var log = new ActivityLogDto
        {
            Contents = $"đăng nhập thành công vào lúc.",
            Params = "Đăng nhập",
            Target = "Đăng nhập",
            TargetCode = user.Code,
            UserId = createdBy
        };

        _iMemoryCachingService.Remove(searchParam);

        await _activityLogRepository.SaveLogAsync(log, createdBy, LogMode.Account);
        return StatusCode(StatusCodes.Status200OK, new SignInBaseResponse
        {
            Message = "Đăng nhập thành công!",
            Token = token,
            RefreshToken = refreshToken,
            User = userMapped
        });

        
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
           
        var UserId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _context.Set<User>().SingleOrDefaultAsync(p => p.Id == UserId);

        if (user == null) throw new ArgumentException("Người dùng không tồn tại.");

        user.Token = null;   
        user.RefreshToken = null;
        await _context.SaveChangesAsync();
       
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đăng thoát thành công",
        });
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh()
    {
        // var oldToken = HttpContext.Request.Headers.Authorization!.ToString().Split(" ").Last();
        var oldRefreshToken = HttpContext.Request.Headers["_rt"].FirstOrDefault();
        if (oldRefreshToken == null) throw new ArgumentException("Token không hợp lệ 1.");
        var user = await _context.Set<User>().SingleOrDefaultAsync(p => p.RefreshToken == oldRefreshToken);
        if (user == null) throw new ArgumentException("Token không hợp lệ 2.");

        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        user.Token = token;
        user.RefreshToken = refreshToken;
        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK, new SignInBaseResponse
        {
            Message = "Khởi tạo lại mã đăng nhập thành công.",
            Token = token,
            RefreshToken = refreshToken
        });
    }

    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var user = await _context.Set<User>().FindAsync(userId);
        if(user == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Tài khoản không tồn tại.",
                Success = false,
                ErrorCode = 1
            });
        } else
        {
            if (user.Password != null)
            {
                //var isSame = PasswordBuilder.VerifyBCrypt(model.OldPassword, user.Password);
                //if (!isSame) throw new ArgumentException("Mật khẩu không đúng.");

                user.Password = PasswordBuilder.HashBCrypt(model.NewPassword);
                _context.Set<User>().Update(user);
                await _context.SaveChangesAsync();

                // Ghi logs
                ActivityLog activityLog = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    Contents = user.Fullname + " đã thay đổi mật khẩu tài khoản " + user.Code,
                    Target = Target.User.Value.ToString(),
                    TargetCode = user.Code,
                    FullName = user.Fullname ?? "",
                    UserId = userId,
                    Params = JsonSerializer.Serialize(user)
                };
                await _context.Set<ActivityLog>().AddAsync(activityLog);

                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Thay đổi mật khẩu thành công",
                    Success = true,
                    ErrorCode = 0

                });
            } else
            {
                return StatusCode(StatusCodes.Status200OK, new BaseResponse
                {
                    Message = "Tài khoản không chính xác.",
                    Success = false,
                    ErrorCode = 2
                });
            }
        }
    }

    [HttpGet]
    [Route("profile")]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        User? user = await _context.Set<User>().FindAsync(long.Parse(userId!));

        if (user is null) throw new UnauthorizedAccessException("Không tìm thấy người dùng");

        List<long> RoleIds = await _permissionRepository.Select().Where(x => x.UserId == user.Id).Select(p => p.RoleId).ToListAsync();

        var roles = await _roleRepository.Select().Where(x => RoleIds.Contains(x.Id)).Where(p => p.Status == 1).ToListAsync();

        //Role? roleOfUser = await _context.Set<Role>().Where(x => x.Status == 1).SingleOrDefaultAsync(x => x.Code == user.Role);
        //if (roleOfUser is null) throw new UnauthorizedAccessException("Người dùng chưa được cấp quyền");
        // List<Permission>? roleHasPermissions = await _context.Set<Permission>().Where(x => x.UserId == user.Id).ToListAsync();

        List<RoleModel> roleModels = new List<RoleModel>();
        foreach (Role role in roles)
        {
            if (role != null)
            {
                roleModels.Add(new RoleModel { Code = role.Code, Module = role.Module, Status = 1, 
                Description = "" });
            }
        }

        var userMapped = _mapper.Map<UserResponse>(user!);

        userMapped.roles = roleModels;

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thông tin cá nhân thành công",
            Data = userMapped
        });
    }

    [HttpPut]
    [Route("profile")]
    public async Task<IActionResult> UpdateProfile(UserUpdateDto model)
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return NotFound("Không tìm thấy userid");
        }
        Console.WriteLine(userId);
       User? user = await _context.Set<User>().SingleOrDefaultAsync(p => p.Id == long.Parse(userId));
        // User? user = await _context.Set<User>().FindAsync(long.Parse(userId));
        if (user == null)
        {
            return NotFound("Không tìm thấy user");
        }

        user.Address = model.Address;
        user.Fullname = model.FullName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.Ward = model.Ward;
        user.NationalId = model.NationalId ?? "VN";
        user.Remark = model.Remark;
        user.District = model.District;
        user.Province = model.Province;
     
        _context.Set<User>().Update(user);
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        Console.WriteLine(user);

        //// 7. Ánh xạ thông tin người dùng đã cập nhật và trả về phản hồi
        var userMapped = _mapper.Map<UserResponse>(user);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Cập nhật thành công",
            Data = userMapped
        });
        //List<long> RoleIds = await _permissionRepository.Select().Where(x => x.UserId == user.Id).Select(p => p.RoleId).ToListAsync();

        //var roles = await _roleRepository.Select().Where(x => RoleIds.Contains(x.Id)).Where(p => p.Status == 1).ToListAsync();



        //List<RoleModel> roleModels = new List<RoleModel>();
        //foreach (Role role in roles)
        //{
        //    if (role != null)
        //    {
        //        roleModels.Add(new RoleModel
        //        {
        //            Code = role.Code,
        //            Module = role.Module,
        //            Status = 1,
        //            Description = ""
        //        });
        //    }
        //}

        //var userMapped = _mapper.Map<UserResponse>(user!);

        //userMapped.roles = roleModels;


    }

    // [HttpGet("reset-password")]
    // public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    // {
    //     var isEmail = Utils.IsValidEmail(model.Username);
    //     var isPhoneNumber = Utils.IsValidPhoneNumber(model.Username);
    //
    //     if (isEmail == false && isPhoneNumber == false)
    //     {
    //         throw new ArgumentException("Email hoặc số điện thoại không hợp lệ.");
    //     }
    //     
    //     var searchParam = isEmail ? model.Username : Utils.FormatPhoneNumber(model.Username);
    //     
    //     var user = await _context
    //         .Set<User>()
    //         .SingleOrDefaultAsync(p => p.Email == searchParam || p.Phone == searchParam);
    //     
    //     if (user == null) throw new ArgumentException("Người không đã tồn tại.");
    //     
    //     user.ResetPasswordCode 
    //     
    // }

    [HttpGet]
    [Route("role/check")]
    public async Task<IActionResult> Can(string code)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (long.IsNegative(userId) || string.IsNullOrEmpty(code))
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Không quyền truy cập",
                ErrorCode = 1,
                Success = false
            });
        }

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Thông tin người dùng ko tìm thấy.",
                ErrorCode = 2,
                Success = false
            });
        }
        if(user.Role == "Admin")
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Truy xuất quyền thành công",
                Data = "Admin",
                Success = true,
                ErrorCode = 0
            });
        }

        List<long> Roles = await _permissionRepository.Select().Where(x => x.UserId == user.Id).Select(p => p.RoleId).ToListAsync();

        Role? role = await _roleRepository.Select().Where(x => Roles.Contains(x.Id))
            .Where(p => p.Code.Equals(code))
           .FirstOrDefaultAsync();

        if (role is null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Truy xuất quyền lỗi",
                ErrorCode = 1,
                Success = false
            });
        }


        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất quyền thành công",
            Data = role,
            Success = true,
            ErrorCode = 0
        });
    }

    [HttpGet]
    [Route("roles")]
    public async Task<IActionResult> GetRoleList(string module = "")
    {
        List<string> roles = new List<string>();
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

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Message = "Thông tin người dùng ko tìm thấy.",
                ErrorCode = 2,
                Success = false
            });
        }

        List<long> RoleIds = await _permissionRepository.Select().Where(x => x.UserId == user.Id).Select(p => p.RoleId).ToListAsync();

        if (module.IsNullOrEmpty())
        {
            roles = await _roleRepository.Select().Where(x => RoleIds.Contains(x.Id)).Select(p => p.Code).ToListAsync();
        }
        else
        {
            roles = await _roleRepository.Select().Where(x => RoleIds.Contains(x.Id))
                .Where(p => p.Module.Equals(module, StringComparison.OrdinalIgnoreCase)).Select(p => p.Code).ToListAsync();
        }

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất quyền thành công",
            Data = roles,
            Success = true,
            ErrorCode = 0
        });
    }

    //Captcha

    [HttpGet]
    [AllowAnonymous]
    [Route("generateCode")]
    public async Task<IActionResult> GenerateCode(string code)
    {
        Random random = new Random();
        string randomNumberInRange = random.Next(1000, 9999).ToString(); // Generates random 
        string _key = code + randomNumberInRange;
        //Lưu session
        //HttpContext.Session.SetString(_key, randomNumberInRange);
        //Console.WriteLine(randomNumberInRange);
        _iMemoryCachingService.Set<String>(_key, randomNumberInRange, 60 * 1);
        return Ok(new { session = _key });
    }
}