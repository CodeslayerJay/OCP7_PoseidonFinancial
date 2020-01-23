using Dot.Net.WebApi;
using Dot.Net.WebApi.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using WebApi.ApiResources;
using WebApi.Services;
using Xunit;

namespace PoseidonFinancial.Testing.IntegrationTests
{
    public class CurveApiTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/curve/";

        public CurveApiTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        [Theory]
        [InlineData("controller")]
        [InlineData("get")]
        [InlineData("put")]
        [InlineData("post")]
        [InlineData("delete")]
        public void RouteAction_WhenNotAuthorized_ReturnsNotAuthorized(string actionType)
        {
            HttpResponseMessage response = null;
            // Act
            switch (actionType)
            {
                case "controller":
                    response = _client.GetAsync(APIROUTE).Result;
                    break;
                case "get":
                    response = _client.GetAsync(APIROUTE).Result;
                    break;
                case "put":
                    response = _client.PutAsync(APIROUTE + 99999, null).Result;
                    break;
                case "post":
                    response = _client.PostAsync(APIROUTE, null).Result;
                    break;
                case "delete":
                    response = _client.DeleteAsync(APIROUTE + 9999).Result;
                    break;
                default:
                    response = _client.GetAsync(APIROUTE).Result;
                    break;
            }


            // Assert
            Assert.NotNull(response);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public void Get_ReturnsOk_WithListOfCurveResources()
        {
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<CurveResource>>(json);

            Assert.IsType<List<CurveResource>>(result);
        }

        [Fact]
        public void Post_ReturnsOk_WithNewCurveResource()
        {

            var curve = new EditCurveResource
            {
                CurveId = "4",
                Term = "5",
                Value = "5"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(curve)).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CurveResource>(json);

            Assert.NotNull(result);
            Assert.IsType<CurveResource>(result);
        }

        [Fact]
        public void Post_WhenResourceIsInvalied_ReturnsBadRequestWithValidationErrors()
        {

            var curve = new EditCurveResource
            {
                CurveId = "4",
                Term = "5",
                Value = "sadasd"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(curve)).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);
        }


        [Fact]
        public void GetById_ReturnsOk_WithCurveResource()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getResources.EnsureSuccessStatusCode();

            var resourcesRaw = getResources.Content.ReadAsStringAsync().Result;
            var resources = JsonConvert.DeserializeObject<IEnumerable<CurveResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();
            
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + resource.Id).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CurveResource>(json);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CurveResource>(result);
        }

        [Fact]
        public void GetById_WhenNull_ReturnsNotFound()
        {
            // Arrange + Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + 99999).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public void Put_ReturnsOk()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getResources.EnsureSuccessStatusCode();

            var resourcesRaw = getResources.Content.ReadAsStringAsync().Result;
            var resources = JsonConvert.DeserializeObject<IEnumerable<CurveResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();

            var editedCurve = new EditCurveResource
            {
                CurveId = "3",
                Term = "4",
                Value = "3"
            };
            
            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(editedCurve)).Result;
            
            // Assert Ok
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public void Put_WhenResourceInvalid_ReturnsWithValidationErrors()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getResources.EnsureSuccessStatusCode();

            var resourcesRaw = getResources.Content.ReadAsStringAsync().Result;
            var resources = JsonConvert.DeserializeObject<IEnumerable<CurveResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();

            var editedCurve = new EditCurveResource
            {
                CurveId = "3",
                Term = "4",
                Value = "asdad"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(editedCurve)).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);

        }


        [Fact]
        public void Put_WhenNull_ReturnsNotFound()
        {
            var editedCurve = new EditCurveResource
            {
                CurveId = "3",
                Term = "4",
                Value = "5"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + 99999, ClientHelper.EncodeContent(editedCurve)).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getResources.EnsureSuccessStatusCode();

            var resourcesRaw = getResources.Content.ReadAsStringAsync().Result;
            var resources = JsonConvert.DeserializeObject<IEnumerable<CurveResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();

            // Act
            var response = _client.AuthorizeRequest().DeleteAsync(APIROUTE + resource.Id).Result;

            // Assert Ok
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public void Delete_WhenBidIsNull_ReturnsNotFound()
        {
            // Arrange + Act
            var response = _client.AuthorizeRequest().DeleteAsync(APIROUTE + 99999).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
