using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class RuleNameValidator : ValidatorBase<EditRuleNameResource>
    {
        public RuleNameValidator()
        {
            RuleFor(x => x.Description).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);

            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);

            RuleFor(x => x.Template).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);
        }
    }
}
