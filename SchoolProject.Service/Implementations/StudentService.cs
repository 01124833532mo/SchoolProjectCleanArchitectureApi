using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.InfrastructureBases.UnitOfWork;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _unitOfWork.GetRepository<Student>().GetTableNoTracking().Include(p => p.Department).Where(p => p.Id.Equals(id)).FirstOrDefault();

            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            //var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.name.Equals(student.Name)).FirstOrDefault();
            //if (studentResult is not null) return "Exist";

            //if (student.Id != null)   
            //    student.Id = null;


            if (student.DepartmentId == null)
                student.DepartmentId = null;

            await GetUnitOfWork().AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return "Success";



            //await _studentRepository.AddAsync(student);
            //return "Success";
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _unitOfWork.StudentRepository.GetStudentsListAsync();
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

        public async Task<string> EditAsync(Student student)
        {
            await GetUnitOfWork().UpdateAsync(student);
            await _unitOfWork.CompleteAsync();
            return "Success";


        }

        public async Task<string> DeleteAsync(Student student)
        {
            var uniteofwork = GetUnitOfWork();

            var trans = uniteofwork.BeginTransaction();
            try
            {
                await uniteofwork.DeleteAsync(student);
                var complete = await _unitOfWork.CompleteAsync() > 0;
                await trans.CommitAsync();
                if (complete)
                    return "Success";
                else
                    return "Faild";

            }
            catch
            {
                await trans.RollbackAsync();
                return "Faild";
            }

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await GetUnitOfWork().GetByIdAsync(id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuarable()
        {
            return GetUnitOfWork().GetTableNoTracking().Include(x => x.Department).AsQueryable();



        }

        public IQueryable<Student> FilterStudentsPaginatedQueryable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = GetUnitOfWork().GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.NameAr.Contains(search) || x.Address.Contains(search));

            }

            switch (orderingEnum)
            {
                case StudentOrderingEnum.Id:
                    querable = querable.OrderBy(querable => querable.Id);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(querable => querable.Address);
                    break;
                case StudentOrderingEnum.Name:
                    querable = querable.OrderBy(querable => querable.NameAr);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(querable => querable.Department.NameAr);
                    break;

                default:
                    querable = querable.OrderBy(x => x.Id);
                    break;
            }
            return querable;
        }

        public IQueryable<Student> GetStudentsByDepartmentIdQuarable(int Did)
        {
            return GetUnitOfWork().GetTableNoTracking().Where(x => x.DepartmentId.Equals(Did)).AsQueryable();


        }
        private IGenericRepository<Student> GetUnitOfWork()
        {
            return _unitOfWork.GetRepository<Student>();
        }
    }
}
