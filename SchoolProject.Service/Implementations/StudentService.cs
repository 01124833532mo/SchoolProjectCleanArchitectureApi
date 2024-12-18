using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking().Include(p => p.Department).Where(p => p.Id.Equals(id)).FirstOrDefault();

            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            var studentResult = _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(student.Name)).FirstOrDefault();
            if (studentResult is not null) return "Exist";

            //if (student.Id != null)
            //    student.Id = null;

            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }

        public async Task<bool> IsNameExist(string name)
        {
            var student = _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(name)).FirstOrDefault();

            if (student == null) return false;
            return true;

        }

        public async Task<bool> IsNameExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().Where(x => x.Name.Equals(name) & !x.Id.Equals(id)).FirstOrDefaultAsync();

            if (student == null) return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";


        }

        public async Task<string> DeleteAsync(Student student)
        {

            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Faild";
            }

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return student;
        }

        public IQueryable<Student> GetStudentsQuarable()
        {
            return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();



        }

        public IQueryable<Student> FilterStudentsPaginatedQueryable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Name.Contains(search) || x.Address.Contains(search));

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
                    querable = querable.OrderBy(querable => querable.Name);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(querable => querable.Department.Name);
                    break;

                default:
                    querable = querable.OrderBy(x => x.Id);
                    break;
            }
            return querable;
        }
    }
}
