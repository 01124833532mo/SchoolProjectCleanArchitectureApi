using SchoolProject.Core.Features.Subjects.Commands.Modles;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subject
{
    public partial class SubjectProfile
    {
        void EditSubjectCommandMapping()
        {
            CreateMap<EditSubjectCommand, Subjects>();

        }

    }
}
