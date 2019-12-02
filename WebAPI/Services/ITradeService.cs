using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface ITradeService
    {
        TradeResource Add(EditTradeResource resource);
        void Delete(int id);
        TradeResource FindById(int id);
        TradeResource[] GetAll();
        void Update(int id, EditTradeResource resource);
    }
}