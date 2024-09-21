using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

[Table("Groups", Schema = "skhcn")]
public class Nhom : BaseEntity
{
    [StringLength(30)]
    public required string Code { get; set; }

    [StringLength(200)]
    public required string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
}