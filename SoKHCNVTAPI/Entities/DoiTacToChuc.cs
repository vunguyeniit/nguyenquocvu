using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("DoiTacToChuc", Schema = "skhcn")]
public class DoiTacToChuc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public required string TenDoiTac { get; set; }
    
    public short Loai { get; set; } = 1;
    
    public string? NoiDungHopTac { get; set; }

    public string? NamThamGia { get; set; } = DateTime.UtcNow.Year.ToString();
    
    public long MaToChuc { get; set; }
    
    public long NguoiTao { get; set; }
    
    public long NguoiCapNhat { get; set; }
    
    public DateTime? NgayTao { get; set; }
   
    public DateTime? NgayCapNhat { get; set; }
}


public class DoiTacToChucDto
{

    public required string TenDoiTac { get; set; }

    public short Loai { get; set; } = 1;

    public string? NoiDungHopTac { get; set; }

    public string? NamThamGia { get; set; } = DateTime.UtcNow.Year.ToString();

    public long MaToChuc { get; set; }

    public long NguoiTao { get; set; }

    public long NguoiCapNhat { get; set; }

    public DateTime? NgayTao { get; set; }

    public DateTime? NgayCapNhat { get; set; }
}

public class DoiTacToChucFilter : PaginationDto, IKeyword
{
    public string? TenDoiTac { get; set; }
    public short? Loai { get; set; }
    public string? NamThamGia { get; set; }
    public string? NoiDungHopTac { get; set; }
    public long? MaToChuc { get; set; }
    public string? Keyword { get; set; }
}