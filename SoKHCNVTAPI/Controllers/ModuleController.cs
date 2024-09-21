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
[Route("/api/v{version:apiVersion}/modules")]
[ApiVersion("1")]
public class ModuleController : ControllerBase
{
    private readonly IModuleRepository _repo;
    public ModuleController(IModuleRepository repository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ModuleFilter model)
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
    public async Task<IActionResult> CreateAsync([FromBody] ModuleModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var data   =  await _repo.CreateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = data
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] ModuleModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        if (id < 0) throw new ArgumentException("Mã không hợp lệ!");
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
        if (id < 0) throw new ArgumentException("Mã không hợp lệ!");
        await _repo.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }

    //[HttpGet]
    //public async Task<IActionResult> GetWorkFlows([FromQuery] WorkflowFilter model)
    //{
    //    var (items, records) = await _repo.FilterAsync(model);
    //    return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
    //    {
    //        Message = "Truy xuất danh sách thành công!",
    //        Meta = new Meta(model, records),
    //        Data = items
    //    });
    //}
}