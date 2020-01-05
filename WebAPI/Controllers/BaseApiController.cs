using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppUtilities;
using WebApi.Services;

namespace Dot.Net.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public abstract class BaseApiController<TController> : Controller
    {
        public IAppLogger<TController> AppLogger { get; }
        
        public BaseApiController(IAppLogger<TController> logger)
        {
            AppLogger = logger;
        }

        internal BadRequestObjectResult BadRequestExceptionHandler(Exception exception, string caller)
        {
            AppLogger.LogError(caller, exception.Message);
            return BadRequest(new { Errors = exception.Message });
        }

        internal string GetRequestToken()
        {
            if (Request == null)
                return null;

            string token = Request.Headers["Authorization"];

            if (token == null)
                return null;

            return token.Replace("Bearer ", "");
        }

        internal string GetUsernameForToken()
        {
            var token = GetRequestToken();

            if (String.IsNullOrEmpty(token))
                return null;

            return AppSecurity.GetUsernameForToken(token);
        }

        internal void GetErrorsForModelState(Dictionary<string, string> errors)
        {
            if (errors.Any())
            {
                foreach(var error in errors)
                {
                    ModelState.AddModelError(error.Key, error.Value);
                }
            }
        }
    }
}
