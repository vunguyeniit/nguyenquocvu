using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Helpers;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

// LinhVucNghienCuu -> Gán tổ chức
[Table("LinhVucNghienCuu", Schema = "skhcn")]
public class LinhVucNghienCuu
{
    [Key]
    public long Id { get; init; }

    public long Cha { get; set; }

    [StringLength(200)]
    public required string Ten { get; set; }
     
    [StringLength(50)]
    public required string Ma { get; set; }
    
    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }

    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;
}


public class LinhVucNghienCuuResponse
{
    public long Id { get; set; }

    public long Cha { get; set; }

    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; }

    public long? NguoiCapNhat { get; set; }

    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;

    public virtual IEnumerable<LinhVucNghienCuu>? Subs { get; set; }
}


public class LinhVucNghienCuuDto
{
    public long Cha { get; set; } = 0;

    [StringLength(200)]
    public required string Ten { get; set; }

    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }
    public short? TrangThai { get; set; } = 1;

    public long? NguoiCapNhat { get; set; }

    public DateTimeOffset? NgayTao { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? NgayCapNhat { get; set; } = DateTimeOffset.Now;
}

public class LinhVucNghienCuuFilter : PaginationDto, IKeyword
{
    public  string? Ten { get; set; }
    public long Cha { get; set; } = 0;
    public string? Ma { get; set; }
    public string? MoTa { get; set; }
    public short? TrangThai { get; set; }
     public string? Keyword { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? NgayTao { get; set; }
    public string? NgayCapNhat { get; set; }
}