using GTL.Application.Contracts;

namespace GTL.Customer.Application.Contracts;

public interface ICustomerRepository : IRepository<Domain.Aggregates.Customer>
{
    public Task<bool> Exists(string email);
}