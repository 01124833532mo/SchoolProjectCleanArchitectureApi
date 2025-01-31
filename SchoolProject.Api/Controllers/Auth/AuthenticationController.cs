using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authntecation.Commands.Models;
using SchoolProject.Core.Features.Authntecation.Queries.Models;
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

        [HttpPost(Router.Authentication.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreskTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet(Router.Authentication.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromBody] AuthorizeUserQuery command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
