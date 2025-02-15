using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Instractors.Command.Modles;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Instractors.Command.Validators
{
    public class AddInstractorValidator : AbstractValidator<AddInstractorCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentServices _departmentServices;
        private readonly IInstractorService _instractorService;

        public AddInstractorValidator(
            IStringLocalizer<SharedResources> localizer,
            IDepartmentServices departmentServices,
            IInstractorService instractorService)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _departmentServices = departmentServices ?? throw new ArgumentNullException(nameof(departmentServices));
            _instractorService = instractorService ?? throw new ArgumentNullException(nameof(instractorService));

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(e => e.NameAr)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Name Must not Be Null")
                .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(e => e.NameEn)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Name Must not Be Null")
                .MaximumLength(100).WithMessage("Max Length is 100");

            RuleFor(e => e.Address)
                .NotEmpty().WithMessage("{PropertyName} Must not be Empty")
                .NotNull().WithMessage("{PropertyValue} Must not Be Null")
                .MaximumLength(100).WithMessage("{PropertyName} Length is 100");

            RuleFor(e => e.DepartmentId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage("Department Must Not Be Null, Must Be Required");
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, cancellationToken) => !await _instractorService.IsNameArExist(key))
                .WithMessage("NameAr is Exist");

            RuleFor(x => x.NameEn)
                .MustAsync(async (key, cancellationToken) => !await _instractorService.IsNameEnExist(key))
                .WithMessage("NameEn is Exist");

            RuleFor(x => x.DepartmentId)
                .MustAsync(async (key, cancellationToken) => await _departmentServices.IsDepartmentIdExist(key))
                .WithMessage(_localizer[SharedResourcesKeys.DepartmentIdIsNotExist]);
        }
    }
}