using System.Security.Cryptography;

namespace InventarioAPI.Helpers.Utils
{
    public class PasswordHelper
    {
        public static (byte[] hash, byte[] salt) HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
            byte[] hashToCheck = pbkdf2.GetBytes(32);
            return CryptographicOperations.FixedTimeEquals(hashToCheck, hash);
        }
    }
}
