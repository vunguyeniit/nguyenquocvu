namespace SoKHCNVTAPI.Models;

public class UserFilterDto : PaginationDto
{
    public string? Keyword { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Phone { get; set; } = null;
    public  string? Code { get; set; }
    public  string? FullName { get; set; }
    public string? UpdatedAt { get; set; }
    public string? Address { get; set; }
    public long? GroupId { get; set; } = null;
    public short? Status { get; set; }
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
}