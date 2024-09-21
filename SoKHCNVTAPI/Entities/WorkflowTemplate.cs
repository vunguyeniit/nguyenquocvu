using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("QuyTrinhMau", Schema = "skhcn")]
public class WorkflowTemplate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [ForeignKey("WorkflowCategory")]
    [Column("MaDMQT")]
    public required long WorkflowCategoryId { get; set; }

    [StringLength(50)] public required string Ma { get; set; }

    [StringLength(50)] public required string Ten { get; set; }
    public int TongBuoc { get; set; } = 0;

    [StringLength(20000)] public string? NoiDung { get; set; } = "";

    public short TrangThai { get; set; } = 0;

    [StringLength(250)] public string? GhiChu { get; set; }

    public DateTime? CapNhat { get; set; } = Utils.getCurrentDate();

    public long NguoiCapNhat { get; set; } = 0;

    [StringLength(1000)] 
    public string? SuKien { get; set; }

    [StringLength(1000)] 
    public string? Quyen { get; set; }

    public virtual ICollection<WorkflowTemplateStep>? WorkflowTemplateSteps { get; set; }
}