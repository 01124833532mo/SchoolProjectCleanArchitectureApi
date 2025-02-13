using SchoolProject.Core.Features.Subjects.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subject
{
    public partial class SubjectProfile
    {
        void GetSubjectsListMapping()
        {
            CreateMap<Subjects, GetSubjectListResponse>();

        }
    }
}
