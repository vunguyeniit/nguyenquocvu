using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("SoHuuTriTue", Schema = "skhcn")]
public class SoHuuTriTue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public required string TenSangChe { get; set; }

    public string? LoaiSoHuu { get; set; }

    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;
  
    public required string PhanLoai { get; set; }

    public required string SoBang { get; set; }

    public string? ChuBang { get; set; }

    public string? ThongTinSoHuu { get; set; }

    public string? SangCheToanVan { get; set; }

    public string? ToChucDaiDien { get; set; }

    public string? NguoiDaiDien { get; set; }

    public string? ToChucDuDieuKien { get; set; }

    public string? CaNhanDuDieuKien { get; set; }

    public string? NgayCap { get; set; }

}

public class SoHuuTriTueDto
{
    public required string TenSangChe { get; set; }

    public string? LoaiSoHuu { get; set; }

    //public short? TrangThai { get; set; } = 1;

    public required string PhanLoai { get; set; }

    public required string SoBang { get; set; }

    public string? ChuBang { get; set; }

    public string? ThongTinSoHuu { get; set; }

    public string? SangCheToanVan { get; set; }

    public string? ToChucDaiDien { get; set; }

    public string? NguoiDaiDien { get; set; }

    public string? ToChucDuDieuKien { get; set; }

    public string? CaNhanDuDieuKien { get; set; }

    public string? NgayCap { get; set; }
}


public class SoHuuTriTueFilter : PaginationDto, IKeyword
{
    public string? ChuBang { get; set; }
    public  string? PhanLoai { get; set; }
    public string? LoaiSoHuu { get; set; }
    public  string? TenSangChe { get; set; }
    public string? SoBang { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? NgayCapNhat { get; set; }
    public string? sorted_by { get; set; }
}