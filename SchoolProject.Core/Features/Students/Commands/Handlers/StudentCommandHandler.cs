using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler :
        ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;


        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // Mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);

            // Add
            var result = await _studentService.AddAsync(studentMapper);

            if (result == "Success") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if the Id is Exist Or not
            var student = await _studentService.GetByIdAsync(request.Id);

            // return Not Found
            if (student == null) return NotFound<string>("Student is Not Found");

            // mapping Between request and student
            var studentMapper = _mapper.Map(request, student);

            // Call service that make Edit
            var result = await _studentService.EditAsync(studentMapper);

            // return response
            if (result == "Success") return Success($"Edit Successfully ({studentMapper.Id})");
            else return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // Check if the Id is Exist Or not
            var student = await _studentService.GetByIdAsync(request.Id);

            // return Not Found
            if (student == null) return NotFound<string>("Student is Not Found");

            // Call service that make Delete
            var result = await _studentService.DeleteAsync(student);

            // return response
            if (result == "Success") return Deleted<string>($"Deleted Sussessfully ({request.Id})");
            else return BadRequest<string>();


        }
    }
}
