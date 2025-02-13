using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Subjects.Commands.Validators
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ISubjectService _subjectService;

        public CreateSubjectValidator(IStringLocalizer<SharedResources> localizer, ISubjectService subjectService)
        {
            _localizer = localizer;
            _subjectService = subjectService;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }

        public void ApplayValidationsRules()
        {
            RuleFor(e => e.NameAr).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.NameEn).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");



            RuleFor(e => e.Period).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                        .NotNull().WithMessage("Period Must Not Null , Must Be Requerd");

        }
        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _subjectService.IsNameArExist(key))
                .WithMessage("Name is Exist");
            RuleFor(x => x.NameEn)
               .MustAsync(async (key, CancellationToken) => !await _subjectService.IsNameEnExist(key))
               .WithMessage("Name is Exist");



        }
    }
}
