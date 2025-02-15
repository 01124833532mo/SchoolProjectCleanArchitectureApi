using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instractors.Command.Modles;
using SchoolProject.Core.Features.Instractors.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Instructors
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {

        [HttpGet(Router.InstructorRouting.GetSalarySummationOfInstructor)]

        public async Task<IActionResult> GetSalarySummationOfInstructor()
        {
            var result = await Mediator.Send(new GetSummationSalaryOfInstructorQuery());
            return Ok(result);
        }


        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstractorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
