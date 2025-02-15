using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IDepartmentServices _departmentServices;
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer, IDepartmentServices departmentServices, IMapper mapper, IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _departmentServices = departmentServices;
            _mapper = mapper;
            _studentService = studentService;
        }
        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentServices.GetDepartmentById(request.Id);


            if (response == null) return NotFound<GetDepartmentByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var mappedresult = _mapper.Map<GetDepartmentByIdResponse>(response);

            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.Id, e.Localize(e.NameAr, e.NameEn));
            var studentQuarable = _studentService.GetStudentsByDepartmentIdQuarable(request.Id);
            var paginatedList = await studentQuarable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mappedresult.studentList = paginatedList;
            return Success(mappedresult);



        }
    }
}
