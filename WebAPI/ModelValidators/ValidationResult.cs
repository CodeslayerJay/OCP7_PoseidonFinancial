using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ModelValidators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ErrorMessages = new Dictionary<string, string>();
        }
        public bool IsValid { get; set; }
        public Dictionary<string, string> ErrorMessages { get; set; }
    }
}
