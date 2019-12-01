using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.AppUtilities;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{

    public class UserController : BaseApiController<UserController>
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService, IAppLogger<UserController> appLogger) : base(appLogger)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUserByUsername(string username)
        {
            AppLogger.LogResourceRequest(nameof(GetUserByUsername), "test");
            try
            {
                var result = _userService.FindByUsername(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequestExceptionHandler(ex, nameof(GetUserByUsername));
            }
        }
        
        

    }
}