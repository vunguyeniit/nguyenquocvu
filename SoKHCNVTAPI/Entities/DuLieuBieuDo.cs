using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities;

[Table("DuLieuBieuDo", Schema = "skhcn")]
public class DuLieuBieuDo
{
    [Key]
    public long Id { get; init; }

    public long BieuDoId { get; set; }

    [StringLength(150)]
    public required string MaDuLieu { get; set; }

    [StringLength(10000)]
	public required string NoiDung { get; set; }

    public required long NguoiCapNhat { get; set; }

    public DateTime? NgayCapNhat { get; set; }

    //public required virtual ICollection<DuLieuBieuDo> DuLieuBieuDos { get; set; }
}

public class DuLieuBieuDoFilter : PaginationDto
{
    public long? BieuDoId { get; set; }
    public string? TuKhoa { get; set; }
}

