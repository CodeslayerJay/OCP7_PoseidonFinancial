using System;
using System.Collections.Generic;
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


    public class CreateUserResource
    {
        public string FullName { get; set; }
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
        public string UserName { get; set; }
    }


    public class EditUserResource
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public string PasswordConfirm { get; set; }
        public string UserName { get; set; }
    }

}
