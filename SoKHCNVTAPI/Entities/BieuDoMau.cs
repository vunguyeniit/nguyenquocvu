using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("BieuDoMau", Schema = "skhcn")]
public class BieuDoMau {
    [Key]
    public long Id { get; init; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(50)]
	public required string Ten { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }

    [StringLength(100)]
    public required string Loai { get; set; }

    [StringLength(100)]
    public required string KieuHienThi { get; set; }

    public short? ThongKe { get; set; } = 1;
    public short? SoCot { get; set; } = 1;

    public string? CotY { get; set; }
    public string? CotX { get; set; }

    public short? TrangThai { get; set; } = 1;
    public required long NguoiCapNhat { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<BieuDo>? BieuDos { get; set; }
}

public class BieuDoMauFilter : PaginationDto
{
    public short? TrangThai { get; set; }
    public string? TuKhoa { get; set; }
    public string? Ma { get; set; }
}

