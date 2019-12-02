using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }
        
        public string Password { get; set; }

        [StringLength(20)]
        public string FullName { get; set; }

        [StringLength(20)]
        public string Role { get; set; }
    }
}