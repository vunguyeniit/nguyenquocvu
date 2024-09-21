using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;


namespace SoKHCNVTAPI.Entities
{
    [Table("StepStatus", Schema = "skhcn")]
    public class StepStatus : BaseEntity
	{
        public required long StepId { get; set; }

        public long? ModuleId { get; set; }

        public required long? TargetId { get; set; }

        [JsonIgnore]
        public virtual required Step Step { get; set; }

    }
}

