using Microsoft.AspNetCore.Http;
using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstractions
{
    public interface IInstractorService
    {
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExist(string name);

        public Task<bool> IsNameArExistExcludeSelf(string name, int id);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);

        public Task<string> AddInstractorAsync(Instructor instructor, IFormFile file);

        public Task<decimal> GetSalarySummationOfInstructor();
        public Task<bool> IsInstractorIdExist(int? instractorId);

    }
}
