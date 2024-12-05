using GTL.Customer.Application.Contracts;
using GTL.Customer.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GTL.Customer.Persistence.Repositories;

public class CustomerRepository(CustomerServiceDbContext context) : ICustomerRepository
{
    public async Task<Domain.Aggregates.Customer> AddAsync(Domain.Aggregates.Customer entity, CancellationToken cancellationToken = default)
    {
        var result = await context.Customers.AddAsync(entity, cancellationToken);
        return result.Entity;
    }

    public async Task<Domain.Aggregates.Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Customers.FindAsync([id], cancellationToken);
    }

    public Domain.Aggregates.Customer Update(Domain.Aggregates.Customer entity)
    {
        var result = context.Customers.Update(entity);
        return result.Entity;
    }

    public void Delete(Domain.Aggregates.Customer entity)
    {
        context.Customers.Remove(entity);
    }

    public async Task<bool> Exists(string email)
    {
        return await context.Customers.AnyAsync(c => c.Email.Address == email);
    }
}