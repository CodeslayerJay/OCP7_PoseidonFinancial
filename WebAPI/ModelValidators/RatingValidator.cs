using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class RatingValidator : ValidatorBase<EditRatingResource>
    {
        public RatingValidator()
        {
            RuleFor(x => x.FitchRating).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(20);

            RuleFor(x => x.MoodysRating).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(20);

            RuleFor(x => x.SandPRating).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(20);
        }
    }
}
