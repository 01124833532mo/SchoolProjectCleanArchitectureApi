using AutoMapper;
using MediatR;
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
    internal class StudentHandler : IRequestHandler<GetStudentListQuery, List<GetStudentListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentHandler(IStudentService studentService,IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<List<GetStudentListResponse>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
          var StudentList= await  _studentService.GetStudentsAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
            return studentListMapper;

        }
    }
}
