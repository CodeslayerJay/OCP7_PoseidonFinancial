using Dot.Net.WebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.AppUtilities;

namespace WebApi.Repositories
{
    public class AccessTokenRepository
    {
        private LocalDbContext _dbContext;

        public AccessTokenRepository(LocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void StoreAccessToken(JsonWebToken token)
        {
            if(token != null)
            {
                
            }
        }
    }
}
