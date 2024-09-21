namespace SoKHCNVTAPI.Models.Base;

public class BaseResponse
{
    public string Message { get; set; } = "Ok";
    public bool Success { get; set; } = true;
    public int ErrorCode { get; set; } = 0;
}