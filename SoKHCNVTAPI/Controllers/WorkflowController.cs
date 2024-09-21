using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;
using SoKHCNVTAPI.Repositories;
using System.Security.Claims;
using Asp.Versioning;
using SoKHCNVTAPI.Helpers;
using Microsoft.IdentityModel.Tokens;
using SoKHCNVTAPI.Enums;
using DocumentFormat.OpenXml.Bibliography;

namespace SoKHCNVTAPI.Controllers;

[ApiController]
[Authorize]
[Route("/api/v{version:apiVersion}/workflow")]
[ApiVersion("1")]
public class WorkflowController : BaseController
{
    private readonly IWorkflowRepository _repoWorkflow;
    private readonly IUserRepository _userRepository;

    public WorkflowController(IWorkflowRepository repository, IRepository<Permission> permissionRepository,
        IRepository<Role> rolePermission, IUserRepository userRepository) : base(permissionRepository, rolePermission, userRepository)
    {
        _repoWorkflow = repository;
        _userRepository = userRepository;
    }

    #region Dự án Công Nghê
    [HttpGet("du-an")]
    public async Task<IActionResult> GetProjects([FromQuery] DuAnFilter model)
    {
        var (items, records) = await _repoWorkflow.GetProjectsAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách dự án thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("du-an/{id:long}")]
    public async Task<IActionResult> GetDuAn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repoWorkflow.GetDuAnByIdAsync(id);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("du-an")]
    public async Task<IActionResult> CreateDuAn([FromBody] DuAnModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repoWorkflow.CreateDuAnAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới dự án thành công!",
            Data = item
        });
    }
    

    [HttpPut("du-an/{id:long}")]
    public async Task<IActionResult> EditDuAn(long id, [FromBody] DuAnModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateDuAnAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật dự án thành công!",
        });
    }

    [HttpDelete("du-an/{id:long}")]
    public async Task<IActionResult> DeleteDuAn(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool ret = await _repoWorkflow.DeleteDuAnAsync(id, userId);
        if (ret)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Xoá dự án thành công!"
            });
        }
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xoá dự án lỗi!",
            ErrorCode = 1,
            Success = false
        });
    }

    #endregion

    #region Category
    [HttpGet("category")]
    public async Task<IActionResult> GetCategories([FromQuery] WorkflowCategoryFilter model)
    {
        var (items, records) = await _repoWorkflow.GetCategoryAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách danh mục thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("category/{id:long}")]
    public async Task<IActionResult> GetCategory(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repoWorkflow.GetCategoryByIdAsync(id);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost("category")]
    public async Task<IActionResult> CreateCategory([FromBody] WorkflowCategoryModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repoWorkflow.CreateCategoryAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới danh mục thành công!",
            Data = item
        });
    }

    [HttpPut("category/{id:long}")]
    public async Task<IActionResult> EditCategory(long id, [FromBody] WorkflowCategoryModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateCategoryAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật danh mục thành công!",
        });
    }

    [HttpDelete("category/{id:long}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool ret = await _repoWorkflow.DeleteCategoryAsync(id, userId);
        if (ret)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Xoá danh mục quy trình thành công!"
            });
        }
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xoá danh mục quy trình lỗi!",
            ErrorCode = 1,
            Success = false
        });
    }

    #endregion

    #region Quy Trinh Mau Workflow Template
    [HttpGet("template")]
    public async Task<IActionResult> GetTemplates([FromQuery] WorkflowFilter model)
    {
        var (items, records) = await _repoWorkflow.GetTemplateAsync(model);
        return StatusCode(StatusCodes.Status200OK, new PaginationBaseResponse
        {
            Message = "Truy xuất danh sách quy trình mẫu thành công!",
            Meta = new Meta(model, records),
            Data = items
        });
    }

    [HttpGet("template/{id:long}")]
    public async Task<IActionResult> GetTemplate(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repoWorkflow.GetTemplateByIdAsync(id);

        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất quy trình mãu thành công!",
            Data = item
        });
    }

    [HttpPost("template")]
    public async Task<IActionResult> CreateTemplate([FromBody] WorkflowTemplateModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repoWorkflow.CreateTemplateAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới quy trình mẫu thành công!",
            Data = item
        });
    }


    [HttpPut("template/role/{id:long}")]
    public async Task<IActionResult> SetRoleActionTemplate(long id, [FromBody] WorkflowTemplateModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateTemplateRoleAndAction(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật quyền và sự kiện thành công!",
        });
    }

    [HttpDelete("template/{id:long}")]
    public async Task<IActionResult> DeleteTemplate(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool ret = await _repoWorkflow.DeleteTemplateAsync(id, userId);
        if (ret)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Xoá quy trình mẫu thành công!"
            });
        }
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xoá quy trình mẫu lỗi!",
            ErrorCode = 1,
            Success = false
        });
    }

    #endregion

    #region Step template
    [HttpPost("step")]
    public async Task<IActionResult> CreateStep([FromBody] WorkflowStepTemplateModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var item = await _repoWorkflow.CreateTemplateStepAsync(model, userId);

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }


    [HttpPut("step/{id:long}")]
    public async Task<IActionResult> UpdateStep(long id, [FromBody] WorkflowStepTemplateModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }

        if (model.MaQuyTrinhMau <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Mã quy trình không hợp lệ.",
                ErrorCode = 1,
                Success = false
            });
        }

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateTemplateStepAsync(id, model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Data = "",
            Message = "Cập nhật bước thành công!"
        });
    }


    [HttpDelete("step/{id:long}")]
    public async Task<IActionResult> DeleteStep(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        bool ret = await _repoWorkflow.DeleteStepAsync(id, userId);
        if (ret)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Xóa bước thành công!",
                ErrorCode = 0,
                Success = true
            });
        }
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xóa bước lỗi!",
            ErrorCode = 2,
            Success = false
        });
    }

    #endregion

    #region Workflow Quy trình 

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] WorkflowFilter model)
    {
        var (items, records) = await _repoWorkflow.FilterAsync(model);
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
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var item = await _repoWorkflow.GetByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Truy xuất thành công!",
            Data = item
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] WorkflowModel model)
    {
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        // Kiem tra quyen
        if (model.MaDMQT == 1) // TT09
        {
            string codeQuyen = "";
            var QTMau = await _repoWorkflow.GetTemplateByIdAsync(model.MaQuyTrinhMau);
            if (QTMau != null)
            {
                if (QTMau.Ma == "01-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 01 TT09";
                }
                else if (QTMau.Ma == "02-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 02 TT09";
                }
                else if (QTMau.Ma == "03-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 03 TT09";
                }
                else if (QTMau.Ma == "04-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 04 TT09";
                }
                else if (QTMau.Ma == "05-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 05 TT09";
                }
                else if (QTMau.Ma == "06-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 06 TT09";
                }
                else if (QTMau.Ma == "07-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 07 TT09";
                }
                else if (QTMau.Ma == "08-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 08 TT09";
                }
                else if (QTMau.Ma == "09-TT09")
                {
                    codeQuyen = "Thẩm định mẫu 09 TT09";
                }
            }
            if (codeQuyen != "")
            {
                if (!await Can(codeQuyen, "TT09")) return PermissionMessage();
            }
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "Tài khoản không tồn tại",
                ErrorCode = 2,
                Success = false
            });
        }
        //if(user.MaNhom > 0)
        //{
        //    var nhom = _userRepository.GetNhom((long)user.MaNhom);
        //    if(nhom != null)
        //    {

        //    }
        //}
        var item = await _repoWorkflow.CreateWorkflowAsync(model, userId);
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Tạo mới thành công!",
            Data = item
        });
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Edit(long id, [FromBody] WorkflowModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật thành công!",
        });
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.DeleteAsync(id, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Xoá quy trình thành công!"
        });
    }

    #endregion

    [HttpPost("status/{id:long}")]
    public async Task<IActionResult> UpdateWorkflow(long id, [FromBody] WorkflowStatusModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status200OK, new BaseResponse
            {
                Message = "ID quy trình không hợp lệ!",
                ErrorCode = 1,
                Success = false
            });
        }
        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _repoWorkflow.UpdateWorkflowAsync(id, model, userId);
        return StatusCode(StatusCodes.Status200OK, new BaseResponse
        {
            Message = "Cập nhật trạng thái quy trình thành công!",
        });
    }


    [HttpPost("step/approve/{id:long}")]
    public async Task<IActionResult> SaveStepLog(long id, WorkflowStepModel model)
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "Mã ID không chính xác.",
                ErrorCode = 1,
                Success = false
            });
        }


        if (model.NoiDung.IsNullOrEmpty())
        {
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "Nội dung yêu cầu nhập.",
                ErrorCode = 2,
                Success = false
            });
        }


        if (model.PhanHoi.IsNullOrEmpty())
        {
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "Phản hồi yêu cầu nhập.",
                ErrorCode = 3,
                Success = false
            });
        }

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        UserResponse? user = await _userRepository.GetByIdAsync(userId);
        string fullname = "";
        if (user != null)
        {
            fullname = user.FullName != null ? user.FullName : "";
        }
        var workflowStep = await _repoWorkflow.GetStepById(id);
        if (workflowStep != null)
        {
            if (workflowStep.TrangThai == (short)WorkflowStepStatus.KhoiTao && model.TrangThaiXL == (short)WorkflowStepStatus.Duyet)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse
                {
                    Message = "Bước xử lý yêu cầu chờ duyệt trước khi duyệt.",
                    ErrorCode = 4,
                    Success = true
                });
            }
            else if (workflowStep.TrangThai == (short)WorkflowStepStatus.ChoDuyet && model.TrangThaiXL == (short)WorkflowStepStatus.DaKy)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse
                {
                    Message = "Yêu cầu duyệt trước khi xác nhận đã ký chữ ký số.",
                    ErrorCode = 4,
                    Success = true
                });
            }
            else if (model.MaChuKy.IsNullOrEmpty() && model.TrangThaiXL == (short)WorkflowStepStatus.DaKy)
            {
                return StatusCode(StatusCodes.Status200OK, new ApiResponse
                {
                    Message = "Yêu cầu ký chữ ký số.",
                    ErrorCode = 5,
                    Success = true
                });
            }

            model.Id = id;
            var stepLog = await _repoWorkflow.UpdateStepAsync(workflowStep, model, userId, fullname);

            return StatusCode(StatusCodes.Status200OK, new ApiResponse
            {
                Data = stepLog,
                Message = "Lưu thông tin phản hồi thành công.",
                ErrorCode = 0,
                Success = true
            });
        }
        return StatusCode(StatusCodes.Status200OK, new ApiResponse
        {
            Message = "Bước xử lý không tìm thấy.",
            ErrorCode = 6,
            Success = false
        });

        
    }

    //Check step 
    [HttpGet("step/check/{id:long}")]
    public async Task<IActionResult> CheckStep(long id, string Action = "")
    {
        if (id <= 0)
        {
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "Mã ID không chính xác.",
                ErrorCode = 1,
                Success = false
            });
        }

        var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        WorkflowStep item = await _repoWorkflow.GetStepById(id);
        if (item == null)
        {
            return StatusCode(StatusCodes.Status201Created, new ApiResponse
            {
                Message = "Bước quy trình không tìm thấy.",
                ErrorCode = 1,
                Success = false
            });
        }

        UserResponse? user = await _userRepository.GetByIdAsync(userId);
        if (user != null)
        {
            //if (user.Role != null && user.Role.ToLower() == "admin")
            //{
                return StatusCode(StatusCodes.Status201Created, new ApiResponse
                {
                    Message = "Kiểm tra bước thành công!",
                    Data = item,
                    Action = Action,
                    ErrorCode = 0,
                    Success = true
                });
            //}
        }
        //var workflow = await _repoWorkflow.GetByIdAsync(item.WorkflowId);
        //if (workflow != null)
        //{
        //    if (userId == workflow.NguoiCapNhat) // Owner
        //    {
        //        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        //        {
        //            Message = "Kiểm tra bước thành công!",
        //            Data = item,
        //            Action = Action,
        //            ErrorCode = 0,
        //            Success = true
        //        });
        //    }
        //    if (workflow.Quyen != null)
        //    {
        //        string quyen = workflow.Quyen;
        //        if (!string.IsNullOrEmpty(quyen))
        //        {
        //            bool ret = await _repoWorkflow.CheckStepPermission(workflow, quyen, Action, userId);
        //            if (ret)
        //            {
        //                return StatusCode(StatusCodes.Status201Created, new ApiResponse
        //                {
        //                    Message = "Kiểm tra bước thành công!",
        //                    Data = item,
        //                    Action = Action,
        //                    ErrorCode = 0,
        //                    Success = true
        //                });
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status201Created, new ApiResponse
        //                {
        //                    Message = "Kiểm tra bước thất bại: " + quyen,
        //                    ErrorCode = 2,
        //                    Success = false
        //                });
        //            }
        //        }
        //    }
        //}

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Kiểm tra bước thất bại, Quyền rỗng.",
            ErrorCode = 2,
            Success = false
        });

    }

    // Xu ly action cho Step
    [HttpPost("step/action/{id:long}")]
    public async Task<IActionResult> DoActionStep(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        Workflow item = await _repoWorkflow.GetByIdAsync(id);

        if (item != null)
        {
            if (item.SuKien != null)
            {
                string thaoTac = item.SuKien;
                if (!string.IsNullOrEmpty(thaoTac))
                {

                    bool ret = await _repoWorkflow.DoAction(thaoTac, item);
                    if (ret)
                    {
                        return StatusCode(StatusCodes.Status201Created, new ApiResponse
                        {
                            Message = "Xứ lý thao tác cho bước thành công!",
                            Data = item,
                            ErrorCode = 0,
                            Success = true
                        });
                    }
                }
            }
        }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Xứ lý thao tác cho bước lỗi!",
            Data = item,
            ErrorCode = 2,
            Success = false
        });
    }

    #region Export PDF TT 09

    // Xu ly action cho Step
    [HttpPost("xuat/Mau01TT9/{id:long}")]
    public async Task<IActionResult> ExportMau1(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            DuAn duan = await _repoWorkflow.GetDuAnByIdAsync(workflow.DuAnId);
            if (duan != null)
            {
                PDFHelper pDF = new PDFHelper();
                string pdfPath = Path.Combine("tmp/", "Mau01_TT9_" + DateTime.Now.Ticks + ".pdf");
                pDF.XuatMau01TT9(pdfPath, workflow, duan);
                var stream = System.IO.File.OpenRead(pdfPath);
                return File(stream, "application/octet-stream", pdfPath);
            }
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau02TT9/{id:long}")]
    public async Task<IActionResult> ExportMau02(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau02_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau02TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03TT9/{id:long}")]
    public async Task<IActionResult> ExportMau03(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau03TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau04TT9/{id:long}")]
    public async Task<IActionResult> ExportMau04(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau04_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau04TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau05TT9/{id:long}")]
    public async Task<IActionResult> ExportMau05(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau05TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau06TT9/{id:long}")]
    public async Task<IActionResult> ExportMau06(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau06_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau06TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau07TT9/{id:long}")]
    public async Task<IActionResult> ExportMau07(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau07_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau07TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau08TT9/{id:long}")]
    public async Task<IActionResult> ExportMau08(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau08_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau08TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau09TT9/{id:long}")]
    public async Task<IActionResult> ExportMau09(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau09_TT9_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau09TT9(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    #endregion

    #region Export PDF TT 13

    [HttpPost("xuat/Mau01ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau01ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau01A_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau1ATT13(pdfPath, workflow);

            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    
    [HttpPost("xuat/Mau01BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau01BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau01B_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau1BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau02ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau02ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau02A_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau2ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau02BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau02BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau02B_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau2BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau02CTT13/{id:long}")]
    public async Task<IActionResult> ExportMau02CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau02C_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau2CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau03ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03A_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau3ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau03BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03B_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau3BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03CTT13/{id:long}")]
    public async Task<IActionResult> ExportMau03CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03C_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau3CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03DTT13/{id:long}")]
    public async Task<IActionResult> ExportMau03DTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03D_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau3DTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03DZTT13/{id:long}")]
    public async Task<IActionResult> ExportMau03DDTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03D_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau3DDTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau04ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau04ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau04A_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau4ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau04BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau04BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau04B_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau4BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau04CTT13/{id:long}")]
    public async Task<IActionResult> ExportMau04CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau04C_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau4CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau05ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau05ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05A_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau5ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau05BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau05BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau5BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau05CTT13/{id:long}")]
    public async Task<IActionResult> ExportMau05CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau5CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau05DTT13/{id:long}")]
    public async Task<IActionResult> ExportMau05DTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05d_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau5DTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau06ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau06ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau06a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau6ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau06BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau06BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau06b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau6BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau07ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau07ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau07a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau7ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    
    [HttpPost("xuat/Mau07BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau07BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau07b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau7BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }


        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau07CTT13/{id:long}")]
    public async Task<IActionResult> ExportMau07CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau07c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau7CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }


        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau08ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau08ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau08a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau8ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau08BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau08BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau08b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau8BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau09ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau09ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau09a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau9ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau09BTT13/{id:long}")]
    public async Task<IActionResult> ExportMau09BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau09b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau9BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
     
    [HttpPost("xuat/Mau10ATT13/{id:long}")]
    public async Task<IActionResult> ExportMau10ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau10a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau10ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
      
    [HttpPost("xuat/Mau10bTT13/{id:long}")]
    public async Task<IActionResult> ExportMau10bTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau10b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau10BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau10cTT13/{id:long}")]
    public async Task<IActionResult> ExportMau10cTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau10c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau10CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
   
    [HttpPost("xuat/Mau11aTT13/{id:long}")]
    public async Task<IActionResult> XuatMau11ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau11a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau11ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau11bTT13/{id:long}")]
    public async Task<IActionResult> XuatMau11BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau11b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau11BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau12TT13/{id:long}")]
    public async Task<IActionResult> XuatMau12TT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau12_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau12TT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau13aTT13/{id:long}")]
    public async Task<IActionResult> XuatMau13ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau13a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau13ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }


    [HttpPost("xuat/Mau13bTT13/{id:long}")]
    public async Task<IActionResult> XuatMau13BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau13b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau13BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau13cTT13/{id:long}")]
    public async Task<IActionResult> XuatMau13CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau13c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau13CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau14TT13/{id:long}")]
    public async Task<IActionResult> XuatMau14TT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau14_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau14TT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau15aTT13/{id:long}")]
    public async Task<IActionResult> XuatMau15ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau15a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau15ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau15bTT13/{id:long}")]
    public async Task<IActionResult> XuatMau15BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau15b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau15BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau15cTT13/{id:long}")]
    public async Task<IActionResult> XuatMau15CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau15c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau15CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau15dTT13/{id:long}")]
    public async Task<IActionResult> XuatMau15DTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau15d_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau15DTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau16aTT13/{id:long}")]
    public async Task<IActionResult> XuatMau16ATT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau16a_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau16ATT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau16bTT13/{id:long}")]
    public async Task<IActionResult> XuatMau16BTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau16b_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau16BTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau16cTT13/{id:long}")]
    public async Task<IActionResult> XuatMau16CTT13(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau16c_TT13_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau16CTT13(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }






    #endregion

    #region Export PDF TT 15

    [HttpPost("xuat/Mau01TT15/{id:long}")]
    public async Task<IActionResult> ExportMau01TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau01_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau01TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau02TT15/{id:long}")]
    public async Task<IActionResult> ExportMau02TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau02_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau02TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau03TT15/{id:long}")]
    public async Task<IActionResult> ExportMau03TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau03_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau03TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }
        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau04TT15/{id:long}")]
    public async Task<IActionResult> ExportMau04TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau04_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau04TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau05TT15/{id:long}")]
    public async Task<IActionResult> ExportMau05TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau05_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau05TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }
    [HttpPost("xuat/Mau06TT15/{id:long}")]
    public async Task<IActionResult> ExportMau06TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau06_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau06TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau10TT15/{id:long}")]
    public async Task<IActionResult> ExportMau10TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau10_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau10TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau07TT15/{id:long}")]
    public async Task<IActionResult> ExportMau07TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau07_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau07TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau08TT15/{id:long}")]
    public async Task<IActionResult> ExportMau08TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau08_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau08TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }

    [HttpPost("xuat/Mau09TT15/{id:long}")]
    public async Task<IActionResult> ExportMau09TT15(long id)
    {
        //var userId = long.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var workflow = await _repoWorkflow.GetByIdAsync(id);
        if (workflow != null)
        {
            PDFHelper pDF = new PDFHelper();
            string pdfPath = Path.Combine("tmp", "Mau09_TT15_" + DateTime.Now.Ticks + ".pdf");
            string pdf = pDF.XuatMau09TT15(pdfPath, workflow);
            var stream = System.IO.File.OpenRead($"{Path.Combine(Directory.GetCurrentDirectory(), pdf)}");
            return File(stream, "application/octet-stream", pdfPath);
        }

        return StatusCode(StatusCodes.Status201Created, new ApiResponse
        {
            Message = "Quy trình không tìm thấy!",
            ErrorCode = 2,
            Success = false
        });
    }


    #endregion
}