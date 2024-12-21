﻿using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstractions
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();

        public Task<Student> GetStudentByIdWithIncludeAsync(int id);
        public Task<Student> GetByIdAsync(int id);


        public Task<string> AddAsync(Student student);

        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);

        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);


        public Task<string> EditAsync(Student student);

        public Task<string> DeleteAsync(Student student);

        public IQueryable<Student> GetStudentsQuarable();
        public IQueryable<Student> FilterStudentsPaginatedQueryable(StudentOrderingEnum orderingEnum, string search);



    }
}
