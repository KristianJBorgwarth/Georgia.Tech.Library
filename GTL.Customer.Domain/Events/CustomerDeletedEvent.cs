using GTL.Domain.Events;

namespace GTL.Customer.Domain.Events;

public sealed record CustomerDeletedEvent(Guid CustomerId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}