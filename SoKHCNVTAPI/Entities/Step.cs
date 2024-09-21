using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

    [Table("Steps", Schema = "skhcn")]
    public class Step : BaseEntity
{
	public required long ModuleId { get; set; }

	[StringLength(100)]
	public required string Name { get; set; }

    [StringLength(200)]
    public string? Description { get; set; }

	public long RoleId { get; set; } = 0;

	public required string Tag { get; set; }

    [JsonIgnore]
    public virtual Module? Module { get; set; }

    public virtual required StepStatus StepStatus { get; set; }

    public required List<ActionResponse> Actions { get; set; }

    public virtual  List<StepHasStatus> StatusList { get; set; }



}

public class StepItem
{
    public required long Id { get; set; }
    public required long ModuleId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public long RoleId { get; set; } = 0;

    public required string Tag { get; set; }

    public short? Status { get; set; }

    public StepStatus? StepStatus { get; set; }

    [JsonIgnore]
    public virtual Module? Module { get; set; }

    public required List<ActionResponse> Actions {get; set;}

    public virtual List<StepHasStatus>? Statuses { get; set; }

}
public class StepFilter : PaginationDto, IKeyword
{
    public short? Status { get; set; }
    public string? Keyword { get; set; }
    public required long ModuleId { get; set; }
}


