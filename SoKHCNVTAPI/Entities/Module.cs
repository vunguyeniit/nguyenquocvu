using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("Modules", Schema = "skhcn")]
public class Module : BaseEntity
	{
    [StringLength(50)]
    public required string Code { get; set; }

    [StringLength(50)]
	public required string Name { get; set; }

    [StringLength(200)]
    public required string Description { get; set; }

    public required long CreatedBy { get; set; }

    //public required virtual ICollection<Step> Steps { get; set; }
}

public class ModuleFilter : PaginationDto
{
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }

    public string? Ma { get; set; }
    public long TargetId { get; set; }
}

public class ModuleReponse : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    //public List<StepItem> Steps { get; set; }

};

