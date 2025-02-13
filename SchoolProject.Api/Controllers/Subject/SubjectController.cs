using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Core.Features.Subjects.Queries.Modles;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Subject
{
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class SubjectController : AppControllerBase
    {
        [HttpPost(Router.SubjectRouting.Create)]
        public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut(Router.SubjectRouting.Edit)]
        public async Task<IActionResult> EditStudent([FromBody] EditSubjectCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpGet(Router.SubjectRouting.List)]
        public async Task<IActionResult> GetSubjectList()
        {
            var result = await Mediator.Send(new GetSubjectListQuery());
            return Ok(result);
        }
    }
}
