using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Department.Command.Models
{
    public class CreateDepartmentCommand : IRequest<Response<string>>
    {
        public string? NameAr { get; set; }

        public string? NameEn { get; set; }

        public int? InsManger { get; set; }
    }
}
