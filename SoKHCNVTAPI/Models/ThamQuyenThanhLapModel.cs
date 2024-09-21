using SoKHCNVTAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class ThamQuyenThanhLapModel
    {
        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(20, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Ma { get; set; }

        [Required(ErrorMessage = "{0} không được để trống")]
        [StringLength(500, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public required string Ten { get; set; }

        [StringLength(300, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
        public string MauBaoCao { get; set; } = "";

        public short TrangThai { get; set; } = 1;

        [StringLength(500)]
        public string? MoTa { get; set; }

        public short? TrucThuoc { get; set; }
        public short? TrucThuocDP { get; set; }
        public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
        public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
    }
}