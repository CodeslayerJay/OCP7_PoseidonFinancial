using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppUtilities;
using WebApi.Data;

namespace WebApi.Repositories
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        private LocalDbContext _dbContext;

        public AccessTokenRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void StoreAccessToken(JsonWebToken token, int userId)
        {
            if (token != null || userId > 0)
            {
                _dbContext.AccessTokens.Add(new AccessToken { Token = token.Token, UserId = userId, ExpiresAt = token.ExpiresAt });
                _dbContext.SaveChanges();
            }
        }

        public AccessToken GetAccessToken(int userId)
        {
            return _dbContext.AccessTokens.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public AccessToken GetAccessToken(JsonWebToken token)
        {
            return GetAccessToken(token.Token);
        }

        public AccessToken GetAccessToken(string token)
        {
            return _dbContext.AccessTokens.Where(x => x.Token == token).FirstOrDefault();
        }

        public void DeleteToken(int userId)
        {
            if(userId > 0)
            {
                var token = _dbContext.AccessTokens.Where(x => x.UserId == userId).FirstOrDefault();
                DeleteToken(token);
            }
        }

        public void DeleteToken(AccessToken token)
        {   
            if (token != null)
            {
                _dbContext.AccessTokens.Remove(token);
                _dbContext.SaveChanges();
            }
            
        }
    }
}
