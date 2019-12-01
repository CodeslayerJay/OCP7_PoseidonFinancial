using Dot.Net.WebApi.Data;
using System.Linq;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.ObjectModel;

namespace WebApi.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LocalDbContext appDbContext) : base(appDbContext)
        { }

        public User FindByUserName(string username)
        {
            return AppDbContext.Users.Where(user => user.UserName == username)
                                  .FirstOrDefault();
        }

        public override void Update(int id, User user)
        {
            var userToUpdate = AppDbContext.Users.Find(id);
            if (userToUpdate != null && user != null)
            {
                userToUpdate.FullName = user.FullName;
                userToUpdate.Password = user.Password;
                userToUpdate.Role = user.Role;
                userToUpdate.UserName = user.UserName;
            }
        }
    }
}