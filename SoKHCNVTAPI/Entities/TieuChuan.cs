using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("TieuChuan", Schema = "skhcn")]
public class TieuChuan
{
    [Key]
    public long Id { get; init; }

    [StringLength(50)] public required string TenTieuChuan { get; set; }

    [StringLength(350)] public required string LoaiTieuChuan { get; set; }
    [StringLength(350)] public required string SoHieu { get; set; }
    [StringLength(350)] public string? TTHieuLuc { get; set; }

    [StringLength(350)] public string? NamBanHanh { get; set; }

    [StringLength(100)] public string? ThuMucTieuChuan { get; set; }

    [StringLength(100)] public string? KyThuatQuocGia { get; set; }

    [StringLength(250)] public string? KyThuatDiaPhuong { get; set; }

    [StringLength(250)] public string? DuocChiDinh { get; set; }
    [StringLength(250)] public string? ChatLuongQuocGia { get; set; }
    public short? TrangThai { get; set; }
    public DateTime? NgayTao { get; set; }
}

public class TieuChuanDto
{
    //[StringLength(50)] public required string Ma { get; set; }

    [StringLength(250)] public required string TenTieuChuan { get; set; }

    [StringLength(250)] public required string LoaiTieuChuan { get; set; }
    [StringLength(250)] public required string SoHieu { get; set; }
    [StringLength(250)] public string? TTHieuLuc { get; set; }

    [StringLength(250)] public string? NamBanHanh { get; set; }

    [StringLength(250)] public string? ThuMucTieuChuan { get; set; }

    [StringLength(250)] public string? KyThuatQuocGia { get; set; }

    [StringLength(250)] public string? KyThuatDiaPhuong { get; set; }

    [StringLength(250)] public string? DuocChiDinh { get; set; }
    [StringLength(250)] public string? ChatLuongQuocGia { get; set; }
   
}

public class TieuChuanFilter : PaginationDto, IKeyword
{
    public string? SoHieu { get; set; }
    public string? TenTieuChuan { get; set; }
    public short? TrangThai { get; set; }
    public string? Keyword { get; set; }
}