using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class BidValidator : ValidatorBase<EditBidResource>
    {
        public BidValidator()
        {
            RuleFor(x => x.Account).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.BidQuantity).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BidQuantity => ValidNumber(BidQuantity)).WithMessage(NotValidNumberMsg);

            RuleFor(x => x.Type).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
