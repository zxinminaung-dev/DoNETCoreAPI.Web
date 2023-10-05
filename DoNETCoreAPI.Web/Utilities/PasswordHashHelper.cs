using System.Security.Cryptography;
using System.Web.Helpers;

namespace DoNETCoreAPI.Web.Utilities
{
    public class PasswordHashHelper
    {
        public PasswordHashHelper()
        {

        }
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }
        public static bool ValidatePassword(string hashPassword, string password)
        {
            return Crypto.VerifyHashedPassword(hashPassword, password);
        }
        public static string GetReadomSalt()
        {
            return Crypto.GenerateSalt(16);
        }
    }
}
