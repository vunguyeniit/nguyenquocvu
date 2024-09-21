using EasyCaching.Core;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models
{
	public class GiaoDienModel
    {
        public required long TaiKhoan { get; init; }

        [StringLength(50)]
        public required string Ma { get; set; }

        [StringLength(250)]
        public required string Ten { get; set; }

        [StringLength(20000)]
        public required string DuLieu { get; set; }

        public DateTime? CapNhat { get; set; }

    }


    public class GiaoDienFilter
    {
        public string? TuKhoa { get; set; } = "";
        public string? Ma { get; set; } = "";

        public long TaiKhoan { get; set; } = 0;

    }

}