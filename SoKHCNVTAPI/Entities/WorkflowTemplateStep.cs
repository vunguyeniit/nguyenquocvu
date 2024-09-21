using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

//Bang MAU buoc quy trinh
[Table("BuocQuyTrinhMau", Schema = "skhcn")]
public class WorkflowTemplateStep
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(50)] public required string Ma { get; set; }

    //public required long MaQuyTrinh { get; set; }

    [ForeignKey("WorkflowTemplate")]
    [Column("MaQTMau")]
    public required long WorkflowTemplateId { get; set; }

    [StringLength(100)] public required string Ten { get; set; }

    public required int Buoc { get; set; } = 1; // So thu tu buoc trong quy trinh

    [StringLength(2000)] public string ThaoTac { get; set; } = ""; //Json thao tac xu ly

    [StringLength(20000)] public string? NoiDung { get; set; } = "";

    public short? TrangThai { get; set; } = 0; //-> KhoiTao = 0, ChoDuyet = 1, Duyet = 2,

    public DateTime? Ngay { get; set; } = Utils.getCurrentDate();

    public long NguoiCapNhat { get; set; } = 0;

    public DateTime? CapNhat { get; set; } = Utils.getCurrentDate();

    public short ChuKy { get; set; } = 0; // co yeu cau hay ko? 0:ko, 1:Co

    [StringLength(2000)] public string Quyen { get; set; } = ""; //Json thao tac xu ly
    
}

//public class WorkflowTemplateSteppDto
//{
//    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
//    [Required(ErrorMessage = "{0} không được để trống!")]
//    public required string Ma { get; set; }

//    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
//    [Required(ErrorMessage = "{0} không được để trống!")]
//    public required string Ten { get; set; }

//    [Required(ErrorMessage = "{0} không được để trống!")]
//    public required int MaQuyTrinh { get; set; }

//    public int TongBuoc { get; set; }

//    [StringLength(20000, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
//    public string? NoiDung { get; set; }

//    public string? ThaoTac { get; set; }

//    public DateTime? CapNhat { get; set; } = DateTime.Now;

//    public int NguoiCapNhat { get; set; } = 0;
//    public int ChuKy { get; set; } = 0;
//    public string? TenNguoiCapNhat { get; set; } = "";

//    public short? TrangThai { get; set; }
//}

public class WorkflowStepwFilter : PaginationDto
{
    public string? Ma { get; set; }
    public int MaQuyTrinhMau { get; set; }
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }
}