using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IAuthorizationService _authorizationService;

        public AddRoleValidator(IStringLocalizer<SharedResources> localizer, IAuthorizationService authorizationService)
        {
            _localizer = localizer;
            _authorizationService = authorizationService;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }

        public void ApplayValidationsRules()
        {
            RuleFor(e => e.RoleName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");



        }
        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (key, CancellationToken) => !await _authorizationService.IsRoleExist(key))
                .WithMessage("Name is Exist");

        }

    }
}
