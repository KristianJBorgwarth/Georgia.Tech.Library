using GTL.Application.Abstractions;

namespace GTL.Customer.Application.Features.Customer.Commands.Create;

public sealed record CreateCustomerCommand : ICommand<CreateCustomerDto>
{
    public required string Username { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}