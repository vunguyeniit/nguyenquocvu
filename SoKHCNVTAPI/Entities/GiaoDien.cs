using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("GiaoDien", Schema = "skhcn")]
public class GiaoDien
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public required long TaiKhoan { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }
    
    [StringLength(250)]
    public required string Ten { get; set; }

    [StringLength(20000)]
    public required string DuLieu { get; set; }

    public DateTime? CapNhat { get; set; } = Utils.getCurrentDate();
}