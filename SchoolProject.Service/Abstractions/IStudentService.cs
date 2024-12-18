using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();

        public Task<Student> GetStudentByIdWithIncludeAsync(int id);
        public Task<Student> GetByIdAsync(int id);


        public Task<string> AddAsync(Student student);

        public Task<bool> IsNameExist(string name);
        public Task<bool> IsNameExistExcludeSelf(string name, int id);

        public Task<string> EditAsync(Student student);

        public Task<string> DeleteAsync(Student student);

        public IQueryable<Student> GetStudentsQuarable();
        public IQueryable<Student> FilterStudentsPaginatedQueryable(string search);



    }
}
