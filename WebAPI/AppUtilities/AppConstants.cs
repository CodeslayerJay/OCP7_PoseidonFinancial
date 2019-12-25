using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public static class AppConstants
    {
        public const string GenericErrorMsg = "An error occurred. Please try again or contact support.";
        public const string ResourceNotFoundById = "The resource was not found using the id: ";
        public const int DefaultTokenTimeout = 20;
    }
}
