using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class RuleName
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        [StringLength(20)]
        public string JSON { get; set; }

        [StringLength(20)]
        public string Template { get; set; }

        [StringLength(20)]
        public string SqlString { get; set; }

        [StringLength(20)]
        public string SqlPart { get; set; }
    }
}