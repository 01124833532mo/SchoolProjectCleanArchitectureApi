using FluentValidation;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Validations
{
    public class EditStudentValidator : AbstractValidator<EditStudentCommand>
    {
        private readonly IStudentService _studentService;



        public EditStudentValidator(IStudentService studentService)
        {
            ApplayValidationsRules();
            ApplayCustomValidationsRules();
            _studentService = studentService;
        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.NameAr).NotEmpty().WithMessage("Name Must not be Empty")
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.Address).NotEmpty().WithMessage("{PropertyName} Must not be Empty")
                              .NotNull().WithMessage("{PropertyValue} Must not Be Null")
                              .MaximumLength(100).WithMessage("{PropertyName} Length is 10");

        }
        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameArExistExcludeSelf(key, model.Id))
                .WithMessage("Name is Exist");

            RuleFor(x => x.NameEn)
             .MustAsync(async (model, key, CancellationToken) => !await _studentService.IsNameEnExistExcludeSelf(key, model.Id))
             .WithMessage("Name is Exist");
        }

    }
}
