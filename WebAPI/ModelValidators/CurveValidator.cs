using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class CurveValidator : ValidatorBase<EditCurveResource>
    {
        public CurveValidator()
        {
            RuleFor(x => x.CurveId).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(CurveId => ValidNumber(CurveId)).WithMessage(NotValidNumberMsg);

            RuleFor(x => x.Term).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(Term => ValidNumber(Term)).WithMessage(NotValidNumberMsg);

            RuleFor(x => x.Value).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(Value => ValidNumber(Value)).WithMessage(NotValidNumberMsg);
        }
    }
}
