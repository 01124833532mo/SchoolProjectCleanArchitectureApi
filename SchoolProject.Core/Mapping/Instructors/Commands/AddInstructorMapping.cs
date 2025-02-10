using SchoolProject.Core.Features.Instractors.Command.Modles;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
    public partial class InstructorProfile
    {
        public void AddInstructorMapping()
        {
            CreateMap<AddInstractorCommand, Instructor>()
                 .ForMember(dest => dest.Image, opt => opt.Ignore())
                 .ForMember(dest => dest.NameAr, opt => opt.MapFrom(src => src.NameAr))
                 .ForMember(dest => dest.NameEn, opt => opt.MapFrom(src => src.NameEn));
        }
    }
}
