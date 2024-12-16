using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();

        public Task<Student> GetStudentByIdAsync(int id);

        public Task<string> AddAsync(Student student);

        public Task<bool> IsNameExist(string name);

    }
}
