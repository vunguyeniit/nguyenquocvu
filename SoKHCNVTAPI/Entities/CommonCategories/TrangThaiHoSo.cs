using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("FileStatuss", Schema = "skhcn")]
public class TrangThaiHoSo : BaseEntity
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public string? Color {set; get;}
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class FileStatusDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Color {set; get;}
    public short? Status {set; get;}
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class FileStatusFilter : PaginationDto, IKeyword
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Color {set; get;}
    public short? Status {set; get;}
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}