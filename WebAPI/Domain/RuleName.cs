using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class RuleName
    {
        [Key]
        public int Id { get; set; }

        
        public string Name { get; set; }

        
        public string Description { get; set; }

        
        public string JSON { get; set; }

        
        public string Template { get; set; }

        
        public string SqlString { get; set; }

        
        public string SqlPart { get; set; }
    }
}