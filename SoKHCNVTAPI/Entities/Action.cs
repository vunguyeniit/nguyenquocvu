using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

    [Table("Actions", Schema = "skhcn")]
    public class Action : BaseEntity
{
	[StringLength(50)]
	public required string Name { set; get; }

	[StringLength(100)]
	public string? Description { get; set; }

	public long RoleId { get; set; } = 0;

	public string? Tag { get; set; }
}
