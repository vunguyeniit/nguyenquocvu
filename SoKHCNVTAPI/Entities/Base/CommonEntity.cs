using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Entities.Base;

public class CommonEntity : BaseEntity
{
    [StringLength(200)]
    public required string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
}