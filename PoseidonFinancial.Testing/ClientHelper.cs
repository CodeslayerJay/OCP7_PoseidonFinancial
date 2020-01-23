using Dot.Net.WebApi.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using WebApi.AppUtilities;

namespace PoseidonFinancial.Testing
{
    public static class ClientHelper
    {
        
        // Retrieves and stores an access token for authorization requests
        public static HttpClient AuthorizeRequest(this HttpClient httpClient, string username = null)
        {
            var token = GetAccessToken(username);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            return httpClient;
        }
        
        // Gets an access token for requests
        public static string GetAccessToken(string username = null)
        {
            using (var context = new LocalDbContext())
            {
                if (String.IsNullOrEmpty(username))
                    username = "unitTester";

                var user = context.Users.Where(x => x.UserName == username).FirstOrDefault();
                
                if (user == null)
                    return "";

                var token = AppSecurity.GenerateToken(user.Id);

                return (token == null || String.IsNullOrEmpty(token.Token)) ? "" : token.Token;
            }
        }

        // Builds content for making post/put requests
        public static StringContent EncodeContent(object formData)
        {
            var jsonData = JsonConvert.SerializeObject(formData);

            return new StringContent(jsonData, Encoding.UTF8, "application/json");
        }

    }
}
