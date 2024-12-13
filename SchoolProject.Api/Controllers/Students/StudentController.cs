using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Queries.Models;

namespace SchoolProject.Api.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudentList()
        {
           var result = await _mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }

        [HttpGet("GetStudent/{id}")]
        public async Task<IActionResult> GetStudent(/*[FromRoute]*/ int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(result);
        }

    }
}
