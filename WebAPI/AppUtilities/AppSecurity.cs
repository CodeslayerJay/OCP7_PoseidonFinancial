using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public static class AppSecurity
    {
        public static HashResult HashPassword(string password, byte[] salt = null)
        {
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password cannot be null.");

            if (salt == null)
            {
                salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            }


            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new HashResult { HashedPassword = hashed, SaltKey = salt };
        }

        //public static bool VerifyPasswords(UserCredential userCreds, string password)
        //{
        //    var result = HashPassword(password, userCreds.GetKey());
        //    return result.HashedPassword == userCreds.Password;
        //}

        public class HashResult
        {
            public string HashedPassword { get; set; }
            public byte[] SaltKey { get; set; }

        }
    }
}
