using Dot.Net.WebApi.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public class PerformLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            using(var dbContext = new LocalDbContext())
            {

            }
        }
    }
}
