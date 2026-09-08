using Microsoft.Extensions.DependencyInjection;
using Sindibad.Application.IServices;
using Sindibad.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sindibad.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
