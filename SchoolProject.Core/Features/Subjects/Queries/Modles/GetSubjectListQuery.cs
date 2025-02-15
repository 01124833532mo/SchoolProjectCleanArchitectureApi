using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Results;

namespace SchoolProject.Core.Features.Subjects.Queries.Modles
{
    public class GetSubjectListQuery : IRequest<Response<List<GetSubjectListResponse>>>
    {
    }
}
