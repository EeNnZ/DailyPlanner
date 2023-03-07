using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerCore
{
    public class MainDbContext : DbContext
    {
        readonly string connectionString;
        public DbSet<PlannedEvent> PlannedEvents { get; set; }
        public DbContextOptions? DbContextOptions { get; set; }
        public MainDbContext()
        {
            connectionString = GetConnectionString();
            //Database.EnsureCreated();
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder();
            string debugDir = Directory.GetCurrentDirectory();
            builder.SetBasePath(debugDir);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string? connectionString = config.GetConnectionString("DefaultConnection");
            if (connectionString is null)
            {
                throw new NullReferenceException("Invalid database location, check appsettings.json");
            }
            return connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.LogTo((s) =>
            {
                Debug.WriteLine(s);
                using TextWriter writer = File.AppendText("application_log.txt");
                writer.WriteLine(s);
            });
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is PlannedEvent && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((PlannedEvent)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((PlannedEvent)entityEntry.Entity).CreatedDateTime = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
