using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

//Don Vi
[Table("Departments", Schema = "skhcn")]
public class DonVi : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }

    public string? Manager { get; set; }

    public string? Description { get; set; }
    public long? CreatedBy { get; set; } = 0;
    public long? UpdatedBy { get; set; } = 0;
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class DonViDto
{
    [StringLength(200)]
    public required string Name { get; set; }

    [StringLength(50)]
    public required string Code { get; set; }
    
    [StringLength(250)]
    public string? Manager { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }
    
    public short? Status { get; set; } = 1;
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class DonViFilter : PaginationDto, IKeyword
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public short? Status { get; set; }
    public string? Description { get; set; }
    public string? Keyword { get; set; }
    public string? CreatedAt { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? UpdatedAt { get; set; }
}