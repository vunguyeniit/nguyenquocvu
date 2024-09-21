using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class WorkflowStepLogModel
    {
        public required long Id { get; set; }

        public required long MaQuyTrinh { get; set; }

        [StringLength(20000, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string? NoiDung { get; set; }

        public string? DinhKem { get; set; }

        public short TrangThaiXL { get; set; } = 0;

        public string MaChuKy { get; set; } = "";
        
        public string HanhDong { get; set; } = ""; //Xem, Thêm, Cập Nhật, Xóa, Duyệt, Ký
    }
}