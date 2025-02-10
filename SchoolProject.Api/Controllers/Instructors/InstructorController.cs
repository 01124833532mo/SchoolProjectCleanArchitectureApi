using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instractors.Command.Modles;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Instructors
{
    [ApiController]
    public class InstructorController : AppControllerBase
    {
        [HttpPost(Router.InstructorRouting.AddInstructor)]
        public async Task<IActionResult> AddInstructor([FromForm] AddInstractorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
