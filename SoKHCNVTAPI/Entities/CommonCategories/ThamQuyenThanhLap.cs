using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Helpers;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// Thẩm quyền thành lập -> Gán tổ chức
[Table("ThamQuyenThanhLap", Schema = "skhcn")]
public class ThamQuyenThanhLap
{
    [Key]
    public long Id { get; init; }

    [StringLength(200)]
     public required string Ten { get; set; }
     
     [StringLength(50)]
     public required string Ma { get; set; }
    
     [StringLength(500)]
     public required string MoTa { get; set; }
    public short? TrangThai { get; set; }
    public short? TrucThuoc { get; set; }
    public short? TrucThuocDP { get; set; }
    public long? NguoiCapNhat { get; set; }
    public DateTime? NgayTao { get; set; } = Utils.getCurrentDate();
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

public class ThamQuyenThanhLapDto
{
    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public string? MoTa { get; set; }
    public short? TrangThai { get; set; }
    public short? TrucThuoc { get; set; }
    public short? TrucThuocDP { get; set; }
    public DateTime? NgayCapNhat { get; set; } = Utils.getCurrentDate();
}

public class ThamQuyenThanhLapFilter : PaginationDto, IKeyword
{
     public string? Ma { get; set; }
     public short? TrangThai { get; set; }
     public string? Keyword { get; set; }
}