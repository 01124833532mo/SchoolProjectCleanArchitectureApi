using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subject
{
    public partial class SubjectProfile
    {
        public void CreateSubjectCommandMapping()
        {
            CreateMap<CreateSubjectCommand, Subjects>();

        }

    }
}
