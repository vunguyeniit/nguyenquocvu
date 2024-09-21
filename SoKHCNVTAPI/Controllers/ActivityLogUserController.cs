using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/activity-logs-user")]
[ApiVersion("1")]
public class ActivityLogUserController : BaseController
{
    private readonly IActivityLogUserRepository _repository;

    public ActivityLogUserController(IActivityLogUserRepository repository, IRepository<Permission> permissionRepository, IUserRepository userRepository,
        IRepository<Role> rolePermission) : base(permissionRepository, rolePermission, userRepository)
    { _repository = repository; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ActivityLogUserFilter model)
    {
        var (items, records) = await _repository.FilterAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách lịch sử hoạt động thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }
}