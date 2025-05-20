using System;
using System.Security.Cryptography;

namespace PasswordHashingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a password to hash:");
            string password = Console.ReadLine();

            byte[] salt = GenerateSalt(16);
            Console.WriteLine($"Salt (Base64): {Convert.ToBase64String(salt)}");

            string hashedPassword = GeneratePasswordHashUsingSalt(password, salt);
            Console.WriteLine($"Password Hash (Base64): {hashedPassword}");

            Console.ReadKey();
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            if (string.IsNullOrEmpty(passwordText)) throw new ArgumentException("Password cannot be null or empty");
            
            if (salt == null || salt.Length != 16) throw new ArgumentException("Salt must be exactly 16 bytes.");

            const int iterations = 10000;
            const int hashLength = 20;
            const int totalLength = 16 + 20;

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterations))
            {
                byte[] hashBytes = new byte[totalLength];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(pbkdf2.GetBytes(hashLength), 0, hashBytes, 16, hashLength);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public static byte[] GenerateSalt(int size)
        {
            byte[] salt = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }
    }
}