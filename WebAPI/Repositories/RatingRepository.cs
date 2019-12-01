using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Repositories
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        

        public RatingRepository(LocalDbContext appDbContext) :base(appDbContext)
        {  }

        public override void Update(int id, Rating rating)
        {
            var ratingToUpdate = AppDbContext.Ratings.Find(id);

            if (rating != null && ratingToUpdate != null)
            {
                ratingToUpdate.FitchRating = rating.FitchRating;
                ratingToUpdate.MoodysRating = rating.MoodysRating;
                ratingToUpdate.OrderNumber = rating.OrderNumber;
                ratingToUpdate.SandPRating = rating.SandPRating;

                AppDbContext.Ratings.Update(ratingToUpdate);
            }
        }

    }
}
