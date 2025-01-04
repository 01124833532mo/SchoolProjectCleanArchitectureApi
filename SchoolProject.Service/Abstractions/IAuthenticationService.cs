using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(User user);
    }
}
