using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ApiResources
{
    public class RuleResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string JSON { get; set; }
        public string Template { get; set; }
        public string SqlString { get; set; }
        public string SqlPart { get; set; }
    }
}
