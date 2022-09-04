using CrudCosmos.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudCosmos
{
    public class CosmosContext : DbContext
    {

        private readonly FunctionConfiguration _config;

        public DbSet<Book> Books { get; set; }

        public CosmosContext(FunctionConfiguration config)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Book>().ToContainer("Book")
                .HasNoDiscriminator().HasPartitionKey("Id");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(
                accountEndpoint: _config.CosmosAccountEndpoint,
                accountKey: _config.CosmosAccountKey,
                databaseName: _config.CosmosDatabaseName);
        }
    }
}