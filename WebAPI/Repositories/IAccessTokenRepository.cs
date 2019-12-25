using WebApi.AppUtilities;
using WebApi.Data;

namespace WebApi.Repositories
{
    public interface IAccessTokenRepository
    {
        void StoreAccessToken(JsonWebToken token, int userId);
        AccessToken GetAccessToken(JsonWebToken token);
        AccessToken GetAccessToken(string token);
        void DeleteToken(int userId);
        void DeleteToken(AccessToken token);
    }
}