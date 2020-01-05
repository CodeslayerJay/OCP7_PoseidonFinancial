using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;

namespace WebApi.ModelValidators
{
    public class UserValidator :ValidatorBase<EditUserResource>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);

            RuleFor(x => x.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.PasswordConfirm).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Matches("Password")
                .MaximumLength(30);

            RuleFor(x => x.UserName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(x => x.Role).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);
        }
    }
}
