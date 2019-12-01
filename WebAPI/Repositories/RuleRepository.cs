using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class RuleRepository : RepositoryBase<RuleName>, IRuleRepository
    {
        public RuleRepository(LocalDbContext appDbContext) : base(appDbContext)
        { }

        public override void Update(int id, RuleName rulename)
        {
            var ruleToUpdate = AppDbContext.RuleNames.Find(id);

            if (rulename != null && ruleToUpdate != null)
            {
                AppDbContext.RuleNames.Update(ruleToUpdate);
            }
        }
    }
}
