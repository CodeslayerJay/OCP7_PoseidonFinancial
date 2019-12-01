using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUserName(string username);
    }
}