using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IDepartmentServices
    {
        public Task<Department> GetDepartmentById(int id);

        public Task<bool> IsDepartmentIdExist(int departmentId);
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);

        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);
        public Task<string> AddAsync(Department department);


    }
}
