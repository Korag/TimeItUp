using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.DataAccess
{
    public class EFDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users { get; set; }

        public DbSet<Timer> Timers { get; set; }

        public DbSet<Split> Splits { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<Pause> Pauses { get; set; }

        public EFDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("EFDatabaseConnectionString"));
            optionsBuilder.LogTo(Console.WriteLine);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            modelBuilder.Entity<User>().HasMany(p => p.Timers)
                                       .WithOne(b => b.User);

            modelBuilder.Entity<Alarm>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Alarm>().HasOne(p => p.Timer)
                                        .WithMany(b => b.Alarms);

            modelBuilder.Entity<Timer>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Timer>().HasOne(p => p.User)
                                        .WithMany(b => b.Timers);
            modelBuilder.Entity<Timer>().HasMany(p => p.Splits)
                                        .WithOne(b => b.Timer);
            modelBuilder.Entity<Timer>().HasMany(p => p.Alarms)
                                        .WithOne(b => b.Timer);
            modelBuilder.Entity<Timer>().HasMany(p => p.Pauses)
                                        .WithOne(b => b.Timer);

            modelBuilder.Entity<Split>().HasKey(x => new { x.Id, x.TimerId });
            modelBuilder.Entity<Split>().HasOne(p => p.Timer)
                                        .WithMany(b => b.Splits);

            modelBuilder.Entity<Pause>().HasKey(x => new { x.Id, x.TimerId });
            modelBuilder.Entity<Pause>().HasOne(p => p.Timer)
                                        .WithMany(b => b.Pauses);

            base.OnModelCreating(modelBuilder);
        }
    }
}
