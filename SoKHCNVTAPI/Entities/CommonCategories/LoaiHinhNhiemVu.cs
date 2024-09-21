using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// CapQuanLy -> Gán tổ chức
[Table("LoaiHinhNhiemVu", Schema = "skhcn")]
public class LoaiHinhNhiemVu
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

    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

public class LoaiHinhNhiemVuDto
{
    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }

    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

public class LoaiHinhNhiemVuFilter : PaginationDto, IKeyword
{
     public string? Ma { get; set; }
     public short? TrangThai { get; set; }
     public string? Keyword { get; set; }
}