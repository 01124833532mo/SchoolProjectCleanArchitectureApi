using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Authntecation.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Authntecation.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentServices _departmentServices;

        public SignInValidator(IStudentService studentService, IStringLocalizer<SharedResources> localizer, IDepartmentServices departmentServices)
        {
            _studentService = studentService;
            _localizer = localizer;
            _departmentServices = departmentServices;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.UserName).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("UserName Must not Be Null");

            RuleFor(e => e.Password).NotEmpty().WithMessage("Password Must not be Empty")
                              .NotNull().WithMessage("Password Must not Be Null");


        }
        public void ApplayCustomValidationsRules()
        {

        }
    }
}
