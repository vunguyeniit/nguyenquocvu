using SoKHCNVTAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;
public class BieuDoMauDto
{
    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Ma { get; set; }

    [StringLength(150, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Ten { get; set; }

    [StringLength(500, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string MoTa { get; set; }

    [StringLength(100)]
    public required string Loai { get; set; }

    [StringLength(100)]
    public required string KieuHienThi { get; set; } = "Doc";

    public short? ThongKe { get; set; } = 0;
    public short? SoCot { get; set; } = 1;
    public string? CotY { get; set; } = "";
    public string? CotX { get; set; } = "";

    public short? TrangThai { get; set; } = 1;
  
}

public class BieuDoDto
{
    public required long BieuDoMauId { get; set; }

    [StringLength(50, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string Ma { get; set; }

    [StringLength(150, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string TieuDe { get; set; }

    [StringLength(500, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public required string MoTa { get; set; }

    [StringLength(10000, ErrorMessage = "{0} không được vượt quá {2} ký tự")]
    public string? ThongKe { get; set; }

    //public virtual ICollection<DuLieuBieuDo>? DuLieuBieuDos { get; set; }
}

public class DuLieuBieuDoDto
{
    public long BieuDoId { get; set; }

    [StringLength(150)]
    public required string MaDuLieu { get; set; }

    [StringLength(10000)]
    public required string NoiDung { get; set; }
}