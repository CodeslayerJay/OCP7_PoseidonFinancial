﻿using WebApi.ApiResources;
using WebApi.ModelValidators;

namespace WebApi.Services
{
    public interface IRuleService
    {
        RuleNameResource Add(EditRuleNameResource resource);
        void Delete(int id);
        RuleNameResource FindById(int id);
        RuleNameResource[] GetAll();
        void Update(int id, EditRuleNameResource resource);
        ValidationResult ValidateResource(EditRuleNameResource resource);
    }
}