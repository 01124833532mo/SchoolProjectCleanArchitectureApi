using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authntecation.Queries.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Authntecation.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ConfirmEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.UserId).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("userid Must not Be Null");

            RuleFor(e => e.code).NotEmpty().WithMessage("code Must not be Empty")
                              .NotNull().WithMessage("code Must not Be Null");


        }
        public void ApplayCustomValidationsRules()
        {

        }

    }
}
