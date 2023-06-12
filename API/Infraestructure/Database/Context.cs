using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infraestructure.Database.Entities;
using API.Infraestructure.Database.Map;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.Database
{
    public class Context : DbContext
    {
        public DbSet<User> Users => Set<User>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DBCONN");
            optionsBuilder.UseNpgsql(connectionString, options =>
            {
                options.MigrationsHistoryTable("_MigrationHistory", "userControl");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            });
            base.OnConfiguring(optionsBuilder);

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}