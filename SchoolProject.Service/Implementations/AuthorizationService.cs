using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Data;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : Abstractions.IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext _dbContext;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager,ApplicationDbContext dbContext)
        {
            _roleManager = roleManager;
            this.userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<string> UpdateUserClaim(UpdateUserClaimsRequest updateUserClaimsRequest)
        {
            var transact = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await userManager.FindByIdAsync(updateUserClaimsRequest.UserId.ToString());
                if (user == null)
                {
                    return "UserIsNull";
                }
                //remove old Claims
                var userClaims = await userManager.GetClaimsAsync(user);
                var removeClaimsResult = await userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeClaimsResult.Succeeded)
                    return "FailedToRemoveOldClaims";
                var claims = updateUserClaimsRequest.UserClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

                var addUserClaimResult = await userManager.AddClaimsAsync(user, claims);
                if (!addUserClaimResult.Succeeded)
                    return "FailedToAddNewClaims";

                await transact.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                return "FailedToUpdateClaims";
            }
        }
    }
}
