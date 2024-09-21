using SoKHCNVTAPI.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class HocVanCanBoDto
{
    public required long? MaCanBo { get; set; }
    
    [StringLength(350)]
    public required string BacDaoTao { get; set; }
    
    [StringLength(350)]
    public string? TrinhDoDaoTao { get; set; }
    
    [StringLength(250)]
    public string? NoiDaoTao { get; set; }
    [StringLength(250)]
    public string? ChuyenNganh { get; set; }
    [StringLength(250)]
    public string? ChucDanhKHCN { get; set; }

    [StringLength(4)]
    public string? NamTotNghiep { get; set; }
}

public class HocVanCanBoResponse
{
    public long? Id { get; set; }

    public long? MaCanBo { get; set; }
    public string? BacDaoTao { get; set; }
    public string? TrinhDoDaoTao { get; set; }
    public string? NoiDaoTao { get; set; }
    public string? ChuyenNganh { get; set; }
    public string? ChucDanhKHCN { get; set; }
    public string? NamTotNghiep { get; set; }

    public DateTime? NgayTao { get; set; }
    public DateTime? NgayCapNhat { get; set; }
}

public class HocVanCanBoFilter : PaginationDto, IKeyword
{
    public long? MaCanBo { get; set; } = 0;
    public string? BacDaoTao { get; set; }
    public string? TrinhDoDaoTao { get; set; }
    public string? NoiDaoTao { get; set; }
    public string? ChuyenNganh { get; set; }
    public string? ChucDanhKHCN { get; set; }
    public string? NamTotNghiep { get; set; }
    public string? Keyword { get; set; }
}