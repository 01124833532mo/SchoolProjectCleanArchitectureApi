using AutoMapper;

namespace SchoolProject.Core.Mapping.Subject
{
    public partial class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateSubjectCommandMapping();
            EditSubjectCommandMapping();
            GetSubjectsListMapping();
        }
    }
}
