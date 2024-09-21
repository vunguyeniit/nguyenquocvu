using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities
{
	[Table("StepUser", Schema = "skhcn")]
	public class StepUser : BaseEntity
	{
			public required long StepId { get; set; }

			public required long UserId { get; set; }


			public virtual required User User { get; set; }
    }
}

