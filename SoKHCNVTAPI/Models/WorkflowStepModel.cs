using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class WorkflowStepModel
    {
        public required long Id { get; set; }

        public required long MaQuyTrinh { get; set; }

        [StringLength(50000, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string NoiDung { get; set; }

        public required string PhanHoi { get; set; }

        public string? DinhKem { get; set; }

        public required short TrangThaiXL { get; set; } = 0;

        public string MaChuKy { get; set; } = "";

        public string HoTen { get; set; } = "";

        public string HanhDong { get; set; } = "Xem"; //Xem, Thêm, Cập Nhật, Xóa, Duyệt, Ký
    }
}