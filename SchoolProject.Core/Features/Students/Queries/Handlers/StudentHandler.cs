using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    internal class StudentHandler :ResponseHandler , IRequestHandler<GetStudentListQuery,Response <List<GetStudentListResponse>>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        //private readonly ResponseHandler responseHandler;

        public StudentHandler(IStudentService studentService,IMapper mapper/*,ResponseHandler responseHandler*/)
        {
            _studentService = studentService;
            _mapper = mapper;
            //this.responseHandler = responseHandler;
        }
        public async Task<Response< List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
          var StudentList= await  _studentService.GetStudentsAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            return Success(studentListMapper);

        }
    }
}
