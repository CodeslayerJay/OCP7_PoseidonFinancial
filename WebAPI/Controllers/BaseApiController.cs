using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.ApiResources;
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

        // Gets the username for the requesting authorized claim/token
        internal string GetUsernameForRequest()
        {
            try
            {
                var userClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!String.IsNullOrEmpty(userClaim))
                {
                    // Make sure we have a valid id that can be parsed to an int
                    if (Int32.TryParse(userClaim, out int id))
                    {
                        return AppSecurity.GetClaimUsernameById(id);
                    }
                }
            }
            catch(Exception ex)
            {
                AppLogger.LogError(nameof(GetUsernameForRequest),ex.Message);
            }

            // Something happened so just return empty string
            return String.Empty;
        }


        internal BadRequestObjectResult BadRequestExceptionHandler(Exception exception, string caller)
        {
            AppLogger.LogError(caller, exception.Message);
            return BadRequest(new { Errors = exception.Message });
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
