using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public AddUserValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;

            ApplayValidationsRules();
            ApplayCustomValidationsRules();
        }

        public void ApplayValidationsRules()
        {
            RuleFor(e => e.FullName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(e => e.UserName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage("User name Must not Be Null")
                   .MaximumLength(100).WithMessage("Max Length is 100");


            RuleFor(e => e.Email).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage("Email Must not Be Null")
                   .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(e => e.Password).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("Password Must not Be Null")
         .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.ConfirmPassword).Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPasssword]);



        }
        public void ApplayCustomValidationsRules()
        {

        }

    }
}
