namespace SoKHCNVTAPI.Models;

using SoKHCNVTAPI.Entities;

public class AuthenticateResponse
{
    // public long Id { get; set; }
    // public string? FirstName { get; set; }
    // public string? LastName { get; set; }
    public User User { get; set; }
    public string Token { get; set; }

    public int ErrorCode { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }


    public AuthenticateResponse(User user, string token, bool Success, string Message, int ErrorCode)
    {
        this.Success = Success;
        this.Message = Message;
        this.ErrorCode = ErrorCode;
        this.User = user;
        this.Token = token;
    }
}