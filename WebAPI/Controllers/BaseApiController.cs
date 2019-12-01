using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppUtilities;

namespace Dot.Net.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class BaseApiController<TController> : Controller
    {
        public IAppLogger<TController> AppLogger { get; }

        public BaseApiController(IAppLogger<TController> logger)
        {
            AppLogger = logger;
        }
        internal BadRequestObjectResult BadRequestExceptionHandler(Exception exception, string caller)
        {
            AppLogger.LogError(exception.Message, caller);
            return BadRequest(new { Errors = exception.Message });
        }
    }
}
