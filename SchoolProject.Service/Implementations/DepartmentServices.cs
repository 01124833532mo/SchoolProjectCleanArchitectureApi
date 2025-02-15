using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentServices(IDepartmentRepository departmentRepository, IUnitOfWork unitOfWork)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            //var department = await _departmentRepository.GetTableNoTracking().Where(e => e.Id.Equals(id))
            //        .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
            //        .Include(x => x.Instructors)
            //        .Include(x => x.Instructor).FirstOrDefaultAsync();

            var department = await _unitOfWork.GetRepository<Department>().GetTableNoTracking().Where(e => e.Id.Equals(id))
                    .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
                    .Include(x => x.Instructors)
                    .Include(x => x.Instructor).FirstOrDefaultAsync();

            return department;
        }

        public async Task<bool> IsDepartmentIdExist(int departmentId)
        {

            //return await _departmentRepository.GetTableNoTracking().AnyAsync(e => e.Id.Equals(departmentId));

            return await _unitOfWork.GetRepository<Department>().GetTableNoTracking().AnyAsync(e => e.Id.Equals(departmentId));
        }
        public async Task<bool> IsNameArExist(string name)
        {
            var student = GetUnitOfWork().GetTableNoTracking().Where(x => x.NameAr.Equals(name)).FirstOrDefault();

            if (student == null) return false;
            return true;

        }

        public async Task<bool> IsNameEnExist(string name)
        {
            var student = GetUnitOfWork().GetTableNoTracking().Where(x => x.NameEn.Equals(name)).FirstOrDefault();

            if (student == null) return false;
            return true;

        }



        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            var student = await GetUnitOfWork().GetTableNoTracking().Where(x => x.NameAr.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            var student = await GetUnitOfWork().GetTableNoTracking().Where(x => x.NameEn.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }
        private IGenericRepository<Department> GetUnitOfWork()
        {
            return _unitOfWork.GetRepository<Department>();
        }

        public async Task<string> AddAsync(Department subjects)
        {
            await GetUnitOfWork().AddAsync(subjects);
            await _unitOfWork.CompleteAsync();
            return "Success";
        }
    }
}
