using Microsoft.EntityFrameworkCore;

namespace SportTracker.Server.Models
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<SportEvent> SportEvents => Set<SportEvent>();
        public DbSet<User> Users => Set<User>();
    }
}
