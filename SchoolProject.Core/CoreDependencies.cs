using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstractions;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core
{
    public  static class CoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(option=>option.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
