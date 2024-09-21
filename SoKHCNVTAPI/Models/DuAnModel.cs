using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

public class DuAnModel
{

    [Required]
    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(300)]
    public required string Ten { get; set; }

    [StringLength(250)]
    public required string LoaiCN { get; set; }

    [StringLength(300)]
    public required string QuocGia { get; set; }

    [StringLength(250)]
    public required string NamCB { get; set; } // Năm công bố

    public long UserId { get; set; } = 0;

    public required string Nguon { get; set; } // nguồn công nghê

    public string? CNTB { get; set; } = ""; // Công nghê và thiết bị
    public string? UngDungCNC { get; set; } = "";
    public string? DTSXSPCNC { get; set; } = ""; // Dự án đầu tư SX sản phẩm CNC
    public string? CoSoUomTaoCNC { get; set; } = ""; // Co so uom tao Công nghê cao
    public string? UomTaoDNCNC { get; set; } = ""; // Uom tao Doanh nghiep CNC
    public string? CGNGVN { get; set; } = ""; // Chuyển giao công nghệ nuoc ngoai vao VN
    public string? CGVNNG { get; set; } = ""; //Chuyển giao công nghệ Vn ra nước ngoài

    public string? CGTN { get; set; } = ""; // Chuyển giao trong nuoc

    public short TrangThai { get; set; } = 1;
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now; // Lưu theo UTC
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now; // Lưu theo UTC
}