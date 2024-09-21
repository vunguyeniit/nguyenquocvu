using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;


namespace SoKHCNVTAPI.Entities;


[Table("StepHasStatus", Schema = "skhcn")]
public class StepHasStatus : BaseEntity
{
        public required long StepId { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }

}

