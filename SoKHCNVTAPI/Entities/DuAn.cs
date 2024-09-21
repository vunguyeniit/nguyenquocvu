using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities;

[Table("DuAn", Schema = "skhcn")]
public class DuAn
{
    [Key]
    public long Id { get; init; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(300)]
    public required string Ten { get; set; }
    
    [StringLength(250)]
    public required string LoaiCN { get; set; }
    
    [StringLength(300)]
    public required string QuocGia  { get; set; }

    [StringLength(250)]
    public required string NamCB { get; set; } // Năm công bố

    public long UserId { get; set; }

    public short TrangThai { get; set; } = 1;

    //public DateTime? CreatedAt { get; set; }
    //public DateTime? UpdatedAt { get; set; } = Utils.getCurrentDate();
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.Now; // Lưu theo UTC
    public DateTimeOffset? UpdatedAt { get; set; } = DateTimeOffset.Now; // Lưu theo UTC


    public required string Nguon { get; set; } // nguồn công nghê

    public string? CNTB { get; set; } = ""; // Công nghê và thiết bị
    public string? UngDungCNC { get; set; } = "";
    public string? DTSXSPCNC { get; set; } = ""; // Dự án đầu tư SX sản phẩm CNC
    public string? CoSoUomTaoCNC { get; set; } = ""; // Co so uom tao Công nghê cao
    public string? UomTaoDNCNC { get; set; } = ""; // Uom tao Doanh nghiep CNC
    public string? CGNGVN { get; set; } = ""; // Chuyển giao công nghệ nuoc ngoai vao VN
    public string? CGVNNG { get; set; } = ""; //Chuyển giao công nghệ Vn ra nước ngoài
    public string? CGTN { get; set; } = ""; // Chuyển giao trong nuoc
    public virtual List<Workflow> Workflows { get; set; } = new List<Workflow> { };
}
public class DuAnFilter : PaginationDto
{
    public string? Ma { get; set; }
    public string? Ten { get; set; }
    public short? TrangThai { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? Keyword { get; set; }
}