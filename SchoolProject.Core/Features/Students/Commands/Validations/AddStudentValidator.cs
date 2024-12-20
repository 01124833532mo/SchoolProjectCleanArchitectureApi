using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.NameAr).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.Address).NotEmpty().WithMessage("{PropertyName} Must not be Empty")
                              .NotNull().WithMessage("{PropertyValue} Must not Be Null")
                              .MaximumLength(100).WithMessage("{PropertyName} Length is 10");

        }
        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                .WithMessage("Name is Exist");
        }
    }
}
