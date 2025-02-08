using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IStudentService), typeof(StudentService));
            services.AddTransient(typeof(IDepartmentServices), typeof(DepartmentServices));
            services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddTransient(typeof(IAuthorizationService), typeof(AuthorizationService));
            services.AddTransient(typeof(IEmailService), typeof(EmailService));

            services.Configure<JwtSettings>(configuration.GetSection("jwtSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("emailSettings"));


            return services;
        }
    }
}
