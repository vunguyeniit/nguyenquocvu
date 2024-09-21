using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

[Table("Documents", Schema = "skhcn")]
public class Document : BaseEntity
{
    [StringLength(50)]
    public required string Code { get; set; }

    [StringLength(300)]
    public required string Name { get; set; }
    
    [StringLength(10)]
    public required string Extension { get; set; }
    
    [StringLength(300)]
    public required string Path { get; set; }

    [StringLength(250)]
    public required string FullName { get; set; }

    public long UserId { get; set; }

    public required string Target { get; set; }

    public string? TargetCode { get; set; } = "";
}