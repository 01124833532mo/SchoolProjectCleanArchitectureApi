using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authntecation.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Auth
{

    public class AuthenticationController : AppControllerBase
    {

        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> Create([FromBody] SignInCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
