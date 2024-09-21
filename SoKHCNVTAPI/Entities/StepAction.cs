using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities
{
	[Table("StepAction", Schema = "skhcn")]
	public class StepAction : BaseEntity
	{
		public required long StepId { get; set; }

		public required long ActionId { get; set; }

		public long? RoleId { get; set; }

		public long? UserId { get; set; }


        public virtual Action? Action { get; set; }

    }
}

