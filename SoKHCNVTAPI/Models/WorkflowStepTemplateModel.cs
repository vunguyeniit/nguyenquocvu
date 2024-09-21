using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class WorkflowStepTemplateModel
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(20, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Ma { get; set; }

        public long MaQuyTrinhMau { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(500, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Ten { get; set; }

        [StringLength(50000, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string? NoiDung { get; set; } = "";

        public short TrangThai { get; set; }

        public int Buoc { get; set; } = 1;

        public short ChuKy { get; set; } = 0;

        public string Quyen { get; set; } = ""; // Permission

       //public string ThaoTac { get; set; } = ""; // Action
    }
}