using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace PlannerCore
{
    public class MainDbContext : DbContext
    {
        readonly string _connectionString;
        public DbSet<PlannedEvent> PlannedEvents { get; set; }
        public DbContextOptions? DbContextOptions { get; set; }
        public IConfigurationRoot Configuration { get; private set; }
        public MainDbContext()
        {
            Configuration = SetupJsonConfig();
            _connectionString = GetConnectionString();
            Database.EnsureCreated();
        }

        private IConfigurationRoot SetupJsonConfig()
        {
            var builder = new ConfigurationBuilder();
            string debugDir = Directory.GetCurrentDirectory();
            builder.SetBasePath(debugDir);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config;
        }

        private string GetConnectionString()
        {

            string? connectionString = Configuration.GetConnectionString("DefaultConnection");
            if (connectionString is null)
            {
                throw new NullReferenceException("Invalid database location, check appsettings.json");
            }
            return connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
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
                .Where(e => e.Entity is PlannedEvent
                && (e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((PlannedEvent)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((PlannedEvent)entityEntry.Entity).CreatedDateTime = DateTime.Now;
                    ((PlannedEvent)entityEntry.Entity).IsDone = false;
                }
            }
            return base.SaveChanges();
        }
    }
}
