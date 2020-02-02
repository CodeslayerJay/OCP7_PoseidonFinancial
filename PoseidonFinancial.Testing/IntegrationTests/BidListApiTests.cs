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
    [Collection("CustomWebAppFactory")]
    public class BidListApiTests // : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/bidlist/";

        public BidListApiTests(CustomWebApplicationFactory factory)
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
        public void Get_ReturnsOk_WithListOfBids()
        {
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<BidResource>>(json);

            Assert.IsType<List<BidResource>>(result);
        }

        [Fact]
        public void Post_ReturnsOk_WithNewBidResource()
        {

            var bid = new EditBidResource
            {
                Account = "tester",
                BidQuantity = "5",
                Type = "Buy"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(bid)).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BidResource>(json);

            Assert.NotNull(result);
            Assert.IsType<BidResource>(result);
        }

        [Fact]
        public void Post_WhenResourceIsInvalied_ReturnsBadRequestWithValidationErrors()
        {

            var bid = new EditBidResource
            {
                Account = "tester",
                BidQuantity = "notvalid", // This will fail validation since cannot convert to double
                Type = "Buy"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(bid)).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);
        }


        [Fact]
        public void GetById_ReturnsOk_WithBidResource()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getBids = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getBids.EnsureSuccessStatusCode();

            var bidsRaw = getBids.Content.ReadAsStringAsync().Result;
            var bids = JsonConvert.DeserializeObject<IEnumerable<BidResource>>(bidsRaw);

            // Verify we have something to look up
            Assert.NotEmpty(bids);

            // The Test
            // Arrange - Grab first one
            var bid = bids.FirstOrDefault();
            
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + bid.BidListId).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<BidResource>(json);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BidResource>(result);
        }

        [Fact]
        public void GetById_WhenBidIsNull_ReturnsNotFound()
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
            var getBids = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getBids.EnsureSuccessStatusCode();

            var bidsRaw = getBids.Content.ReadAsStringAsync().Result;
            var bids = JsonConvert.DeserializeObject<IEnumerable<BidResource>>(bidsRaw);

            // Verify we have something to look up
            Assert.NotEmpty(bids);

            // The Test
            // Arrange - Grab first one
            var bid = bids.FirstOrDefault();

            var editedBid = new EditBidResource
            {
                Account = "tester2",
                BidQuantity = "3",
                Type = "Sell"
            };
            
            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + bid.BidListId, ClientHelper.EncodeContent(editedBid)).Result;
            
            // Assert Ok
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public void Put_WhenResourceInvalid_ReturnsWithValidationErrors()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getBids = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getBids.EnsureSuccessStatusCode();

            var bidsRaw = getBids.Content.ReadAsStringAsync().Result;
            var bids = JsonConvert.DeserializeObject<IEnumerable<BidResource>>(bidsRaw);

            // Verify we have something to look up
            Assert.NotEmpty(bids);

            // The Test
            // Arrange - Grab first one
            var bid = bids.FirstOrDefault();

            var editedBid = new EditBidResource
            {
                Account = "tester2",
                BidQuantity = "thisIsInvalid", // Validation should fail to convert to double
                Type = "Sell"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + bid.BidListId, ClientHelper.EncodeContent(editedBid)).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);

        }


        [Fact]
        public void Put_WhenBidIsNull_ReturnsNotFound()
        {
            // Arrange
            var editedBid = new EditBidResource
            {
                Account = "tester2",
                BidQuantity = "3",
                Type = "Sell"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + 99999, ClientHelper.EncodeContent(editedBid)).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getBids = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getBids.EnsureSuccessStatusCode();

            var bidsRaw = getBids.Content.ReadAsStringAsync().Result;
            var bids = JsonConvert.DeserializeObject<IEnumerable<BidResource>>(bidsRaw);

            // Verify we have something to look up
            Assert.NotEmpty(bids);

            // The Test
            // Arrange - Grab first one
            var bid = bids.FirstOrDefault();

            // Act
            var response = _client.AuthorizeRequest().DeleteAsync(APIROUTE + bid.BidListId).Result;

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
