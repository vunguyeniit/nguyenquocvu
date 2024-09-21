namespace SoKHCNVTAPI.Helpers;

public static class Generator
{
    public static string MissionIdentifierCode() { return $"TIC-{Guid.NewGuid()}".ToUpper(); }

    public static string ResetPasswordCode()
    {
        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
        return new string(Enumerable.Repeat(characters, 8)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
}