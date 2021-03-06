﻿using Dot.Net.WebApi;
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
    public class RulenameApiTests //: IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        private const string APIROUTE = "api/rulename/";

        public RulenameApiTests(CustomWebApplicationFactory factory)
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
        public void Get_ReturnsOk_WithListOfRuleNameResources()
        {
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<IEnumerable<RuleNameResource>>(json);

            Assert.IsType<List<RuleNameResource>>(result);
        }

        [Fact]
        public void Post_ReturnsOk_WithNewRuleNameResource()
        {

            var ruleName = new EditRuleNameResource
            {
                Description = "blah",
                Name = "Blah",
                Template = "Yo!"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(ruleName)).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RuleNameResource>(json);

            Assert.NotNull(result);
            Assert.IsType<RuleNameResource>(result);
        }

        [Fact]
        public void Post_WhenResourceIsInvalied_ReturnsBadRequestWithValidationErrors()
        {

            var ruleName = new EditRuleNameResource
            {
                Description = "blah",
                Name = "Blahasd asd asaaaaaaaaaaaa dddddddddddddddddddddddddddddddddd",
                Template = "Yo!"
            };

            var response = _client.AuthorizeRequest().PostAsync(APIROUTE, ClientHelper.EncodeContent(ruleName)).Result;
            var json = response.Content.ReadAsStringAsync().Result;

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);
        }


        [Fact]
        public void GetById_ReturnsOk_WithRuleNameResource()
        {
            // Get a list of bids so we can grab the correct id that is stored in
            // the (in)memory db
            var getResources = _client.AuthorizeRequest().GetAsync(APIROUTE).Result;
            getResources.EnsureSuccessStatusCode();

            var resourcesRaw = getResources.Content.ReadAsStringAsync().Result;
            var resources = JsonConvert.DeserializeObject<IEnumerable<RuleNameResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();
            
            // Act
            var response = _client.AuthorizeRequest().GetAsync(APIROUTE + resource.Id).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RuleNameResource>(json);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<RuleNameResource>(result);
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
            var resources = JsonConvert.DeserializeObject<IEnumerable<RuleNameResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();

            var ruleName = new EditRuleNameResource
            {
                Description = "blah",
                Name = "Blah",
                Template = "Yo!"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(ruleName)).Result;
            
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
            var resources = JsonConvert.DeserializeObject<IEnumerable<RuleNameResource>>(resourcesRaw);

            // Verify we have something to look up
            Assert.NotEmpty(resources);

            // The Test
            // Arrange - Grab first one
            var resource = resources.FirstOrDefault();

            var ruleName = new EditRuleNameResource
            {
                Description = "blaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaah",
                Name = "Blah",
                Template = "Yo!"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + resource.Id, ClientHelper.EncodeContent(ruleName)).Result;
            var json = response.Content.ReadAsStringAsync().Result;
            
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("validation errors", json);

        }


        [Fact]
        public void Put_WhenNull_ReturnsNotFound()
        {
            var ruleName = new EditRuleNameResource
            {
                Description = "blah",
                Name = "Blah",
                Template = "Yo!"
            };

            // Act
            var response = _client.AuthorizeRequest().PutAsync(APIROUTE + 99999, ClientHelper.EncodeContent(ruleName)).Result;

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
            var resources = JsonConvert.DeserializeObject<IEnumerable<RuleNameResource>>(resourcesRaw);

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
