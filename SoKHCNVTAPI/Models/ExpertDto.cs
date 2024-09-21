using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class ExpertDto
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
    [StringLength(100)] public string? QuocTich { get; set; }
    [StringLength(100)] public string? HocVi { get; set; }

    [StringLength(100)] public string? Email { get; set; }
    [StringLength(100)] public string? BangCap { get; set; }
    [StringLength(250)] public string? LVChuyenMon { get; set; }
    [StringLength(250)] public string? ThongTinLienHe { get; set; }
    public long EducationLevelId { get; set; }
    public long ExpertIdentifierId { get; set; }

    [StringLength(250)] public string? DiaChi { get; set; }
    [StringLength(250)] public string? HinhAnh { get; set; }
    [StringLength(250)] public string? Video { get; set; }
    [StringLength(250)] public string? ThongTinTaiChinh { get; set; }
    [StringLength(250)] public string? ChiPhiDiLai { get; set; }
    public virtual ICollection<User>? User { get; set; }
}