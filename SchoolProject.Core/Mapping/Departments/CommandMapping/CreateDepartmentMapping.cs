using SchoolProject.Core.Features.Department.Command.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        void CreateDepartmentMapping()
        {
            CreateMap<CreateDepartmentCommand, Department>();

        }
    }
}
