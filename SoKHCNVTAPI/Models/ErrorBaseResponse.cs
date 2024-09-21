using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class ErrorBaseResponse : BaseResponse
{
    public Dictionary<string, string[]>? Errors { get; set; }
}