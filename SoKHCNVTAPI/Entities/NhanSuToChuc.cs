using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("NhanLucToChuc", Schema = "skhcn")]
public class NhanSuToChuc
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    public long MaToChuc { get; init; }

    public required short TongNhanLuc { get; set; }

    public short? GiaoSu { get; set; }

    public short? PhoGiaoSu { get; set; }
    
    public short? TienSi { get; set; }

    public short? ThacSi { get; set; }

    public short DaiHoc { get; set; }

    public short CaoDang { get; set; }
    
    public short? NhanLucKhac { get; set; }
    
    
    public DateTime? NgayCapNhat { get; set; }
    
    public DateTime? ThoiGian { get; set; }
}

public class NhanSuToChucFilter : PaginationDto, IKeyword
{
    public long? MaToChuc { get; set; }
    public string? Keyword { get; set; }
}