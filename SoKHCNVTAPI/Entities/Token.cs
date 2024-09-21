using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("Tokens", Schema = "skhcn")]
public class Token
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public long UserId { get; init; }

    [StringLength(350)]
    public required string Authorization { get; set; }
    
    [StringLength(300)]
    public required string DeviceKey { get; set; }

    [StringLength(350)]
    public required string NotificationKey { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; } = Utils.getCurrentDate();
   
    public DateTime? UpdatedAt { get; set; } = Utils.getCurrentDate();
}