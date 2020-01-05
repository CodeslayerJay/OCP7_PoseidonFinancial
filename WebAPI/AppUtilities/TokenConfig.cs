using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public static class TokenConfig
    {
        public const string ValidIssuer = "PoseidonFinancialApp";
        public const string ValidAudience = "PoseidonFinancialApp_User";
        public static TimeSpan SkewTime = TimeSpan.Zero;
        public static DateTime Expires = DateTime.Now.AddMinutes(DefaultTokenTimeout);
        public const int DefaultTokenTimeout = 20;

        private static byte[] _key = Encoding.UTF8.GetBytes("rlyaKithdrYVl6Z80ODU350md");
        

        public static SymmetricSecurityKey GetKey()
        {
            var key = new SymmetricSecurityKey(_key);
            return key;
        }
    }
}
