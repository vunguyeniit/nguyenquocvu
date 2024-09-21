using SoKHCNVTAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;
public class NhomDto
{
    [Required(ErrorMessage = "{0} không được để trống")]
    [StringLength(200, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "{0} không được để trống")]
    [StringLength(30, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Code { get; set; }

    [StringLength(500, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public string? Description { get; set; }
    public short? Status { get; set; } = 1;
    public IList<long>? userIds { get; set; }
    public IList<User>? Users { get; set; }
}

public class NhomFilterDto : PaginationDto
{
    public string? Keyword { get; set; } = null;
    public short? Status { get; set; } = 1;
    public string? Description { get; set; }
    public  string? Name { get; set; }
}