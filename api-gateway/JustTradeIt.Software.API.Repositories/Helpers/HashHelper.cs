using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JustTradeIt.Software.API.Repositories.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: CreateSalt(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private static byte[] CreateSalt(string salt) =>
            Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(salt)));
    }
}