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
    public class UserApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/user/";

        public UserApiTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        [Theory]
        [InlineData("getByUsername")]
        [InlineData("getById")]
        [InlineData("put")]
        [InlineData("delete")]
        public void RouteAction_WhenNotAuthorized_ReturnsNotAuthorized(string actionType)
        {
            HttpResponseMessage response = null;
            // Act
            switch (actionType)
            {
        
                case "getByUsername":
                    response = _client.GetAsync(APIROUTE + "username/unitTester").Result;
                    break;
                case "getById":
                    response = _client.GetAsync(APIROUTE + "getbyid/1").Result;
                    break;
                case "put":
                    response = _client.PutAsync(APIROUTE + 99999, null).Result;
                    break;
                case "delete":
                    response = _client.DeleteAsync(APIROUTE + 99999).Result;
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
        public void GetByUsername_ReturnsOk_WithUserResource()
        {
            // Act
            var username = "unitTester";

            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/"+ username).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserResource>(json);

            Assert.IsType<UserResource>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void Post_WhenUnAuthorized_ReturnsOk_WithNewUserResource()
        {

            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "chesterTester",
                Password = "a1234!",
                PasswordConfirm = "a1234!",
                Role = "Tester"
            };

            var response = _client.PostAsync(APIROUTE, ClientHelper.EncodeContent(user)).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserResource>(json);

            Assert.NotNull(result);
            Assert.IsType<UserResource>(result);
        }

        [Fact]
        public void Post_WhenResourceIsInvalied_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange - Passwords do not match, username taken to be sure for validation failure
            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "unitTester",    // username should already be taken so fails here
                Password = "1234", // password not strong enough fails here
                PasswordConfirm = "12341zxzxc", // password doesn't match fails here
                Role = "Tester"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(user)).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);
        }


        [Fact]
        public void GetById_ReturnsOk_WithUserResource()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var username = "unitTester";
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/"+ username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);

            
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + "getbyid/"+resource.Id).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<UserResource>(json);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserResource>(result);
        }

        [Fact]
        public void GetById_WhenNull_ReturnsNotFound()
        {
            // Arrange + Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE +"getbyid/"+ 99999).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public void Put_ReturnsOk()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var username = "unitTester";
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/" + username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);


            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "unitTester",
                Password = "test1234!",
                PasswordConfirm = "test1234!",
                Role = "Tester"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(user)).Result;
            
            // Assert Ok
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public void Put_WhenUserResourceInvalid_ReturnsWithValidationErrors()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var username = "unitTester";
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/" +username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);

            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "unitTester",
                Password = "test1234!", // Passwords do not match
                PasswordConfirm = "test1234!zzzz",
                Role = "Tester"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(user)).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);

        }


        [Fact]
        public void Put_WhenNull_ReturnsNotFound()
        {
            // Make sure is valid or won't pass validation and return a 500 - json problem
            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "chesterTester",
                Password = "a1234!",
                PasswordConfirm = "a1234!",
                Role = "Tester"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + 99999, ClientHelper.EncodeContent(user)).Result;

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var username = "dummy";

            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/"+ username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);

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
