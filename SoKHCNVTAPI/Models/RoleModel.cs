using System;
using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class RoleModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "{0} không được để trống")]
    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Code { get; set; }

    [StringLength(200, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public string? Description { get; set; }

    public short Status { get; set; }

    public string? Module { get; set; }
}

public class RoleUserModel
{
    public List<long>? RoleIds { get; set; }
    public long UserId { get; set; }
    public long GroupId { get; set; }
   
}

public class RoleWorkflowModel
{
    public List<long>? AddRoles { get; set; }
    public List<long>? UpdateRoles { get; set; }
    public List<long>? ApproveRoles { get; set; }
    public List<long>? SignRoles { get; set; }
    public long WorkflowTemplateId { get; set; }
}

public class RoleFilter : PaginationDto
{
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }

}

