using System;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class StepStatusModel
	{
        // target Id example OrganizationId

        [Required(ErrorMessage = "{0} không được để trống")]
        public required long TargetId { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        public required long ModuleId { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        public required long StepId { get; set; }


        [Required(ErrorMessage = "{0} không được để trống")]
        public short Status { get; set; }
    }
}

