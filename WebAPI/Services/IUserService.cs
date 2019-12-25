
using WebApi.ApiResources;
using WebApi.AppUtilities;

namespace WebApi.Services
{
    public interface IUserService
    {
        UserResource CreateUser(EditUserResource resource);
        UserResource FindByUsername(string username);

        UserResource GetUserById(int id);
        void UpdateUser(int id, EditUserResource resource);
        void DeleteUser(int id);

        bool ValidateUser(string username, string password);
        void StoreAccessToken(JsonWebToken token, int userId);
        UserResource GetUserForAccessToken(JsonWebToken token);
        UserResource GetUserForAccessToken(string token);
    }
}