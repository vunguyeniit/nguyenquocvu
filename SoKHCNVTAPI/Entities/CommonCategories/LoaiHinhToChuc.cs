using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// LoaiHinhToChuc -> Gán tổ chức
[Table("LoaiHinhToChuc", Schema = "skhcn")]
public class LoaiHinhToChuc
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
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;
}

public class LoaiHinhToChucDto
{
    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }
 
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;
}

public class LoaiHinhToChucFilter : PaginationDto, IKeyword
{
     public string? Ma { get; set; }
    public  string? MoTa { get; set; }
    public short? TrangThai { get; set; }
    public  string? Ten { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? NgayTao { get; set; }
    public string? NgayCapNhat { get; set; }
}