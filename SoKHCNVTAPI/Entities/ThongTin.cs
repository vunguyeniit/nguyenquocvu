using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("ThongTin", Schema = "skhcn")]
public class ThongTin
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public required string MaSoQuocGia { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public DateTime? ThoiGian { get; set; }

    public required string TenQuocGia { get; set; }

    public string? ThongKeNhanLuc { get; set; }

    public string? ThongKeKinhPhi { get; set; }

    public string? ThongKeKetQua { get; set; }

    public string? ThongKeHoatDong { get; set; }
    //public DateTime NgayTao { get; set; } = DateTime.Now;
    //public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.UtcNow; // Lưu theo UTC
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.UtcNow; // Lưu theo UTC
}

public class ThongTinDto
{
    public required string MaSoQuocGia { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public DateTime? ThoiGian { get; set; }

    //public DateTime NgayTao { get; set; } = DateTime.Now;
    //public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.UtcNow; // Lưu theo UTC
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.UtcNow; // Lưu theo UTC
    public required string TenQuocGia { get; set; }
    public string? ThongKeNhanLuc { get; set; }
    public string? ThongKeKinhPhi { get; set; }
    public string? ThongKeKetQua { get; set; }
    public string? ThongKeHoatDong { get; set; }
}

public class ThongTinFilter : PaginationDto, IKeyword
{
    public string? TenQuocGia { get; set; }
    public string? MaSoQuocGia { get; set; }
    public DateTime? ThoiGian { get; set; }
    public string? ThongKeNhanLuc { get; set; }
    public string? ThongKeKinhPhi { get; set; }
    public string? ThongKeKetQua { get; set; }
    public string? ThongKeHoatDong { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
}