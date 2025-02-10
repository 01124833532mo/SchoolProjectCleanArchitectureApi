using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.AuthServices.Contracts;

namespace SchoolProject.Service.AuthServices.Implementation
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }



        public async Task<User> GetUserAsync()
        {
            var userid = GetUserId();
            var user = await _userManager.FindByIdAsync(userid.ToString());
            if (user == null)
                throw new UnauthorizedAccessException();

            return user;

        }

        public int GetUserId()
        {
            var userid = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimModel.Id)).Value;
            if (userid == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(userid);
        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
    }
}
