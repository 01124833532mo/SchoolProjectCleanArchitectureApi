using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Command.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstractions;


namespace SchoolProject.Core.Features.Department.Command.Handlers
{
    public class DepartmentCommandHandler : ResponseHandler, IRequestHandler<CreateDepartmentCommand, Response<string>>
    {
        private readonly IDepartmentServices _departmentServices;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public DepartmentCommandHandler(IDepartmentServices subjectService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _departmentServices = subjectService;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var departmentMapper = _mapper.Map<SchoolProject.Data.Entities.Department>(request); // Correct usage

            // Add
            var result = await _departmentServices.AddAsync(departmentMapper);

            if (result == "Success")
                return Created("");

            return BadRequest<string>();
        }
    }
}
