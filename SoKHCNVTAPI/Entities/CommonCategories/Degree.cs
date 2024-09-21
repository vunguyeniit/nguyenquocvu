using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("Degrees", Schema = "skhcn")]
public class Degree : BaseEntity
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class DegreeDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public short? Status { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class DegreeFilter : PaginationDto, IKeyword
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}