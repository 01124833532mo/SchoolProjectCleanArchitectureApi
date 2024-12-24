using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentsListPaginationMapping()
        {
            CreateMap<Student, GetStudentPaginatedListResponse>()
               .ForMember(d => d.DepartmentName, o => o.MapFrom(src => src.Department.NameAr))
               .ForMember(d => d.Name, o => o.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
               .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
               .ForMember(d => d.Address, o => o.MapFrom(src => src.Address));

        }
    }
}
