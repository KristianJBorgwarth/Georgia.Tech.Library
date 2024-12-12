using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GTL.Warehouse.Persistence.Context
{
    public class WarehouseDbContextFactory : IDesignTimeDbContextFactory<WarehouseDbContext>
    {
        public WarehouseDbContext CreateDbContext(string[] args)
        {
            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<WarehouseDbContext>();
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__WarehouseDbConnectionString");
            optionsBuilder.UseSqlServer(connectionString);

            return new WarehouseDbContext(optionsBuilder.Options);
        }
    }
}
