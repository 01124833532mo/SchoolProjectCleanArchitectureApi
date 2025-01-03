using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
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


        [HttpGet(Router.ApplicationUserRouting.Paginated)]
        public async Task<IActionResult> GetStudentPaginatedList([FromQuery] GetUserPaginationQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        //[HttpGet(Router.ApplicationUserRouting.GetById)]
        [HttpGet("/Api/V1/User/Paginated/{id}")]
        public async Task<IActionResult> GetUserById(/*[FromQuery]*/ int id)
        {
            var result = await Mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPut(Router.ApplicationUserRouting.Edit)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }


    }
}
