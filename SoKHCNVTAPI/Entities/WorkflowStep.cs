using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;
namespace SoKHCNVTAPI.Entities;

//buoc cua quy trinh
[Table("BuocQuyTrinh", Schema = "skhcn")]
public class WorkflowStep
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(50)] public required string Ma { get; set; }

    [StringLength(150)] public required string Ten { get; set; }

    [Column("MaQuyTrinh")]
    public required long WorkflowId { get; set; } // Ma Quy tirnh

    [StringLength(500)] public string DinhKem { get; set; } = ""; //Tap tin dinh kem

    [StringLength(20000)] public required string NoiDung { get; set; } = "";

    public required int Buoc { get; set; } = 1; // So thu tu buoc trong quy trinh

    public required short ChuKy { get; set; } = 0; 

    public required long XuLy { get; set; } = 0; //Nguoi Xu ly cho step nay

    public string HoTen { get; set; } = "";

    public long MaTK { get; set; } = 0;
    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();

    public DateTime? CapNhat { get; set; }

    public required short TrangThai { get; set; } = 0; 

    [StringLength(2000)] public string Quyen { get; set; } = ""; //Json thao tac xu ly
    //[StringLength(2000)] public string ThaoTac { get; set; } = ""; //Json thao tac xu ly

    public virtual ICollection<WorkflowStepLog>? WorkflowStepLogs { get; set; }
}

public class WorkflowStepFilter : PaginationDto
{
    public string? Ma { get; set; }
    public long MaQuyTrinh { get; set; } //WorkflowId
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }
}