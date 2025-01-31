using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : Abstractions.IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;

        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string RoleName)
        {
            return await _roleManager.RoleExistsAsync(RoleName);
        }
    }
}
