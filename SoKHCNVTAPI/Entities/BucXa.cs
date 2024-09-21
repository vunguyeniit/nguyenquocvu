using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("BucXa", Schema = "skhcn")]
public class BucXa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  long Id { get; set; }

    public  string? ho_va_ten    { get; set; }

    public string? ngay_sinh{ get; set; }
    public  string? so_cccd  { get; set; }

    public string? so_chung_chi  { get; set; }

    public string? ngay_cap    { get; set; }

    public string? ghi_chu{ get; set; }

    public string? thoi_gian  { get; set; }
    public DateTime? NgayTao { get; set; } = DateTime.Now;

    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;




}

public class BucXaDto
{
    public  long Id { get; set; }

    public string? ho_va_ten { get; set; }

    public string? ngay_sinh { get; set; }

    public string? so_cccd { get; set; }
    public string? so_chung_chi { get; set; }

    public string? ngay_cap { get; set; }

    public string? ghi_chu { get; set; }

    public string? thoi_gian { get; set; }
    public DateTime? NgayTao { get; set; } = DateTime.Now;

    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
}

public class BucXaFilter : PaginationDto, IKeyword
{
    public long Id { get; set; }
    public string? ho_va_ten { get; set; }

    public string? ngay_sinh { get; set; }

    public string? so_cccd { get; set; }
    public string? so_chung_chi { get; set; }

    public string? ngay_cap { get; set; }

    public string? ghi_chu { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? NgayCapNhat { get; set; }
    public string? start { get; set; }
    public string? end { get; set; }
}