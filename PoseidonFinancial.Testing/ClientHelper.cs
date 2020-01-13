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
        public static HttpClient AuthorizeRequest(this HttpClient httpClient)
        {

            var token = GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            return httpClient;
        }
        
        // Gets an access token for requests
        public static string GetAccessToken()
        {
            using (var context = new LocalDbContext())
            {
                var user = context.Users.Where(x => x.UserName == TestSeedData.TestUsername).FirstOrDefault();

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
