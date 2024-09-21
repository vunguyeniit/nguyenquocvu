using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("TrinhDoDaoTao", Schema = "skhcn")]
public class TrinhDoDaoTao : BaseEntity
{
     [StringLength(50)]
     public required string Code { get; set; }
     public required string Name { get; set; }
     public string? Description { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class TrinhDoDaoTaoDto
{
     [StringLength(50)]
     public required string Code { get; set; }
     public required string Name { get; set; }
     public string? Description { get; set; }
     public short? Status { get; set; } = 1;
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class TrinhDoDaoTaoFilter : PaginationDto, IKeyword
{
     [StringLength(50)]
     public string? Code { get; set; }
     public string? Name { get; set; }
     public string? Description { get; set; }
     public short? Status { get; set; }
     public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}

