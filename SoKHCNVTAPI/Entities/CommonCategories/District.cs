using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;
using SoKHCNVTAPI.Models;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("Districts", Schema = "skhcn")]
public class District : BaseEntity
{
    [StringLength(100)]
    public required string Name { get; set; }
    public required long Code { get; set; }
    public required long ProvinceId { get; set; }
    public TinhThanh Province { get; set; } = null!;
    public ICollection<Ward> Wards { get; set; } = null!;
}

public class DistrictDto
{
    [StringLength(100)]
    public required string Name { get; set; }
    public required long Code { get; set; }
    public required long ProvinceId { get; set; }
    public short? Status { get; set; }
}
