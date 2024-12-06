namespace GTL.Customer.Application.Features.Customer.Commands.Create;

public sealed record CreateCustomerDto
{
    public Guid Id { get; private init; }
    public string Username { get; private init; } = null!;

    
    private CreateCustomerDto(Guid id, string username)
    {
        Id = id;
        Username = username;
    }
    
    public static CreateCustomerDto Map(Domain.Aggregates.Customer customer)
    {
        return new CreateCustomerDto(customer.Id, customer.Username);
    }
}