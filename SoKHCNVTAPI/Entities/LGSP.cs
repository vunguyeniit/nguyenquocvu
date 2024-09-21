using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("LGSP", Schema = "skhcn")]
public class LGSP
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }
    [StringLength(350)] public  string? ky_bao_cao  { get; set; }
    public  long? ky_hd_moi { get; set; }
    public long? kiem_tra_tien_do_theo_ky  { get; set; }
    public long? nghiem_thu_va_thanh_ly { get; set; }
    public long? don_dang_ky_so_huu_tri_tue{ get; set; }
    public long? cong_tac_tham_dinh { get; set; }
    public long? cap_phep_buc_xa_cap_moi{ get; set; }
    public long? cap_phep_buc_xa_gia_han{ get; set; }
    public long? cap_phep_buc_xa_sua_doi{ get; set; }
    public long? cap_phep_buc_xa_bo_sung{ get; set; }
    public long? so_luong_gian_hang{ get; set; }
    public long? so_luong_san_pham { get; set; }
    public long? so_luong_ho_tro { get; set; }
    public long? cong_bo_hop_chuan { get; set; }
    public long? cong_bo_hop_quy{ get; set; }
    public long? kiem_dinh{ get; set; }
    public long? hieu_chuan{ get; set; }
    public long? thu_nghiem { get; set; }
    public string? thoi_gian_bao_cao { get; set; }
    public DateTime? NgayTao { get; set; } = DateTime.Now;
    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;


  

}

public class LGSPDto
{
    public long Id { get; set; }
    [StringLength(350)] public string? ky_bao_cao { get; set; }
    public long? ky_hd_moi { get; set; }
    public long? kiem_tra_tien_do_theo_ky { get; set; }
    public long? nghiem_thu_va_thanh_ly { get; set; }
    public long? don_dang_ky_so_huu_tri_tue { get; set; }
    public long? cong_tac_tham_dinh { get; set; }
    public long? cap_phep_buc_xa_cap_moi { get; set; }
    public long? cap_phep_buc_xa_gia_han { get; set; }
    public long? cap_phep_buc_xa_sua_doi { get; set; }
    public long? cap_phep_buc_xa_bo_sung { get; set; }
    public long? so_luong_gian_hang { get; set; }
    public long? so_luong_san_pham { get; set; }
    public long? so_luong_ho_tro { get; set; }
    public long? cong_bo_hop_chuan { get; set; }
    public long? cong_bo_hop_quy { get; set; }
    public long? kiem_dinh { get; set; }
    public long? hieu_chuan { get; set; }
    public long? thu_nghiem { get; set; }
    public string? thoi_gian_bao_cao { get; set; }
    public DateTime? NgayTao { get; set; } = DateTime.Now;
    public DateTime? NgayCapNhat { get; set; } = DateTime.Now;
}

public class LGSPFilter : PaginationDto, IKeyword
{
    public required long Id { get; set; }
    [StringLength(350)] public string? ky_bao_cao { get; set; }
    public string? thoi_gian_bao_cao { get; set; }
    public long? ky_hd_moi { get; set; }
    public long? kiem_tra_tien_do_theo_ky { get; set; }
    public long? nghiem_thu_va_thanh_ly { get; set; }
    public long? don_dang_ky_so_huu_tri_tue { get; set; }
    public long? cong_tac_tham_dinh { get; set; }
    public long? cap_phep_buc_xa_cap_moi { get; set; }
    public long? cap_phep_buc_xa_gia_han { get; set; }
    public long? cap_phep_buc_xa_sua_doi { get; set; }
    public long? cap_phep_buc_xa_bo_sung { get; set; }
    public long? so_luong_gian_hang { get; set; }
    public long? so_luong_san_pham { get; set; }
    public long? so_luong_ho_tro { get; set; }
    public long? cong_bo_hop_chuan { get; set; }
    public long? cong_bo_hop_quy { get; set; }
    public long? kiem_dinh { get; set; }
    public long? hieu_chuan { get; set; }
    public long? thu_nghiem { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
}