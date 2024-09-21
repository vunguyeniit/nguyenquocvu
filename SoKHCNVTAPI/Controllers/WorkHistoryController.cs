using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using Asp.Versioning;
namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/work-histories")]
[ApiVersion("1")]
public class WorkHistoryController : ControllerBase
{
    private readonly IWorkHistoryRepository _repo;

    public WorkHistoryController(IWorkHistoryRepository repository) { _repo = repository; }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationDto model)
    {
        var (items, records) = await _repo.PagingAsync(model);
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
        var item = await _repo.GetByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkHistoryDto model)
    {
        var item = await _repo.Create(model);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] WorkHistoryDto model)
    {
        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _repo.Update(id, model);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!",
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (id < 0) throw new ArgumentException("Id không hợp lệ!");
        await _repo.Delete(id);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Đã xoá!"
        });
    }
}