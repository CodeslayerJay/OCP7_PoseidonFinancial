using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;


        public AppLogger(ILogger<T> logger)
        {
            _logger = logger;

        }

        public void LogResourceRequest(string caller, string username)
        {
            var msg = username + " requested resource at " + caller + " on " + DateTime.Now.ToString();
            _logger.LogInformation(msg);
        }

        public void LogError(string message, string caller)
        {
            var msg = "An error occured on: " + caller + " Error Message: " + message;
            _logger.LogError(msg);
        }
    }
}
