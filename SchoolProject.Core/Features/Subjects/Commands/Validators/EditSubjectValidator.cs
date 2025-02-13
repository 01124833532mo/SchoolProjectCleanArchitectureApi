using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Subjects.Commands.Validators
{
    public class EditSubjectValidator : AbstractValidator<EditSubjectCommand>
    {
        private readonly ISubjectService _subjectService;
        private readonly IStringLocalizer<SharedResources> _localizer;




        public EditSubjectValidator(ISubjectService subjectService, IStringLocalizer<SharedResources> stringLocalizer)
        {
            _localizer = stringLocalizer ?? throw new ArgumentNullException(nameof(stringLocalizer));
            _subjectService = subjectService ?? throw new ArgumentNullException(nameof(subjectService));
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
                .MustAsync(async (model, key, CancellationToken) => !await _subjectService.IsNameArExistExcludeSelf(key, model.Id))
                .WithMessage("Name is Exist");

            RuleFor(x => x.NameEn)
             .MustAsync(async (model, key, CancellationToken) => !await _subjectService.IsNameEnExistExcludeSelf(key, model.Id))
             .WithMessage("Name is Exist");
        }
    }
}
