using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Abstracts.Functions;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Repositories;
using SchoolProject.Infrastructure.Repositories.Functions;

namespace SchoolProject.Infrastructure
{
    public static class InfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddTransient(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            services.AddTransient(typeof(IInstructorsRepository), typeof(InstructorsRepository));
            services.AddTransient(typeof(ISubjectRepository), typeof(SubjectRepository));
            services.AddTransient(typeof(IRefreshTokenRepository), typeof(RefreshTokenRepository));
            services.AddTransient(typeof(IInstructorFunctionsRepository), typeof(InstructorFunctionsRepository));


            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
