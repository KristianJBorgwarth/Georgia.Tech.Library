using GTL.Customer.Domain.ValueObjects;
using GTL.Domain.Common;

namespace GTL.Customer.Domain.Aggregates;

public class Customer : AggregateRoot
{
    public string Username { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Password Password { get; private set; } = null!;
    
    public Customer() { } //For EF Core
    
    private Customer(string username, string firstName, string lastName, Email email, Password password)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
    
    public static Result<Customer> Create(string userName, string firstName, string lastName, string email, string password)
    {
        var emailResult = Email.Create(email);
        var passwordResult = Password.Create(password);
        
        if (emailResult.Success is false)
        {
            return Result.Fail<Customer>(emailResult.Error);
        }
        
        if (passwordResult.Success is false)
        {
            return Result.Fail<Customer>(passwordResult.Error);
        }
        
        return Result.Ok(new Customer(userName, firstName, lastName, emailResult.Value!, passwordResult.Value!));
    }
}