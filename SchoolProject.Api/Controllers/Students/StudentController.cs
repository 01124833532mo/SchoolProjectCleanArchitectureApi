using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Students
{
    [ApiController]
    public class StudentController : AppControllerBase
    {
        

        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
           var result = await Mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }

        [HttpGet(Router.StudentRouting.GetById )]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(result);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
