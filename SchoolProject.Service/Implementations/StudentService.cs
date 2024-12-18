using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
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
    }
}
