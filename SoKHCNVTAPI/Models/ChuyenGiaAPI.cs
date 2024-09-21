using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class ChuyenGiaAPI
{
    public long Id { get; set; }
    public string? hovaten { get; set; }
    public string? ngaysinh { get; set; }
    public string? quoctich { get; set; }
    public string? hocvi { get; set; }
    public string? bangcap { get; set; }
    public string? linhvucchuyenmon { get; set; }
    public string? thongtinlienhe { get; set; }
    public string? sodienthoai { get; set; }
    public string? email { get; set; }
    public string? diachi { get; set; }


}