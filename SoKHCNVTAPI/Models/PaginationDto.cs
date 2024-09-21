namespace SoKHCNVTAPI.Models;

public class PaginationDto
{
    
    public long? MaCanBo { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public PaginationDto()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public PaginationDto(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 10 : pageSize > 100 ? 100 : pageSize;
    }
}