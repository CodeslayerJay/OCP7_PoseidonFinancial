using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface ICurveService
    {
        CurveResource Add(CurveResource resource);
        void Delete(int id);
        CurveResource FindById(int id);
        CurveResource[] GetAll();
        void Update(int id, CurveResource resource);
    }
}