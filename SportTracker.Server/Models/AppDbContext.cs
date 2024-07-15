using Microsoft.EntityFrameworkCore;
using SportTracker.Shared.Models;

namespace SportTracker.Server.Models 
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
        public DbSet<SportEvent> SportEvents => Set<SportEvent>();
    }
}