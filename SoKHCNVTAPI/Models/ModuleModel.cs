using System.ComponentModel.DataAnnotations;


namespace SoKHCNVTAPI.Models
{
	public class ModuleModel
	{
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(20, ErrorMessage = "{0} không được vượt quá {2} ký tự")]

        public required string Code { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string? Description { get; set; }

        public short Status { get; set; }

    }
}

