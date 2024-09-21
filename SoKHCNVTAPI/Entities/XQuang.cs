using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("XQuang", Schema = "skhcn")]
public class XQuang
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public  string? ten_co_so { get; set; }

    public string? lien_he{ get; set; }
    public  string? loai_thiet_bi { get; set; }

    public string? serial_number { get; set; }

    public string? hang_san_xuat { get; set; }

    public string? nuoc_san_xuat{ get; set; }

    public string? nam_san_xuat   { get; set; }

    public string? so_giay_phep_nam_truoc  { get; set; }
    public string? ngay_cap_nam_truoc { get; set; }
    public string? ngay_het_han_nam_truoc { get; set; }
    public string? so_giay_phep_trong_nam  { get; set; }
    public string? ngay_cap_trong_nam  { get; set; }
    public string? ngay_het_han_trong_nam    { get; set; }
    public string? chua_dat_dieu_kien_cap_phep  { get; set; }
    public string? chua_lam_thu_tuc_cap_phep   { get; set; }
    public string? tong_so_nhan_vien_buc_xa  { get; set; }
    public string? ghi_chu{ get; set; }
    public string? thoi_gian { get; set; }


    public DateTime? NgayTao { get; set; } = DateTime.Now;

    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;




}

public class XQuangDto
{
    public long Id { get; set; }
    public string? ten_co_so { get; set; }
    public string? lien_he { get; set; }
    public string? loai_thiet_bi { get; set; }
    public string? serial_number { get; set; }
    public string? hang_san_xuat { get; set; }
    public string? nuoc_san_xuat { get; set; }
    public string? nam_san_xuat { get; set; }
    public string? so_giay_phep_nam_truoc { get; set; }
    public string? ngay_cap_nam_truoc { get; set; }
    public string? ngay_het_han_nam_truoc { get; set; }
    public string? so_giay_phep_trong_nam { get; set; }
    public string? ngay_cap_trong_nam { get; set; }
    public string? ngay_het_han_trong_nam { get; set; }
    public string? chua_dat_dieu_kien_cap_phep { get; set; }
    public string? chua_lam_thu_tuc_cap_phep { get; set; }
    public string? tong_so_nhan_vien_buc_xa { get; set; }
    public string? ghi_chu { get; set; }
    public string? thoi_gian { get; set; }
    public DateTime? NgayTao { get; set; } =  DateTime.Now;
    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
}

public class XQuangFilter : PaginationDto, IKeyword
{
    public string? ten_co_so { get; set; }
    public string? lien_he { get; set; }
    public string? loai_thiet_bi { get; set; }
    public string? serial_number { get; set; }
    public string? hang_san_xuat { get; set; }
    public string? nuoc_san_xuat { get; set; }
    public string? nam_san_xuat { get; set; }
    public string? so_giay_phep_nam_truoc { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? NgayCapNhat { get; set; } 
}
