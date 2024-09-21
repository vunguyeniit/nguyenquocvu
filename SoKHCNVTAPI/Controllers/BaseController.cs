using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Controllers;

[EnableCors("CorsPolicy")]
[Controller]
public class BaseController : ControllerBase
{
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly IUserRepository _userRepository; 

    public BaseController(IRepository<Permission> permissionRepository, IRepository<Role> rolePermission, IUserRepository userRepository)
    {
        _roleRepository = rolePermission;
        _permissionRepository = permissionRepository;
        _userRepository = userRepository;
    }

    public async Task<bool> Can(string code, string module = "")
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (userId == 0 || string.IsNullOrEmpty(code)) return false;
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) return false;
        if (user.Role != null)
        {
            if (user.Role.ToLower() ==  "admin" || user.Role.ToLower() == "sa") return true;
        }

        if (user.Role.ToLower() == "user")
        {
            // Kiểm tra quyền cụ thể
            return false;
        }
        List<long> Roles = await _permissionRepository.Select().Where(x => x.UserId == userId).Select(p => p.RoleId).ToListAsync();
        if (Roles != null && Roles.Count > 0)
        {
            Role? role = await _roleRepository.Select().Where(x => Roles.Contains(x.Id))
                .Where(p => p.Code.ToLower().Equals(code.ToLower()))
                //.Where(p => p.Module.ToLower().Equals(module.ToLower()))
               .FirstOrDefaultAsync();

            if (role != null)
            {
                return true;
            }
        }

        if (user.GroupId > 0)
        {
            List<long> nhomRoles = await _permissionRepository.Select().Where(x => x.GroupId == user.GroupId).Select(p => p.RoleId).ToListAsync();
            Role? role = await _roleRepository.Select().Where(x => nhomRoles.Contains(x.Id))
                .Where(p => p.Code.ToLower().Equals(code.ToLower()))
                //.Where(p => p.Module.ToLower().Equals(module.ToLower()))
                .FirstOrDefaultAsync();

            if (role != null)
            {
                return true;
            }
        }

        return false;
    }

    public async Task<List<string>> GetRoleList(long userId, long groupId, string module = "")
    {
        List<string> roles = new List<string>();
        //if (userId <= 0)
        //{
        //    userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //}
        //if (long.IsNegative(userId)) return roles;
        //var user = await _userRepository.GetByIdAsync(userId);
        //if (userId == 0) return roles;
        List<long> roleIds = new List<long>();
        if (groupId > 0)
        {
            roleIds = await _permissionRepository.Select().Where(x => x.GroupId == groupId).Select(p => p.RoleId).ToListAsync();
        } else
        {
            roleIds = await _permissionRepository.Select().Where(x => x.UserId == userId).Select(p => p.RoleId).ToListAsync();
        }
       

        if(module.IsNullOrEmpty())
        {
            roles = await _roleRepository.Select().Where(x => roleIds.Contains(x.Id)).Select(p => p.Code).ToListAsync();
        } else
        {
            roles = await _roleRepository.Select().Where(x => roleIds.Contains(x.Id))
                .Where(p => p.Module.Equals(module, StringComparison.OrdinalIgnoreCase)).Select(p => p.Code).ToListAsync();
        }

        return roles;
    }

    public IActionResult PermissionMessage()
    {
        //throw new UnauthorizedAccessException("Không có quyền truy cập !!!");
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Không có quyền truy cập.",
            ErrorCode = 403,
            Success = false
        });

    }
}