using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers.Departments
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {
        [HttpGet(Router.DepartmentRouting.GetById)]

        public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetDepartmentByIdQuery(id));

            return NewResult(result);

        }
    }
}
