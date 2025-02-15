﻿using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void EditStudentCommandMapping()
        {
            CreateMap<EditStudentCommand, Student>()
               .ForMember(d => d.DepartmentId, o => o.MapFrom(src => src.DepartmentId))
                .ForMember(d => d.NameAr, o => o.MapFrom(src => src.NameAr))
               .ForMember(d => d.NameEn, o => o.MapFrom(src => src.NameEn))
               .ForMember(d => d.Id, o => o.MapFrom(src => src.Id));

        }

    }
}
