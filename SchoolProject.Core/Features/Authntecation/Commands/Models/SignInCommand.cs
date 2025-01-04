using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authntecation.Commands.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }

        public string Password { get; set; }


    }
}
