using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class ThongTinAPI
{
    public long Id { get; set; }

    public  string? masoquocgia { get; set; }
    public DateTime? thoigian { get; set; }
    public  string? tenquocgia { get; set; }

    public string? thongkenhanluc { get; set; }

    public string? thongkekinhphi { get; set; }

    public string? thongkeketqua { get; set; }

    public string? thongkehoatdong { get; set; }


}