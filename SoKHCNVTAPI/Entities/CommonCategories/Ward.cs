using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities.CommonCategories;

[Table("Wards", Schema = "skhcn")]
public class Ward : BaseEntity
{
    [StringLength(100)]
    public required string Name { get; set; }
    
    [StringLength(50)]
    public required long Code { get; set; }

    public required long DistrictId { get; set; }
    public District District { get; set; } = null!;
}

public class WardDto
{
    [StringLength(100)]
    public required string Name { get; set; }
    
    [StringLength(50)]
    public required long Code { get; set; }

    public required long DistrictId { get; set; }
}

public class WardFilter : IKeyword
{
    public string? Name { get; set; }
    public string? Keyword { get; set; }
}