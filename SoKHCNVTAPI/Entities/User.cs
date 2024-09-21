using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

[Table("Users", Schema = "skhcn")]
public class User : BaseEntity
{
    [StringLength(60)]
    public string? Email { get; set; }

    [StringLength(15)]
    public string? Phone { get; set; }

    [StringLength(30)]
    public required string Password { get; set; }

    [StringLength(300)]
    public string? Token { get; set; }

    [StringLength(500)]
    public string? RefreshToken { get; set; }

    public bool? IsLocked { get; set; } = false;

    [StringLength(20)]
    public required string NationalId { get; set; }

    [StringLength(100)]
    public string? Fullname { get; set; }

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Province { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [StringLength(50)]
    public string? Ward { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }

    [StringLength(100)]
    public string? Position { get; set; }

    public long CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }

    [StringLength(500)]
    public string? Remark { get; set; }

    [StringLength(50)]
    public required string Code { get; set; }

    public DateTime? LastLogin { get; set; }
    public long? GroupId { get; set; }
    public Nhom? Group { get; set; }
    public int? LoginFailed { get; set; }
    public string? Menus { get; set; }
}