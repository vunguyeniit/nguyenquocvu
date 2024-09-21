using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class WorkflowModel
    {
        public required long DuAnId { get; set; }
        public required long MaDMQT { get; set; } //1, 2, 3

        public required long MaQuyTrinhMau { get; set; }

        [Required(ErrorMessage = "Mã không được để trống")]
        [StringLength(20, ErrorMessage = "Mã không được vượt quá {2} ký tự")]
        public required string Ma { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(500, ErrorMessage = "Tên không được vượt quá {2} ký tự")]
        public required string Ten { get; set; }

        [StringLength(20000, ErrorMessage = "Nội dung không được vượt quá {2} ký tự")]
        public string NoiDung { get; set; } = "";

        public short TrangThai { get; set; } = 1; 

        public int BuocHienTai { get; set; } = 1;

        public long NguoiCapNhat { get; set; } = 0;

        [StringLength(1000)] public string SuKien { get; set; } = "";

        [StringLength(1000)] public string Quyen { get; set; } = "";

        public string? GhiChu { get; set; } = "";
    }

    public class WorkflowStatusModel
    {
        public short TrangThai { get; set; } = 1;

        public string? GhiChu { get; set; } = "";

        [StringLength(1000)] public string? SuKien { get; set; } = "";

        [StringLength(1000)] public string? Quyen { get; set; } = "";
    }

    public class WorkflowTemplateModel
    {
        public required long MaDMQT { get; set; } = 0;

        [Required(ErrorMessage = "Mã không được để trống")]
        [StringLength(20, ErrorMessage = "Mã không được vượt quá {2} ký tự")]

        public required string Ma { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(500, ErrorMessage = "Ten không được vượt quá {2} ký tự")]
        public required string Ten { get; set; }

        [StringLength(20000, ErrorMessage = "Nội dung không được vượt quá {2} ký tự")]
        public string NoiDung { get; set; } = "";

        public short TrangThai { get; set; } = 1;

        public long NguoiCapNhat { get; set; } = 0;

        public string? GhiChu { get; set; } = "";
        public int TongBuoc { get; set; } = 1;

        [StringLength(1000)] public string SuKien { get; set; } = "";

        [StringLength(1000)] public string Quyen { get; set; } = "";
    }
}