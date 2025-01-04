using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services/*,IConfiguration configuration*/)
        {
            services.AddTransient(typeof(IStudentService), typeof(StudentService));
            services.AddTransient(typeof(IDepartmentServices), typeof(DepartmentServices));
            services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));

            //services.Configure<JwtSettings>(configuration.GetSection("jwtSettings"));


            return services;
        }
    }
}
