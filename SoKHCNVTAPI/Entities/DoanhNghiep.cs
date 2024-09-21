using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("DoanhNghiep", Schema = "skhcn")]
public class DoanhNghiep : BaseEntity
{
    public long Id { get; set; }
    [StringLength(50)] public required string Ma { get; set; }

    [StringLength(50)] public  string? MaSoThue { get; set; }

    [StringLength(350)] public  string? Ten { get; set; }
    [StringLength(350)] public  string? TenTiengAnh { get; set; }
    [StringLength(350)] public  string? ThuTruong { get; set; }

    [StringLength(350)] public string? DiaChi { get; set; }

    [StringLength(100)] public string? TenVietTat { get; set; }

    public string? NgayThanhLap { get; set; }

    [StringLength(100)] public string? TinhThanh { get; set; }

    [StringLength(250)] public string? CoQuanChuQuan { get; set; }

    [StringLength(250)] public string? Website { get; set; }
    [StringLength(250)] public string? Email { get; set; }
    [StringLength(250)] public  string? DienThoai { get; set; }
    [StringLength(250)] public string? LinhVucNghienCuu { get; set; }
    [StringLength(250)] public string? LoaiHinhToChuc { get; set; }

    [StringLength(250)] public string? LoaiHinh { get; set; }

    [StringLength(500)] public string? LinhVucSXKD { get; set; }

    [StringLength(500)] public string? HoatDongNCKH { get; set; }

    [StringLength(500)] public string? TaiSanTriTue { get; set; }
    [StringLength(500)] public string? SPDVKHCN { get; set; }
    [StringLength(250)] public string? VonDieuLe { get; set; }
    [StringLength(250)] public string? DTHangNam { get; set; }
    [StringLength(500)] public string? KQNC { get; set; }
    [StringLength(250)] public string? DoanhThuTT { get; set; }
    [StringLength(250)] public string? KinhPhiHangNam { get; set; }
    [StringLength(500)] public string? ChuyenGiaoCN { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now; 

}

public class DoanhNghiepDto
{
    public long Id { get; set; }
    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    [Required(ErrorMessage = "{0} không được để trống!")]
    public required string Ma { get; set; }

    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    [Required(ErrorMessage = "{0} không được để trống!")]
    public required string MaSoThue { get; set; }

    [StringLength(350, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    [Required(ErrorMessage = "{0} không được để trống!")]
    public required string Ten { get; set; }
    [StringLength(350)] public  string? TenTiengAnh { get; set; }
    [StringLength(350)] public  string? ThuTruong { get; set; }

    [StringLength(350)] public  string? DiaChi { get; set; }

    [StringLength(100)] public  string? TenVietTat { get; set; }

    [StringLength(50)] public  string? NgayThanhLap { get; set; }

    [StringLength(100)] public  string? TinhThanh { get; set; }

    [StringLength(250)] public string? CoQuanChuQuan { get; set; }

    [StringLength(250)] public  string? Website { get; set; }
    [StringLength(250)] public  string? Email { get; set; }
    [StringLength(250)] public  string? DienThoai { get; set; }
    [StringLength(500)] public string? LinhVucNghienCuu { get; set; }
    [StringLength(250)] public string? LoaiHinhToChuc { get; set; }

    [StringLength(250)] public string? LoaiHinh { get; set; }

    [StringLength(500)] public string? LinhVucSXKD { get; set; }

    [StringLength(500)] public string? HoatDongNCKH { get; set; }
    [StringLength(500)] public string? TaiSanTriTue { get; set; }
    [StringLength(500)] public string? SPDVKHCN { get; set; }
    [StringLength(250)] public string? VonDieuLe { get; set; }
    [StringLength(250)] public string? DTHangNam { get; set; }
    [StringLength(500)] public string? KQNC { get; set; }
    [StringLength(250)] public string? DoanhThuTT { get; set; }
    [StringLength(250)] public string? KinhPhiHangNam { get; set; }
    [StringLength(500)] public string? ChuyenGiaoCN { get; set; }
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;
}

public class DoanhNghiepFilter : PaginationDto, IKeyword
{
    public string? Ma { get; set; }
    public string? MaSoThue { get; set; }
    public string? Ten { get; set; }
    public string? DiaChi { get; set; }
    public string? TenVietTat { get; set; }
    public string? TenTiengAnh { get; set; }
    public string? TinhThanh { get; set; }
    public string? EnglishName { get; set; }
    public string? LoaiHinhToChuc { get; set; }
    public string? LinhVucNghienCuu { get; set; }
    public string? CoQuanChuQuan { get; set; }
    public string? Presentative { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? UpdatedAt { get; set; }
}