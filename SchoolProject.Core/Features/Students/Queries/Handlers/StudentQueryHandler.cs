using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    internal class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                                        IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
                                                         IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        //private readonly ResponseHandler responseHandler;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper/*,ResponseHandler responseHandler*/, IStringLocalizer<SharedResources> StringLocalizer) : base(StringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = StringLocalizer;
            //this.responseHandler = responseHandler;
        }
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var StudentList = await _studentService.GetStudentsAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            return Success(studentListMapper);

        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null) return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);


        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.Id, e.NameAr, e.Address, e.Department.NameAr);
            //var querable = _studentService.GetStudentsQueryable();
            //var querable = _studentService.GetStudentsQuarable();
            var filterQuery = _studentService.FilterStudentsPaginatedQueryable(request.OrderBy, request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);


            paginatedList.Meta = new { Count = paginatedList.Data.Count() };
            return paginatedList;
        }
    }
}
