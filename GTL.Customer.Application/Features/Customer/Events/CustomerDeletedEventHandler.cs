using GTL.Application.Abstractions;
using GTL.Customer.Application.Contracts;
using GTL.Customer.Domain.Events;
using GTL.Messaging.RabbitMq.Messages.CustomerMessages;
using GTL.Messaging.RabbitMq.Producer;
using Microsoft.Extensions.Logging;

namespace GTL.Customer.Application.Features.Customer.Events;

public class CustomerDeletedEventHandler(
    ICustomerRepository customerRepository,
    IProducer<CustomerDeletedMessage> producer,
    ILogger<CustomerDeletedEventHandler> logger)
    : IDomainEventHandler<CustomerDeletedEvent>
{
    public async Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(notification.CustomerId, cancellationToken);

        if (customer is null)
        {
            logger.LogWarning("Customer with id {Id} was not found", notification.CustomerId);
            throw new InvalidOperationException("Customer not found");
        }

        var message = new CustomerDeletedMessage(customer.Id, customer.Email.Address, customer.GetFullName(), Guid.NewGuid());
        await producer.PublishMessageAsync(message, cancellationToken);
    }
}