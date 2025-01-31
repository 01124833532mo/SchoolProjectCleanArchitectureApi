using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Auth
{
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddRoleCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
    }
}
