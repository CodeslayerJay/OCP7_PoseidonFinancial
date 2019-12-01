using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class CurvePointRepository : RepositoryBase<CurvePoint>, ICurvePointRepository
    {
       
        public CurvePointRepository(LocalDbContext appDbContext) :base(appDbContext)
        { }

        public override void Update(int id, CurvePoint curvePoint)
        {
            var cpToUpdate = AppDbContext.CurvePoints.Find(id);

            if (curvePoint != null && cpToUpdate != null)
            {
                //cpToUpdate.Term = curvePoint.Term;
                //cpToUpdate.Value = cpToUpdate.Value;
                //cpToUpdate.AsOfDate = curvePoint.AsOfDate;

                AppDbContext.CurvePoints.Update(cpToUpdate);
            }
        }


    }
}

