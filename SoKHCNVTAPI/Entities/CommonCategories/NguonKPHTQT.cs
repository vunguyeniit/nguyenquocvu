using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// Nguồn cấp kinh phí hợp tác quốc tế về KH&CN - TT15
[Table("NguonKPHTQT", Schema = "skhcn")]
public class NguonKPHTQT
{
    [Key]
    public long Id { get; init; }

    [StringLength(200)]
    public required string Ten { get; set; }
     
    [StringLength(50)]
    public required string Ma { get; set; }
    
    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }
    
    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

public class NguonKPHTQTDto
{
    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }

    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

    public class NguonKPHTQTFilter : PaginationDto, IKeyword
{
     public string? Ma { get; set; }
     public short? TrangThai { get; set; }
     public string? Keyword { get; set; }
}