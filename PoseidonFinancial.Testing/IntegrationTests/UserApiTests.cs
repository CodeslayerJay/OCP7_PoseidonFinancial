﻿using Dot.Net.WebApi;
using Dot.Net.WebApi.Data;
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
    [Collection("CustomWebAppFactory")]
    public class UserApiTests //: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/user/";

        public UserApiTests(CustomWebApplicationFactory factory)
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
                    response = _client.GetAsync(APIROUTE + "username/" + TestSeedData.TestUsername).Result;
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
            var username = TestSeedData.TestUsername;

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
            // Has to be a unique username
            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = "tempUsername",
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


            // Do cleanup
            CleanUp();
        }

        [Fact]
        public void Post_WhenResourceIsInvalied_ReturnsBadRequestWithValidationErrors()
        {
            // Arrange - Passwords do not match, username taken to be sure for validation failure
            var user = new EditUserResource
            {
                FullName = "Testing Tests",
                UserName = TestSeedData.TestUsername,    // username should already be taken so fails here
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
            var username = TestSeedData.TestUsername;
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
            var username = TestSeedData.TestUsername;
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE + "username/" + username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);

            // Let's just change the name since other tests rely on having this
            // username & password
            var user = new EditUserResource
            {
                FullName = "Test Name Change",
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
            var username = TestSeedData.TestUsername;
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
           
            var getResources = _client.AuthorizeRequest(username).GetAsync(APIROUTE + "username/"+ username).Result;
            getResources.EnsureSuccessStatusCode();

            var resourceRaw = getResources.Content.ReadAsStringAsync().Result;
            var resource = JsonConvert.DeserializeObject<UserResource>(resourceRaw);

            // Verify we have something to look up
            Assert.NotNull(resource);

            // Act
            var response = _client.AuthorizeRequest(username).DeleteAsync(APIROUTE + resource.Id).Result;

            // Assert Ok
            response.EnsureSuccessStatusCode();

            CleanUp();
        }


        // Clean up the db here
        private void CleanUp()
        {
            try
            {

                using (var context = new LocalDbContext())
                {
                    var tempUser = context.Users.Where(x => x.UserName == "tempUsername").FirstOrDefault();
                    var dummyUser = context.Users.Where(x => x.UserName == "dummy").FirstOrDefault();

                    if (tempUser != null)
                    {
                        context.Users.Remove(tempUser);
                        
                    }

                    if(dummyUser == null)
                    {
                        var dummy = new User
                        {
                            FullName = "Dummy User",
                            UserName = "dummy",
                            Password = AppSecurity.HashPassword("test1234!").HashedPassword,
                            Role = "Tester"
                        };

                        context.Users.Add(dummy);
                    }

                    context.SaveChanges();
                }
                
            }
            catch (Exception ex) { }

            
        }

    }
}
