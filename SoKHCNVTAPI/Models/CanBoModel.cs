using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Enums;

namespace SoKHCNVTAPI.Models;
public class CongBoKHCNDto
{
    public required long MaCanBo { get; set; }
    public required string LoaiCongBo { get; set; }
    public required string TenCongBo { get; set; }
    public required string NguonTrich { get; set; }
    public required string ISBN { get; set; }
    public required string URL { get; set; }
}
