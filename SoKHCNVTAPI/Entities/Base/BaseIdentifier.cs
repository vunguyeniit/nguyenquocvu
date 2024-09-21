using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Entities.Base;

public class BaseIdentifier : CommonEntity
{
    [StringLength(50)]
    [Required]
    public required string Code { get; set; }
}