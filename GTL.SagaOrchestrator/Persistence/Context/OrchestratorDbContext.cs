using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace GTL.SagaOrchestrator.Persistence.Context;

public class OrchestratorDbContext(DbContextOptions options) : SagaDbContext(options)
{
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get; //yield return the saga map
    }
}