namespace SoKHCNVTAPI.Models;

public class PagedResponse<T> : ApiResponse
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri? FirstPage { get; set; }
    public Uri? LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri? NextPage { get; set; }
    public Uri? PreviousPage { get; set; }

    // public PagedResponse(T obj, bool Success, int ErrorCode, string Message)
    // {
    //     this.Data = obj;
    //     this.Success = Success;
    //     this.ErrorCode = ErrorCode;
    //     this.Message = Message;
    // }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
        this.Data = data;
        this.Message = "";
        this.Success = true;
        this.ErrorCode = 0;
    }
}