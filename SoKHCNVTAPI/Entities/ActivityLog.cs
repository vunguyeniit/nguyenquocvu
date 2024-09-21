using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("ActivityLogs", Schema = "skhcn")]
public class ActivityLog
{
    [Key]
    [StringLength(60)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [StringLength(2000)]
    public required string Contents { get; set; }
    
    [StringLength(3000)]
    public required string Params { get; set; }

    [StringLength(250)]
    public required string FullName { get; set; }

    public long UserId { get; set; }

    public required string Target { get; set; }

    public string? TargetCode { get; set; } = "";

    [Required]
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
}

public class ActivityLogDto
{
    public required string Contents { get; set; }
    public required string Params { get; set; }
    public  string? FullName { get; set; }
    public long UserId { get; set; }
    public required string Target { get; set; }
    public string? TargetCode { get; set; }
}

public class ActivityLogFilter : PaginationDto, IKeyword
{
    public string? Contents { get; set; }
    public string? FullName { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? CreatedAt { get; set; }
}
