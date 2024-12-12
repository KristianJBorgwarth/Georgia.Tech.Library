using GTL.SagaOrchestrator.Saga.Order;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GTL.SagaOrchestrator.Persistence.ModelConfigurations;

public class OrderProcessingSagaMap : SagaClassMap<OrderProcessingSagaState>
{
    protected override void Configure(EntityTypeBuilder<OrderProcessingSagaState> entity, ModelBuilder model)
    {
        entity.ToTable("OrderProcessingSaga");
    }
}