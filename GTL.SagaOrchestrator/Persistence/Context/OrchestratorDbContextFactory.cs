using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GTL.SagaOrchestrator.Persistence.Context;

public sealed class OrchestratorDbContextFactory : IDesignTimeDbContextFactory<OrchestratorDbContext>
{
    public OrchestratorDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrchestratorDbContext>();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("GTL.SagaOrchestratorDbConnectionString");

        optionsBuilder.UseSqlServer(connectionString, b =>
            b.MigrationsAssembly(typeof(OrchestratorDbContext).Assembly.FullName));

        return new OrchestratorDbContext(optionsBuilder.Options);
    }
}