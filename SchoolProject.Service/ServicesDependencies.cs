using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IStudentService), typeof(StudentService));

            return services;
        }
    }
}
