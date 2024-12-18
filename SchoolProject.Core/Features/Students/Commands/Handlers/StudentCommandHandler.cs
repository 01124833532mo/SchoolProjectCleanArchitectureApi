using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :
        ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // Mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);

            // Add
            var result = await _studentService.AddAsync(studentMapper);

            if (result == "Success") return Created("Added Successfully");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if the Id is Exist Or not
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            // return Not Found
            if (student == null) return NotFound<string>("Student is Not Found");

            // mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);

            // Call service that make Edit
            var result = await _studentService.EditAsync(studentMapper);

            // return response
            if (result == "Success") return Success($"Edit Successfully ({studentMapper.Id})");
            else return BadRequest<string>();

        }
    }
}
