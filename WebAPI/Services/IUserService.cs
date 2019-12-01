using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface IUserService
    {
        UserResource CreateUser(CreateUserResource createUserResource);
        UserResource FindByUsername(string username);
    }
}