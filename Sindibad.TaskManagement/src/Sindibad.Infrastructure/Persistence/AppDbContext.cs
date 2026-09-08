using Microsoft.EntityFrameworkCore;

namespace Sindibad.Infrastructure.Persistence
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
      
     
    }
}
