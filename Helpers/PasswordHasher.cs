using System;
using System.Security.Cryptography;

namespace DsaJet.Api.Helpers;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 1000;

    public static string HashPassword(string password) {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512);

        byte[] hash = pbkdf2.GetBytes(KeySize);

        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    } 

    public static bool VerifyPassword(string password, string storedHash){
        var parts = storedHash.Split(":");

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedHashBytes = Convert.FromBase64String(parts[1]);


        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA512);
        byte[] computedHash = pbkdf2.GetBytes(KeySize);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);
    }
}
