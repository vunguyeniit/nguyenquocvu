using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/stepes")]
[ApiVersion("1")]
public class StepController : BaseController
{
    private readonly IStepRepository _repo;
    private readonly IStepStatusRepository _stepStatusRepository;


    public StepController(
        IStepRepository repository,
        IStepStatusRepository stepStatusRepository,
        IUserRepository userRepository, IRepository<Permission> permissionRepository,
        IRepository<Role> rolePermission
        ) : base(permissionRepository, rolePermission, userRepository) {
        _repo = repository;
        _stepStatusRepository = stepStatusRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] StepFilter model)
    {
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
        var item = await _repo.GetByIdAsync(id);
        if (item == null) throw new ArgumentException("Không tìm thấy!");
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] StepModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new BaseResponse
        {
            Message = "Tạo mới thành công!",
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] StepModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _repo.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }

    [HttpPut("status/{id:long}")]
    public async Task<IActionResult> UpdateStepStatus(long id, [FromBody] StepStatusModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        //var isCheck = await Can(ActionTypeEnum.Apporve);

        //if (!isCheck) return PermissionMessage();


        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _stepStatusRepository.UpdateStepStatus(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!"
        });
    }

    [HttpDelete("status/{id:long}")]
    public async Task<IActionResult> Delete(long id, [FromBody] StepStatusModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _stepStatusRepository.Delete(model.TargetId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xoá thành công!"
        });
    }
}