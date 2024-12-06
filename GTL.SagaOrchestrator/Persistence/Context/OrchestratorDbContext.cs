using GTL.SagaOrchestrator.Persistence.ModelConfigurations;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace GTL.SagaOrchestrator.Persistence.Context;

public class OrchestratorDbContext(DbContextOptions options) : SagaDbContext(options)
{
    protected override IEnumerable<ISagaClassMap> Configurations
    {
        get
        {
            yield return new OrderProcessingSagaMap();
        }
    }
}