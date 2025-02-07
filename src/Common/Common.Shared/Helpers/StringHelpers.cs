namespace Common.Shared.Helpers;

public static class StringHelpers
{
    public static string GenerateRandomString(int size = 10)
    {
        var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(
            [.. Enumerable
                .Repeat(caracteres, size)
                .Select(s => s[random.Next(s.Length)])]);
    }
}
