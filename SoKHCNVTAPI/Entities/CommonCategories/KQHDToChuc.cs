using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;
//Ket qua hoat dong to chuc
[Table("KQHDToChuc", Schema = "skhcn")]
public class KQHDToChuc
{
    [Key]
    public long Id { get; set; }
    public long? MaToChuc { get; set; }

    [StringLength(500)]
    public  string? SoHuuTriTue { get; set; }
    
    [StringLength(500)]
    public  string? SangKien { get; set; }
    
    [StringLength(500)]
    public string? SanPhamCongNgheUngDung { get; set; }
    public string? CongBoQuocTe { get; set; }
    public string? CongBoTrongNuoc { get; set; }
    public string? ChuyenGiaoCongNghe { get; set; }
    public string? KQHDKHCNKhac { get; set; }

    public DateTime? NgayTao { get; set; }
    public DateTime? ThoiGian { get; set; }
}

public class KQHDToChucDto
{
    public long Id { get; set; }
    public long? MaToChuc { get; set; }

    [StringLength(500)]
    public required string SoHuuTriTue { get; set; }

    [StringLength(500)]
    public required string SangKien { get; set; }

    [StringLength(500)]
    public string? SanPhamCongNgheUngDung { get; set; }
    public string? CongBoQuocTe { get; set; }
    public string? CongBoTrongNuoc { get; set; }
    public string? ChuyenGiaoCongNghe { get; set; }
    public string? KQHDKHCNKhac { get; set; }
}

public class KQHDToChucFilter : PaginationDto, IKeyword
{
    public long? MaToChuc { get; set; }
    public string? SoHuuTriTue { get; set; }
    public  string? SangKien { get; set; }
    public string? SanPhamCongNgheUngDung { get; set; }

    public string? Keyword { get; set; }
}
