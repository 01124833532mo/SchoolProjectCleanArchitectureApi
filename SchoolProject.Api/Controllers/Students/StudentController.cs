using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Students
{
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
           var result = await _mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }

        [HttpGet(Router.StudentRouting.GetById )]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(result);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
