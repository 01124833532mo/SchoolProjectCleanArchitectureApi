using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Subjects.Commands.Modles
{
    public class EditSubjectCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }

        public int Period { get; set; }
    }
}
