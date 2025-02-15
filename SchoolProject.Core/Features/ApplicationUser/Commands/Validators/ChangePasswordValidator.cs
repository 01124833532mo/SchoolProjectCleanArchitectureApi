using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    internal class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {

        private readonly IStringLocalizer<SharedResources> _localizer;

        public ChangePasswordValidator(IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer;

            ApplayValidationsRules();
            ApplayCustomValidationsRules();
        }

        public void ApplayValidationsRules()
        {


            RuleFor(e => e.Id).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage("User id Must not Be Null");


            RuleFor(e => e.CurrentPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                   .NotNull().WithMessage("CurrentPassword Must not Be Null")
                   .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(e => e.NewPassword).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
         .NotNull().WithMessage("NewPassword Must not Be Null")
         .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.ConfirmPassword).Equal(x => x.NewPassword).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPasssword]);



        }
        public void ApplayCustomValidationsRules()
        {

        }
    }
}
