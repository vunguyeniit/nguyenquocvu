using SoKHCNVTAPI.Models;
using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class PaginationBaseResponse : BaseResponse
{
    public object? Data { get; set; }
    public Meta? Meta { get; set; }

    //public TTTrang? TTTrang { get; set; }
}