using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class ActionModel
	{
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Name { get; set; }

        [StringLength(200, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string? Description { get; set; }

        public short? Status { get; set; } = 1;


        public long? RoleId { get; set; } = 0;


        public string? Tag { get; set; }
    }

    public class ActionStepDto
    {
        public List<long>? UserId { get; set; }
        public long Id { get; set; }
    }
}

