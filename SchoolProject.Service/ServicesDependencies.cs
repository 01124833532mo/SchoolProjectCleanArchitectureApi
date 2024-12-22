using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implementations;

namespace SchoolProject.Service
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IStudentService), typeof(StudentService));
            services.AddTransient(typeof(IDepartmentServices), typeof(DepartmentServices));


            return services;
        }
    }
}
