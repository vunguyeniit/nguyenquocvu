using System.ComponentModel.DataAnnotations.Schema;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Entities;

[Table("Menus", Schema = "skhcn")]
public class Menu : BaseEntity
{
    public long Parent { get; set; }
    
    public required string Name { get; set; }
    
    public required string Slug { get; set; }
    
    public string? Icon { get; set; }

    public string? Module { get; set; }
}