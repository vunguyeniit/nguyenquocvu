using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("ThongKe", Schema = "skhcn")]
public class ThongKe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [StringLength(350)] public  string? TenBieuMau { get; set; }
    [StringLength(350)] public  string? NamBaoCao { get; set; }
   public short? TrangThai { get; set; }
    [StringLength(350)] public string? LoaiBieuMau { get; set; }
    [StringLength(350)] public string? DonViBaoCao { get; set; }
    [StringLength(350)] public string? DonViNhanBaoCao { get; set; }
    [StringLength(350)] public string? NgayThangBaoCao { get; set; }
    [StringLength(350)] public string? NguoiLapBieu { get; set; }
    [StringLength(350)] public string? NguoiKiemTraBieu { get; set; }
    [StringLength(350)] public string? ThuTruongDonVi { get; set; }
    [StringLength(350)] public string? XemToanVan { get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; } 
 

}

public class ThongKeDto
{
    [StringLength(350)] public string? TenBieuMau { get; set; }
    [StringLength(350)] public string? NamBaoCao { get; set; }
    public short? TrangThai { get; set; }
    [StringLength(350)] public string? LoaiBieuMau { get; set; }
    [StringLength(350)] public string? DonViBaoCao { get; set; }
    [StringLength(350)] public string? DonViNhanBaoCao { get; set; }
    [StringLength(350)] public string? NgayThangBaoCao { get; set; }
    [StringLength(350)] public string? NguoiLapBieu { get; set; }
    [StringLength(350)] public string? NguoiKiemTraBieu { get; set; }
    [StringLength(350)] public string? ThuTruongDonVi { get; set; }
    [StringLength(350)] public string? XemToanVan { get; set; }
    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }
}

public class ThongKeFilter : PaginationDto, IKeyword
{
    public long Id { get; set; }
    public string? TenBieuMau { get; set; }
    public string? Keyword { get; set; }
}