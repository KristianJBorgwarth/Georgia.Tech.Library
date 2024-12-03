using GTL.Customer.Application.Contracts;
using GTL.Customer.Persistence.Context;

namespace GTL.Customer.Persistence.Repositories;

public class CustomerRepository(CustomerServiceDbContext context) : ICustomerRepository
{
    public Task<Domain.Aggregates.Customer> AddAsync(Domain.Aggregates.Customer entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Aggregates.Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Aggregates.Customer> UpdateAsync(Domain.Aggregates.Customer entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Domain.Aggregates.Customer> DeleteAsync(Domain.Aggregates.Customer entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}