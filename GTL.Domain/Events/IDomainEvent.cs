using MediatR;

namespace GTL.Domain.Events;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}