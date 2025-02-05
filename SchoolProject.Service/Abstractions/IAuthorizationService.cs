using SchoolProject.Data.DTOs;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string RoleName);
        public Task<bool> IsRoleExist(string RoleName);
        public Task<ManageUserRolesResult> GetManageUserRolesData(User user);


    }
}
