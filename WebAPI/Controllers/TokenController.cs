﻿using Dot.Net.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.AppUtilities;
using WebApi.Services;

namespace Dotnet.Web.WebApi.Controllers
{
    public class TokenController : BaseApiController<TokenController>
    {
        private readonly IUserService _userService;

        public TokenController(IAppLogger<TokenController> appLogger, IUserService userService) : base(appLogger)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Validate([FromBody]TokenResource resource)
        {
            if (!ModelState.IsValid) return BadRequest("Token failed to generate");

            if (!resource.RefreshToken && !_userService.ValidateUser(resource.Username, resource.Password))
                return Unauthorized();

            var token = AppSecurity.GenerateToken();

            return Ok(token);
        }
    }
}
