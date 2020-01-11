using Dot.Net.WebApi.Data;
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
        public UserValidator(bool isUpdate)
        {
            RuleFor(x => x.FullName).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);

            RuleFor(x => x.Password).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(Password => IsPasswordStrong(Password))
                    .WithMessage("Password must contain letters, one number and symbol.")
                .MaximumLength(30);

            RuleFor(x => x.PasswordConfirm).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Equal(x => x.Password)
                .MaximumLength(30);

            RuleFor(x => x.UserName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(Username => IsUsernameUnique(Username, isUpdate))
                    .WithMessage("Username is already taken.")
                .MaximumLength(30);

            RuleFor(x => x.Role).Cascade(CascadeMode.StopOnFirstFailure)
                .MaximumLength(30);


        }

        protected virtual bool IsUsernameUnique(string username, bool isUpdate)
        {
            var valid = false;

            if (!String.IsNullOrEmpty(username))
            {
                using(var context = new LocalDbContext())
                {
                    var user = context.Users.Where(x => x.UserName == username).FirstOrDefault();

                    if (user == null)
                        return true;

                    return isUpdate ? true : username != user.UserName;

                }
            }

            return valid;
        }

        protected virtual bool IsPasswordStrong(string password)
        {
            if (password.Length < 6)
                return false;

            var letters = 0;
            var digits = 0;
            var symbols = 0;
            var invalidChars = 0;
            foreach (var ch in password)
            {
                if (char.IsWhiteSpace(ch))
                    return false;

                if (char.IsLetter(ch)) letters++;
                if (char.IsDigit(ch)) digits++;
                if (char.IsSymbol(ch)) symbols++;
                if (char.IsPunctuation(ch)) symbols++;
            }

            if (letters == 0) return false;
            if (digits == 0) return false;
            if (symbols == 0) return false;
            if (invalidChars > 0) return false;

            return true;
        }
    }
}
