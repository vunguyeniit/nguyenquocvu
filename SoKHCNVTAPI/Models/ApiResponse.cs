using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class ApiResponse : BaseResponse
{
    public object? Data { get; set; } = "";

    public object? Action { get; set; } = "";
}