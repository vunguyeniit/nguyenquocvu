using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("DMQT", Schema = "skhcn")]
public class WorkflowCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [StringLength(50)] public required string Ma { get; set; }

    [StringLength(50)] public required string Ten { get; set; }


    [StringLength(300)] public string? MauBaoCao { get; set; } = "";

    public short TrangThai { get; set; } = 1;

    public string ThongTu { get; set; } = "";

    public virtual ICollection<WorkflowTemplate>? WorkflowTemplates { get; set; }

}

public class WorkflowCategoryFilter : PaginationDto
{
    public string? Ma { get; set; }
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }
    public string? ThongTu { get; set; }
}