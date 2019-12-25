using Dot.Net.WebApi.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

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
            var msg = username + " requested resource at " +
                typeof(T) + " " +caller + " on " + DateTime.Now.ToString();
            SaveLog(msg, "Info");
            _logger.LogInformation(msg);
        }

        public void LogError(string caller, string message)
        {
            var msg = "An error occured on: " + 
                typeof(T) + " " + caller + " Error Message: " + message;

            SaveLog(msg, "Error");
            _logger.LogError(msg);
        }

        private void SaveLog(string msg, string type = null)
        {
            try
            {
                if (!String.IsNullOrEmpty(msg))
                {
                    using (var context = new LocalDbContext())
                    {
                        context.Logs.Add(new AppLog { Details = msg, LogType = type });
                        context.SaveChanges();
                    }
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
