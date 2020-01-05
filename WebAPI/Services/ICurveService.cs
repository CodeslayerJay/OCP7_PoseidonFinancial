using WebApi.ApiResources;
using WebApi.ModelValidators;

namespace WebApi.Services
{
    public interface ICurveService
    {
        CurveResource Add(EditCurveResource resource);
        void Delete(int id);
        CurveResource FindById(int id);
        CurveResource[] GetAll();
        void Update(int id, EditCurveResource resource);
        ValidationResult ValidateResource(EditCurveResource resource);
    }
}