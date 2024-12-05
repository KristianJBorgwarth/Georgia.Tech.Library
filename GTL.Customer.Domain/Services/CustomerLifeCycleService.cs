using GTL.Customer.Domain.Events;

namespace GTL.Customer.Domain.Services;

public interface ICustomerLifeCycleService
{
    void MarkCustomerForDeletion(Aggregates.Customer customer);
}

public class CustomerLifeCycleService : ICustomerLifeCycleService
{
    public void MarkCustomerForDeletion(Aggregates.Customer customer)
    {
        customer.SetDelete();
        customer.AddDomainEvent(new CustomerDeletedEvent(customer.Id));
    }
}