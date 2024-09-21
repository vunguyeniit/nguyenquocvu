using SoKHCNVTAPI.Helpers;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class NhiemVuCanBoDto
{
    public long? MaCanBo { get; set; }

    [StringLength(250)]
    public required string TenNhiemVu { get; set; }

    [StringLength(250)]
    public required string VaiTroThamGia { get; set; }
    
    [StringLength(4)]
    public string? NamBatDau { get; set; }
    
    [StringLength(4)]
    public string? NamKetThuc { get; set; }
    
}

public class NhiemVuCanBoResponse
{
    public long? Id { get; set; }
    public long? MaCanBo { get; set; }
    public string? TenNhiemVu { get; set; }
    public string? VaiTroThamGia { get; set; }
    public string? NamBatDau { get; set; }
    public string? NamKetThuc { get; set; }
    public DateTime? NgayTao { get; set; }
}