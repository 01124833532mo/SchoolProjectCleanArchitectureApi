using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Emails.Commands.Models;
using SchoolProject.Core.Resources;

namespace SchoolProject.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SendEmailValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplayValidationsRules();

        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Email Must not Be Null");

            RuleFor(e => e.Message).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                          .NotNull().WithMessage("Message Must not Be Null");
        }


    }
}
