
namespace SoKHCNVTAPI.Models;

public class ActionResponse
{
    public required long Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public required string Tag { get; set; }

    public short? Status { get; set; }

    public required List<UserResponse> Users { get; set; }
}