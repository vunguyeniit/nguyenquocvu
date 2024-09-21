using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Entities.CommonCategories;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

// CHuyên gia 
[Table("ChuyenGia", Schema = "skhcn")]
public class ChuyenGia : BaseEntity
{
    [Required]
    [StringLength(50)]
    public required string Ma { get; set; }
    [StringLength(250)]
    public string? HovaTen { get; set; }
    [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự.")]
    public string? SDT { get; set; }
    public string? NgaySinh { get; set; }
    public string? Tuoi { get; set; }
    [StringLength(250)] public  string? QuocTich { get; set; }
    [StringLength(250)] public  string? HocVi { get; set; }
    [StringLength(100)] public string? Email { get; set; }
    [StringLength(250)] public  string? BangCap { get; set; }
    [StringLength(250)] public  string? LVChuyenMon { get; set; }
    [StringLength(250)] public string? ThongTinLienHe { get; set; }
    public long EducationLevelId { get; set; }

    public long ExpertIdentifierId { get; set; }
    //public virtual ExpertIdentifier? ExpertIdentifier { get; set; }
  
    [StringLength(250)] public string? DiaChi { get; set; }
    [StringLength(250)] public string? HinhAnh { get; set; }
    [StringLength(250)] public string? Video { get; set; }
    [StringLength(250)] public string? ThongTinTaiChinh { get; set; }
    [StringLength(250)] public string? ChiPhiDiLai { get; set; }

    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now;

}

public class ExpertFilter : PaginationDto, IKeyword
{
    public string? Ma { get; set; }
    public string? SDT { get; set; }
    public string? HoVaTen { get; set; }
    public string? HocVi { get; set; }
    public string? UpdatedAt { get; set; }
    public string? BangCap { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }


}