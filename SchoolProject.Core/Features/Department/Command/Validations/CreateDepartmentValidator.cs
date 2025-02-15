using FluentValidation;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Features.Department.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Department.Command.Validations
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IDepartmentServices _departmentServices;
        private readonly IInstractorService _instractorService;

        public CreateDepartmentValidator(IStringLocalizer<SharedResources> localizer, IDepartmentServices departmentServices, IInstractorService instractorService)
        {
            _localizer = localizer;
            _departmentServices = departmentServices;
            _instractorService = instractorService;
            ApplayValidationsRules();
            ApplayCustomValidationsRules();

        }



        public void ApplayValidationsRules()
        {
            RuleFor(e => e.NameAr).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                               .NotNull().WithMessage("Name Must not Be Null")
                               .MaximumLength(100).WithMessage("Max Length is 10");

            RuleFor(e => e.NameAr).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                             .NotNull().WithMessage("Name Must not Be Null")
                             .MaximumLength(100).WithMessage("Max Length is 10");



            RuleFor(e => e.InsManger).NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                                        .NotNull().WithMessage("Instractor Must Not Null , Must Be Requerd");

        }
        public void ApplayCustomValidationsRules()
        {
            RuleFor(x => x.NameAr)
                .MustAsync(async (key, CancellationToken) => !await _departmentServices.IsNameArExist(key))
                .WithMessage("Name is Exist");
            RuleFor(x => x.NameEn)
               .MustAsync(async (key, CancellationToken) => !await _departmentServices.IsNameEnExist(key))
               .WithMessage("Name is Exist");


            RuleFor(x => x.InsManger)
             .MustAsync(async (key, CancellationToken) => await _instractorService.IsInstractorIdExist(key))
             .WithMessage(_localizer[SharedResourcesKeys.InstractorIdNotExist]);
        }
    }
}
