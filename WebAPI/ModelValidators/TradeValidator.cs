using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class TradeValidator : ValidatorBase<EditTradeResource>
    {
        public TradeValidator()
        {
            RuleFor(x => x.Account).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Type).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(20);

            RuleFor(x => x.BuyQuantity).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(BuyQuantity => ValidNumber(BuyQuantity)).WithMessage(NotValidNumberMsg);

        }
    }
}
