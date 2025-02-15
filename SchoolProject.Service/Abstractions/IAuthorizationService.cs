using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string RoleName);
        public Task<string> UpdateUserClaim(UpdateUserClaimsRequest  updateUserClaimsRequest);
        public Task<bool> IsRoleExist(string RoleName);
        public Task<ManageUserRolesResult> ManageUserRolesData(User user);

        public Task<ManageUserClaimsResult> ManageUserClaimData(User user);


    }
}
