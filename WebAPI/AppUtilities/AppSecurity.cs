using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public static class AppSecurity
    {

        public static JsonWebToken GenerateToken()
        {
            //Add Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, "data"),
                new Claim(JwtRegisteredClaimNames.Sub, "data"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("rlyaKithdrYVl6Z80ODU350md")); //Secret
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(AppConstants.DefaultTokenTimeout);

            var token = new JwtSecurityToken("me",
                "you",
                claims,
                expires: expires,
                signingCredentials: creds);

            var accessToken = new JsonWebToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = AppConstants.DefaultTokenTimeout,
                ExpiresAt = expires,
                Type = "bearer"
            };

            return accessToken;
        }

        public static HashResult HashPassword(string password, byte[] salt = null)
        {
            if (String.IsNullOrEmpty(password))
                throw new ArgumentNullException("password cannot be null.");

            if (salt == null)
            {
                salt = new byte[128 / 8];

                // Scramble salt
                //using (var rng = RandomNumberGenerator.Create())
                //{
                //    rng.GetBytes(salt);
                //}
            }


            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return new HashResult { HashedPassword = hashed, SaltKey = salt };
        }

        public static bool VerifyPasswords(string password, string hashedPassword)
        {
            var result = HashPassword(password);
            return result.HashedPassword == hashedPassword;
        }

        public static string GetUsernameForToken(string token)
        {
            if (String.IsNullOrEmpty(token))
                throw new ArgumentNullException();

            using(var context = new LocalDbContext())
            {
                var aToken = context.AccessTokens.Where(x => x.Token == token).FirstOrDefault();

                if(aToken != null)
                {
                    var user = context.Users.Where(x => x.Id == aToken.UserId).FirstOrDefault();

                    if(user != null)
                    {
                        return user.UserName;
                    }
                }
            }

            return null;
        }

        public class HashResult
        {
            public string HashedPassword { get; set; }
            public byte[] SaltKey { get; set; }

        }

        

    }

    public class JsonWebToken
    {
        public string Token { get; set; }
        public string Type { get; set; } = "bearer";
        public int Expires { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string RefreshToken { get; set; }
    }

}
