using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authntecation.Queries.Models
{
    public class AuthorizeUserQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; }

    }
}
