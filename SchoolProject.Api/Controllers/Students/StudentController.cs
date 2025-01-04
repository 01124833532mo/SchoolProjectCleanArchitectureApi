using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Students
{
    [ApiController]
    [Authorize]
    public class StudentController : AppControllerBase
    {


        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentList()
        {
            var result = await Mediator.Send(new GetStudentListQuery());
            return Ok(result);
        }
        [AllowAnonymous]

        [HttpGet(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> GetStudentPaginatedList([FromQuery] GetStudentPaginatedListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }



        [HttpGet(Router.StudentRouting.GetById)]
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

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(result);
        }

    }
}
