using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("Provinces", Schema = "skhcn")]
public class TinhThanh : BaseEntity
{
    [StringLength(100)]
    public required string Name { get; set; }
    public required long Code { get; set; }
    public ICollection<District> Districts { get; set; } = null!;
}

public class ProvinceDto
{
    [StringLength(100)]
    public required string Name { get; set; }
    public required long Code { get; set; }
    public short? Status { get; set; }
}

public class ProvinceFilter : IKeyword
{
    public string? Name { get; set; }
    public string? Keyword { get; set; }
}

