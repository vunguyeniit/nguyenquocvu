namespace SoKHCNVTAPI.Models;

public class WorkHistoryDto
{
    public required long OfficerId { get; set; }
    public string? Times { get; set; }
    public string? Position { get; set; }
    public string? Organization { get; set; }
}
