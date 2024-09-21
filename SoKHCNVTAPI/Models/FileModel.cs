using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;
public class FileModel
{
    public required IFormFile file { get; set; }
    public required string type { get; set; }
    public string? subFolder { get; set; }
}