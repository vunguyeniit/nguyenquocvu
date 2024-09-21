using System;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class PermissionModel
	{

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(20, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Name { get; set; }

        public short Status { get; set; }
    }
}

