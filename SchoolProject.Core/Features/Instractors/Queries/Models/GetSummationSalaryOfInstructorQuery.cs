using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instractors.Queries.Models
{
    public class GetSummationSalaryOfInstructorQuery : IRequest<Response<decimal>>
    {

    }
}
