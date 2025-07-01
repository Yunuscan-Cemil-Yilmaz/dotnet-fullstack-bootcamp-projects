using System.Security.Cryptography;
using System.Text;

namespace PollCraft.Services
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }

    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // First hash: SHA512
            using var sha512 = SHA512.Create();
            var shaHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            var shaString = Convert.ToBase64String(shaHash);

            // Second hash: BCrypt
            return BCrypt.Net.BCrypt.HashPassword(shaString);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            using var sha512 = SHA512.Create();
            var shaHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
            var shaString = Convert.ToBase64String(shaHash);

            if (BCrypt.Net.BCrypt.Verify(shaString, hashedPassword)) return true;
            else return false;
        }
    }
}
