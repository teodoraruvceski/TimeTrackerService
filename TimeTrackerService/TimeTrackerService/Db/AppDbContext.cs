using Microsoft.EntityFrameworkCore;
using TimeTrackerService.Models;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet properties represent database tables
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects{ get; set; }
        // Add DbSet properties for other entities as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>()
                    .HasKey(t => t.Id);
            modelBuilder.Entity<Project>()
                    .HasKey(t => t.Id);
        }

    }
}
