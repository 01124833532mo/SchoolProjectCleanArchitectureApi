using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authntecation.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<Response<string>>
    {

        public int UserId { get; set; }

        public string code { get; set; }


    }
}
