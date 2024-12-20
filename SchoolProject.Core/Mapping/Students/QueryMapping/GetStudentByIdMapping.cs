using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetSingleStudentResponse>()
               .ForMember(d => d.DepartmentName, o => o.MapFrom(src => src.Department.NameAr))
               .ForMember(d => d.Name, o => o.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }

    }
}
