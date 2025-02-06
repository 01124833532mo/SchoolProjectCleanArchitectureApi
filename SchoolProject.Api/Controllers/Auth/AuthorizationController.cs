using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Auth
{
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> Create([FromBody] AddRoleCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int userid)
        {
            var result = await Mediator.Send(new ManageUserRoleQuery() { UserId = userid });
            return NewResult(result);
        }

        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userid)
        {
            var result = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userid });
            return NewResult(result);
        }
    }


}
