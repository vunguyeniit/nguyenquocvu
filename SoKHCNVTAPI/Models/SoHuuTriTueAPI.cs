using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class SoHuuTriTueAPI
{
    public long Id { get; set; }
    public string? tensangche { get; set; }
    public string? loaisohuu { get; set; }
    public string? phanloai { get; set; }
    public string? sobang { get; set; }
    public string? chubang { get; set; }
    public string? thongtinSoHuu { get; set; }
    public string? sangchetoanvan { get; set; }
    public string? tochucdaidien { get; set; }
    public string? nguoidaidien { get; set; }
    public string? tochucdudieukien { get; set; }
    public string? canhandudieukien { get; set; }
 


}