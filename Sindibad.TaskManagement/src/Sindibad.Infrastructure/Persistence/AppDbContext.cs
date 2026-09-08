using Microsoft.EntityFrameworkCore;
using Sindibad.Domain.Entities;

namespace Sindibad.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<Sindibad.Domain.Entities.Task> Tasks => Set<Sindibad.Domain.Entities.Task>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
