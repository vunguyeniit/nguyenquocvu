namespace SoKHCNVTAPI.Models;

public class SignInDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }

    public required string Session { get; set; }
}