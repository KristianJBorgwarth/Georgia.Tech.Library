using System.Net.Http.Json;
using GTL.Application.Contracts;
using GTL.Customer.Persistence.Context;
using GTL.Customer.Persistence.Outboxing;
using GTL.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GTL.Customer.Persistence.Repositories;

public class UnitOfWork(CustomerServiceDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ConvertDomainEventsToOutboxMessages();
        UpdateAuditableEntities();
        return context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Converts domain events of all <see cref="AggregateRoot"/> entities tracked by the context into
    /// outbox messages and persists them to the database.
    /// </summary>
    private void ConvertDomainEventsToOutboxMessages()
    {
        var outBoxMessages = context.ChangeTracker.Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .SelectMany(aggregateRoot =>
            {
                var domainEvents = aggregateRoot.DomainEvents.ToList();
                aggregateRoot.ClearDomainEvents();
                return domainEvents;
            }).Select(domainEvent => new OutboxMessage()
                {
                    Type = domainEvent.GetType().AssemblyQualifiedName ?? throw new InvalidOperationException("The domain event type must have an assembly qualified name"),
                    Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }),
                }
            ).ToList();

        if (outBoxMessages.Count != 0)
            context.Set<OutboxMessage>().AddRange(outBoxMessages);
    }

    /// <summary>
    /// Updates the Created and LastModified timestamps for auditable entities.
    /// </summary>
    private void UpdateAuditableEntities()
    {
        var entities = context.ChangeTracker.Entries<Entity>().Where(e=> e.State is EntityState.Added or EntityState.Modified);

        foreach (var entity in entities)
        {
            if (entity.State is EntityState.Added)
            {
                entity.Entity.SetCreated();
            }

            entity.Entity.SetLastModified();
        }
    }
}