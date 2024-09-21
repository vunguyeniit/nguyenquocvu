using System.ComponentModel.DataAnnotations;


namespace SoKHCNVTAPI.Models
{
	public class StepModel
	{
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string? Description { get; set; }

        public required string Tag { set; get; }

        public long? RoleId { set; get; }
        public List<long>? UserId { set; get; }

        public required long ModuleId { set; get; }

        public short Status { get; set; } = 1;

        public List<string>? Statuses { get; set; }

        public List<ActionStepDto>? Action { get; set; }

    }
}