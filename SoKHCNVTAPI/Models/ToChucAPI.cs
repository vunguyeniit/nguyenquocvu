using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class ToChucAPI
{
    public long Id { get; set; }
    public string? tentochuc { get; set; }
    public long? madinhdanhtochuc { get; set; }
    public string? diachi { get; set; }
    public string? tinhthanh { get; set; }
    public string? dienthoai { get; set; }
    public string? website { get; set; }
    public string? nguoidungdau { get; set; }
    public string? loaihinhtochuc { get; set; }
    public long? hinhthucsohuu { get; set; }
    public string? linhvucnc { get; set; }


}