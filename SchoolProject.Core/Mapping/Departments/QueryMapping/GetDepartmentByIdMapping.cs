using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {

        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                  .ForMember(d => d.Name, o => o.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                  .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                  .ForMember(d => d.ManagerName, o => o.MapFrom(src => src.Instructor.Localize(src.Instructor.NameAr, src.Instructor.NameEn)))
                  .ForMember(d => d.subjectList, o => o.MapFrom(src => src.DepartmentSubjects))
                  //.ForMember(d => d.studentList, o => o.MapFrom(src => src.Students))
                  .ForMember(d => d.InstractorList, o => o.MapFrom(src => src.Instructors));

            CreateMap<DepartmentSubject, SubjectResponse>()
               .ForMember(d => d.Id, o => o.MapFrom(src => src.SubjectId))
               .ForMember(d => d.Name, o => o.MapFrom(src => src.Subject.Localize(src.Subject.NameAr, src.Subject.NameEn)));

            //CreateMap<Student, StudentResponse>()
            //  .ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
            //  .ForMember(d => d.Name, o => o.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstractorsResponse>()
              .ForMember(d => d.Id, o => o.MapFrom(src => src.InstructorId))
              .ForMember(d => d.Name, o => o.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

        }

    }
}
