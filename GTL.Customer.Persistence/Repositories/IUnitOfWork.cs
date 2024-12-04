using GTL.Application.Contracts;
using GTL.Customer.Persistence.Context;
using GTL.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace GTL.Customer.Persistence.Repositories;

public class UnitOfWork(CustomerServiceDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        return context.SaveChangesAsync(cancellationToken);
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