using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    internal class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
                                                        IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        //private readonly ResponseHandler responseHandler;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper/*,ResponseHandler responseHandler*/)
        {
            _studentService = studentService;
            _mapper = mapper;
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
            if (student == null) return NotFound<GetSingleStudentResponse>();

            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);


        }
    }
}
