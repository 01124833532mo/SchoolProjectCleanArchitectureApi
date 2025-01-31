namespace SchoolProject.Service.Abstractions
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string RoleName);
        public Task<bool> IsRoleExist(string RoleName);


    }
}
