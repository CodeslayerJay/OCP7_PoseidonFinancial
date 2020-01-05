using WebApi.ApiResources;
using WebApi.ModelValidators;

namespace WebApi.Services
{
    public interface IBidService
    {
        BidResource Add(EditBidResource resource);
        void Delete(int id);
        BidResource FindById(int id);
        BidResource[] GetAll();
        void Update(int id, EditBidResource resource);
        ValidationResult ValidateResource(EditBidResource resource);

    }
}