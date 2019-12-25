using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.ApiResources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
    public class EditUserResource
    {
        [StringLength(20)]
        public string FullName { get; set; }
        [Required]
        [StringLength(15)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
        [Required]
        [StringLength(10)]
        public string UserName { get; set; }

        public string Role { get; set; }
    }


  

}
