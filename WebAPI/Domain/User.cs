using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        
        public string UserName { get; set; }
        
        public string Password { get; set; }

        
        public string FullName { get; set; }

        
        public string Role { get; set; }
    }
}