using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("CongBo", Schema = "skhcn")]
public class CongBo
{
    [Key]
    public long Id { get; init; }

    [StringLength(50)] public required string ChiSoDeMuc { get; set; }

    [StringLength(350)] public string? LinhVucNghienCuu { get; set; }
    [StringLength(350)] public string? DangTaiLieu { get; set; }

    [StringLength(350)] public string? TacGia { get; set; }

    [StringLength(250)] public required string NhanDe { get; set; }

    [StringLength(350)] public string? NguonTrich { get; set; }

    [StringLength(50)] public string? NamXuatBan { get; set; }

    [StringLength(250)] public string? So { get; set; }
    [StringLength(250)] public string? Trang { get; set; }
    [StringLength(250)] public string? ISSN { get; set; }
    [StringLength(250)] public string? TuKhoa { get; set; }
    [StringLength(250)] public string? TomTat { get; set; }
    [StringLength(250)] public string? KyHieuKho { get; set; }
    [StringLength(250)] public string? XemToanVan { get; set; }
    //public DateTime? NgayTao { get; set; }
    //public DateTime? NgayTao { get; set; }
    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now; // L?u theo UTC
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now; // L?u theo UTC
}

public class CongBoDto
{
    [StringLength(50)] public required string ChiSoDeMuc { get; set; }

    [StringLength(350)] public string? LinhVucNghienCuu { get; set; }
    [StringLength(350)] public string? DangTaiLieu { get; set; }

    [StringLength(350)] public string? TacGia { get; set; }

    [StringLength(250)] public required string NhanDe { get; set; }

    [StringLength(350)] public string? NguonTrich { get; set; }

    [StringLength(50)] public string? NamXuatBan { get; set; }

    [StringLength(250)] public string? So { get; set; }
    [StringLength(250)] public string? Trang { get; set; }
    [StringLength(250)] public string? ISSN { get; set; }
    [StringLength(250)] public string? TuKhoa { get; set; }
    [StringLength(250)] public string? TomTat { get; set; }
    [StringLength(250)] public string? KyHieuKho { get; set; }
    [StringLength(250)] public string? XemToanVan { get; set; }
}

public class CongBoFilter : PaginationDto, IKeyword
{
    public string? NamXuatBan { get; set; }
    public string? TacGia { get; set; }
    public string? DangTaiLieu { get; set; }
    public string? LinhVucNghienCuu { get; set; }
    public string? ChiSoDeMuc { get; set; }
    public string? NhanDe { get; set; }
    public short? TrangThai { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
   
    public string? NgayCapNhat { get; set; } 
    public string? Keyword { get; set; }
}