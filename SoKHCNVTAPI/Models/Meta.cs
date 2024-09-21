namespace SoKHCNVTAPI.Models;

public class Meta
{
    public Pagination Pagination { get; set; }

    public Meta(PaginationDto model, int records)
    {
        Pagination = new Pagination
        {
            CurrentPage = model.PageNumber,
            PageSize = model.PageSize,
            TotalPages = (int)Math.Ceiling(records / (float)model.PageSize),
            TotalRecords = records
        };
    }
}

public class Pagination
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public bool HasNext => CurrentPage < TotalPages;
    public bool HasPrevious => CurrentPage > 1;
}