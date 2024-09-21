using System.ComponentModel.DataAnnotations;
using SoKHCNVTAPI.Entities;
using SoKHCNVTAPI.Entities.CommonCategories;

namespace SoKHCNVTAPI.Models;

public class CanBoAPI
{
    public long? Id { get; set; }
    public string? hovaten { get; set; }
    public string? madinhdanhcanbo { get; set; }
    public string? ngaysinh { get; set; }
    public short? gioitinh { get; set; }
    public string? quoctich { get; set; }
    public string? cccd { get; set; }
    public string? noiohiennay { get; set; }
    public string? tinhthanh { get; set; }
    public string? dienthoai { get; set; }
    public string? email { get; set; }
    public string? chucdanhnghenghiep { get; set; }
    public string? chucdanh { get; set; }
    public string? namphongchucdanh { get; set; }
    public string? hocvi { get; set; }
    public string? namdathocvi { get; set; }
    public string? coquancongtac { get; set; }
    public string? linhvucnc { get; set; }
    public IEnumerable<LinhVucNghienCuu>? LinhVucNghienCuus { get; set; }


}