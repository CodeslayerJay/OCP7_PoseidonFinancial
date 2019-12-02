using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface IBidService
    {
        BidResource Add(EditBidResource resource);
        void Delete(int id);
        BidResource FindById(int id);
        BidResource[] GetAll();
        void Update(int id, EditBidResource resource);
    }
}