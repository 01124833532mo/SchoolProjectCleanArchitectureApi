using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Auth
{
    [ApiController]
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
