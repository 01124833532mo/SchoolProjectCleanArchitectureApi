using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Auth
{
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {


        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Register([FromBody] AddUserCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
