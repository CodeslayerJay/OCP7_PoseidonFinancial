using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface IRatingService
    {
        RatingResource Add(RatingResource resource);
        void Delete(int id);
        RatingResource FindById(int id);
        RatingResource[] GetAll();
        void Update(int id, RatingResource resource);
    }
}