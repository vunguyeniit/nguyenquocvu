using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("BieuDo", Schema = "skhcn")]
public class BieuDo {
    [Key]
    public long Id { get; init; }

    public required long BieuDoMauId { get; set; }
    [StringLength(50)]
    public required string Ma { get; set; }

    [StringLength(50)]
	public required string TieuDe { get; set; }

    [StringLength(500)]
    public required string MoTa { get; set; }

    [StringLength(10000)]
    public string? ThongKe { get; set; }

    public required long NguoiCapNhat { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    public virtual ICollection<DuLieuBieuDo>? DuLieuBieuDos { get; set; }
}

public class BieuDoFilter : PaginationDto
{
    public string? Ma { get; set; } = "";
    public long? BieuDoMauId { get; set; } = 0;
    //public long? BieuDoMauId { get; set; } = 0;
    public string? TuKhoa { get; set; } = "";

}
