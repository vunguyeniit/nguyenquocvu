using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Enums;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("OwnershipRepresentatives", Schema = "skhcn")]
public class OwnershipRepresentative
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [StringLength(100)]
    public required string Code { get; set; }

    [StringLength(350)]
    public required string Title { get; set; }

    public required short Type { get; set; } = (int)OwnershipRepresentativeType.Default; 
    
    public long UserId { get; set; }
    
    public long OrganizationId { get; set; }
    
    public DateTime CreatedAt { get; set; } = Utils.getCurrentDate();
    
    public DateTime UpdatedAt { get; set; }
    
    public DateTime IssuedAt { get; set; }
    
    [StringLength(2000)]
    public string? Description { get; set; }

    public required short Status { get; set; } = (int)OwnershipRepresentativeStatus.Activate;
    
    public long ApprovedBy { get; set; }
    
    public DateTime ApprovedAt { get; set; }
}