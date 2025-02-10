using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.AuthServices.Contracts
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();

        public int GetUserId();

        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
