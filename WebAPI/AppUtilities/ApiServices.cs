using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AppUtilities
{
    public static class ApiServices
    {
        public static IEnumerable<string> GetModelErrors(ModelStateDictionary modelState)
        {
            var errors = new List<string>();

            if (modelState.ErrorCount <= 0)
                return errors;

            foreach (var value in modelState.Values)
            {
                if (value.Errors.Any())
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }

            return errors;

        }
    }
}
