using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface IRatingService
    {
        RatingResource Add(CreateRatingResource resource);
        void Delete(int id);
        RatingResource FindById(int id);
        RatingResource[] GetAll();
        void Update(int id, EditRatingResource resource);
    }
}