using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sindibad.Application.IRepositories;
using Sindibad.Infrastructure.Persistence;
using Sindibad.Infrastructure.Persistence.Repositories;


namespace Sindibad.Infrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register your singleton ConnectionProvider
            services.AddSingleton<ConnectionProvider>();


            // Use ConnectionProvider to get the connection string for DbContext
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var connectionProvider = serviceProvider.GetRequiredService<ConnectionProvider>();
                options.UseSqlServer(connectionProvider.GetConnectionString());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IProjectRepository, ProjectRepository>();
            return services;
        }
    }
}
