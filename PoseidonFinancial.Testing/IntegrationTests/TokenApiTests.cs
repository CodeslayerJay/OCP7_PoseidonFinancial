using Dot.Net.WebApi;
using Dot.Net.WebApi.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Services;
using Xunit;

namespace PoseidonFinancial.Testing.IntegrationTests
{
    public class TokenApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/token/";

        public TokenApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public void Post_WhenUnAuthorized_ReturnsOk_WithNewAccessToken()
        {

            var tokenResource = new TokenResource
            {
                Username = "unitTester",
                Password = "test1234!"
            };

            var response = _client.PostAsync(APIROUTE + "validate", ClientHelper.EncodeContent(tokenResource)).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<JsonWebToken>(json);

            Assert.NotNull(result);
            Assert.IsType<JsonWebToken>(result);
        }

    }
}
