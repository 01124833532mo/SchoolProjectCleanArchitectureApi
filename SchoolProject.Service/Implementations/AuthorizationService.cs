using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : Abstractions.IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> userManager;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            this.userManager = userManager;
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
        public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userrole = new UserRoles();
                userrole.Id = role.Id;
                userrole.Name = role.Name;
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userrole.HasRole = true;
                }
                else
                {
                    userrole.HasRole = false;
                }
                rolesList.Add(userrole);
            }
            response.Roles = rolesList;
            return response;

        }


        public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
        {
            var response = new ManageUserClaimsResult();
            var userclaimsList = new List<UserClaims>();

            response.UserId = user.Id;

            var userClaims = await userManager.GetClaimsAsync(user);

            foreach (var claim in ClaimsStore.claims)
            {

                var userclaim = new UserClaims();
                userclaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type))
                {
                    userclaim.Value = true;
                }
                else
                {
                    userclaim.Value = false;
                }
                userclaimsList.Add(userclaim);
            }
            response.UserClaims = userclaimsList;
            return response;

        }
    }
}
