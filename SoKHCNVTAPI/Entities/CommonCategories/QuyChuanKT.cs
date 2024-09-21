using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// LoaiHinhToChucDN -> Gán tổ chức
[Table("QuyChuanKT", Schema = "skhcn")]
public class QuyChuanKT
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

public class QuyChuanKTDto
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

public class QuyChuanKTFilter : PaginationDto, IKeyword
{
     public string? Ma { get; set; }
     public short? TrangThai { get; set; }
     public string? Keyword { get; set; }
}