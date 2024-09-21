using DocumentFormat.OpenXml.Wordprocessing;
using QuestPDF.Helpers;
using SoKHCNVTAPI.Entities.Base;

namespace SoKHCNVTAPI.Models;

public class CommonFilterDto : PaginationDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? MissionNumber { get; set; }
    public decimal? GovernmentExpenses { get; set; }
    public decimal? SelfExpenses { get; set; }
    public decimal? OtherExpenses { get; set; }
    public int? TotalTimeWithMonth { get; set; }
    public decimal? TotalExpenses { get; set; }
    public string? AnticipatedProduct { get; set; }
    public short? Status { get; set; }
    public string? Keyword { get; set; } = null;
    public string? Email { get; set; } = null;
    public string? Phone { get; set; } = null;
    public long? GroupId { get; set; } = null;
    public string? order_by { get; set; }
    public string? sorted_by { get; set; }
    public string? UpdatedAt { get; set; } 

    //public int pageNumber { get; set; }
    //public int pageSize { get; set; }

    public CommonFilterDto()
    {
        //pageNumber = 1;
        //pageSize = 10;
    }

    //public CommonFilterDto(int pageNumber, int pageSize)
    //{
    //    pageNumber = pageNumber < 1 ? 1 : pageNumber;
    //    pageSize = pageSize < 1 ? 10 : pageSize > 100 ? 100 : pageSize;
    //}
}



//public class TTTrang
//{
//    public PhanTrang PhanTrang { get; set; }

//    public TTTrang(CommonFilterDto model, int records)
//    {
//        PhanTrang = new PhanTrang
//        {
//            CurrentPage = model.pageNumber,
//            PageSize = model.pageSize,
//            TotalPages = (int)Math.Ceiling(records / (float)model.pageSize),
//            TotalRecords = records
//        };
//    }
//}

//public class PhanTrang
//{
//    public int CurrentPage { get; set; }
//    public int PageSize { get; set; }
//    public int TotalPages { get; set; }
//    public int TotalRecords { get; set; }
//    public bool HasNext => CurrentPage < TotalPages;
//    public bool HasPrevious => CurrentPage > 1;
//}