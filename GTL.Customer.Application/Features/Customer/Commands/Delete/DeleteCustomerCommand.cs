using GTL.Application.Abstractions;

namespace GTL.Customer.Application.Features.Customer.Commands.Delete;

public sealed record DeleteCustomerCommand : ICommand
{
    public required Guid Id { get; init; }
}