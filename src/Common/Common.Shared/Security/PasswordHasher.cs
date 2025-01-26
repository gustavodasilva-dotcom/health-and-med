using System.Security.Cryptography;

namespace Common.Shared.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int _saltSize = 16;
    private const int _hashSize = 32;
    private const int _iterations = 100000;

    private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] hashedInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _algorithm, _hashSize);

        return CryptographicOperations.FixedTimeEquals(hash, hashedInput);
    }
}
