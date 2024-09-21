using SoKHCNVTAPI.Models.Base;

namespace SoKHCNVTAPI.Models;

public class SignInBaseResponse: BaseResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    
    public UserResponse? User { get; set; }
}

public class SignInAPPResponse : BaseResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }

    public UserResponseAPP? User { get; set; }
}