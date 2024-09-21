using BC = BCrypt.Net.BCrypt;

namespace SoKHCNVTAPI.Helpers;

public static class PasswordBuilder
{
    public static string HashBCrypt(string password)
    {
        return BC.HashPassword(password, BC.GenerateSalt());
    }

    public static bool VerifyBCrypt(string password, string correctHash)
    {
        return BC.Verify(password, correctHash);
    }
}