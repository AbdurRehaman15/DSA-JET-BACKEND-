using System;
using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;

namespace DsaJet.Api.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;  // Recommended salt size

        public static string HashPassword(string password)
        {
            // Generate a secure random salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            string saltBase64 = Convert.ToBase64String(salt);

            // Hash the password using Argon2id
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing, // Argon2id
                Version = Argon2Version.Nineteen,
                TimeCost = 10,       // Number of iterations
                MemoryCost = 65536, // 64MB memory usage
                Lanes = 5,          // Number of parallel threads
                Threads = Environment.ProcessorCount,
                HashLength = 32,    // Output hash length
                Salt = salt,        // Pass salt properly
                Password = Encoding.UTF8.GetBytes(password) // Convert password to bytes
            };

           string hash = Argon2.Hash(config);

            return $"{saltBase64}:{hash}";  // Store salt and hash separately
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(":");
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHashValue = parts[1];

            // Configure Argon2id for verification
            var config = new Argon2Config
            {
                Type = Argon2Type.DataIndependentAddressing, // Argon2id
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 65536,
                Lanes = 5,
                Threads = Environment.ProcessorCount,
                HashLength = 32,
                Salt = salt,
                Password = Encoding.UTF8.GetBytes(password)
            };

            string computedHash = Argon2.Hash(config);

            return CryptographicOperations.FixedTimeEquals(
              Convert.FromBase64String(storedHashValue), 
              Convert.FromBase64String(computedHash)
            );
        }
    }
}
