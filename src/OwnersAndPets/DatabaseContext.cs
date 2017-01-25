using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OwnersAndPets.Models;
using System.IO;

namespace OwnersAndPets
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new Configuration.ConfigurationProvider("Configuration\\configuration.json").LoadConfig();

            if (!Directory.Exists(config.PathToDb))
                Directory.CreateDirectory(config.PathToDb);

            DirectoryInfo di = new DirectoryInfo(config.ConnectionString);

            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = di.FullName
            };

            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}