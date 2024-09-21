using System;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class StepActionModel
	{
        [Required(ErrorMessage = "{0} không được để trống")]

        public required long StepId { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        public required long ActionId { get; set; }

		public  long? RoleId { get; set; }

        public required long? UserId { get; set; }

        public short Status { get; set; }

    }
}

