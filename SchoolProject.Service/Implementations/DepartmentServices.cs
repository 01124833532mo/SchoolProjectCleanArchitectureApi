using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentServices(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetTableNoTracking().Where(e => e.Id.Equals(id))
                    .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                    .Include(x => x.Instructors)
                    .Include(x => x.Instructor).FirstOrDefaultAsync();

            return department;
        }

        public async Task<bool> IsDepartmentIdExist(int departmentId)
        {

            return await _departmentRepository.GetTableNoTracking().AnyAsync(e => e.Id.Equals(departmentId));
        }
    }
}
