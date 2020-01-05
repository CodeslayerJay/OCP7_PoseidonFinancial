using WebApi.ApiResources;
using WebApi.ModelValidators;

namespace WebApi.Services
{
    public interface IRatingService
    {
        RatingResource Add(EditRatingResource resource);
        void Delete(int id);
        RatingResource FindById(int id);
        RatingResource[] GetAll();
        void Update(int id, EditRatingResource resource);
        ValidationResult ValidateResource(EditRatingResource resource);
    }
}