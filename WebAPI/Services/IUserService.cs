
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using WebApi.ApiResources;

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
    }
}