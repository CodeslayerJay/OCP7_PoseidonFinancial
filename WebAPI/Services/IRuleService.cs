using WebApi.ApiResources;

namespace WebApi.Services
{
    public interface IRuleService
    {
        RuleNameResource Add(EditRuleNameResource resource);
        void Delete(int id);
        RuleNameResource FindById(int id);
        RuleNameResource[] GetAll();
        void Update(int id, EditRuleNameResource resource);
    }
}