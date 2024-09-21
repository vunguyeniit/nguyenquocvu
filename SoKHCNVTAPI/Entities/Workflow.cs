using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("QuyTrinh", Schema = "skhcn")]
public class Workflow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public required long DuAnId { get; set; } = 0;

    //[ForeignKey("WorkflowCategory")]
    [Column("MaDMQT")]
    public required long WorkflowCategoryId { get; set; }

    [Column("MaMau")]
    public required long WorkflowTemplateId { get; set; }

    [StringLength(50)] public required string Ma { get; set; }

    [StringLength(50)] public required string Ten { get; set; }

    public int TongBuoc { get; set; } = 0;

    public int BuocHienTai { get; set; } = 1;

    [StringLength(20000)] public string? NoiDung { get; set; } = "";

    public short TrangThai { get; set; }

    [StringLength(250)] public string? GhiChu { get; set; }

    [StringLength(1000)] public string? SuKien { get; set; }

    [StringLength(1000)] public string? Quyen { get; set; }

    public DateTime? CapNhat { get; set; } = Utils.getCurrentDate();
    public DateTime? NgayTao { get; set; }

    public required long NguoiCapNhat { get; set; } = 0; // La nguoi owner quy trinh nay

    public virtual List<WorkflowStep> WorkflowSteps { get; set; } = new List<WorkflowStep> { };
}

public class WorkflowDto
{
    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    [Required(ErrorMessage = "{0} không được để trống!")]
    public required string Ma { get; set; }

    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    [Required(ErrorMessage = "{0} không được để trống!")]
    public required string Ten { get; set; }

    [Required(ErrorMessage = "{0} không được để trống!")]
    public required int BuocHienTai { get; set; } = 1;

    public int TongBuoc { get; set; }

    [StringLength(20000, ErrorMessage = "{0} không được vượt quá {2} ký tự!")]
    public string NoiDung { get; set; } = "";

    public string? GhiChu { get; set; }

    public DateTime? CapNhat { get; set; } = Utils.getCurrentDate();

    public long NguoiCapNhat { get; set; } = 0;

    public string? TenNguoiCapNhat { get; set; } = "";

    public short? TrangThai { get; set; }

    [StringLength(1000)] public string? SuKien { get; set; }

    [StringLength(1000)] public string? Quyen { get; set; }
}

public class WorkflowFilter : PaginationDto
{
    public string? Ma { get; set; }
    public string? Ten { get; set; }
    public long MaDMQT { get; set; }
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set;}
    public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? ngayTao { get; set; }
    public string? capNhat { get; set; }
}