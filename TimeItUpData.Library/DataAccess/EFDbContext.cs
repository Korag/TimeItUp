using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.DataAccess
{
    public class EFDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Timer> Timers { get; set; }
        public DbSet<Split> Splits { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<User> Users { get; set; }

        public EFDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EFDatabaseConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });

            modelBuilder.Entity<Timer>().HasKey(x => new { x.Id, x.UserId });

            modelBuilder.Entity<Alarm>().HasKey(x => new { x.Id, x.TimerId });
            modelBuilder.Entity<Split>().HasKey(x => new { x.Id, x.TimerId });
            modelBuilder.Entity<Pause>().HasKey(x => new { x.Id, x.TimerId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
