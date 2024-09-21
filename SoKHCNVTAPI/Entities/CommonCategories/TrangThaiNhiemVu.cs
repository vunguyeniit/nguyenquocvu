using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

//TrangThaiNhiemVu
[Table("MissionStatuses", Schema = "skhcn")]
public class TrangThaiNhiemVu : BaseEntity
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public string? Color {set; get;}
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class TrangThaiNhiemVuDto
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public string? Color {set; get;}
    
    public short? Status {set; get;}
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class TrangThaiNhiemVuFilter : PaginationDto, IKeyword
{
    public string? Name { get; set; }
    public string? Color {set; get;}
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public string? Description { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }

}
