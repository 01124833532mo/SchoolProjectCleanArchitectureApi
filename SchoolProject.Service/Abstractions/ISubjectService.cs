using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface ISubjectService
    {
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);

        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);

        public Task<string> AddAsync(Subjects subjects);
        public Task<string> EditAsync(Subjects student);
        public Task<Subjects> GetByIdAsync(int id);
        public Task<List<Subjects>> GetSubjectsAsync();


    }
}
