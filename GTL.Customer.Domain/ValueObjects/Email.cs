using GTL.Domain.Common;

namespace GTL.Customer.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; private set; } = null!;

    public Email() { } //For EF Core

    // Private constructor to prevent instantiation outside the Create method
    private Email(string address)
    {
        Address = address;
    }

    /// <summary>
    /// Validates and creates an email address
    /// </summary>
    /// <param name="address">Address of the email</param>
    /// <returns>Result containing the Email, if param is valid</returns>
    public static Result<Email> Create(string address)
    {
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(address);
            var lowerCaseAddress = mailAddress.Address.ToLowerInvariant();
            return Result.Ok(new Email(lowerCaseAddress));
        }
        catch
        {
            return Result.Fail<Email>(Errors.General.UnspecifiedError("Invalid email address."));
        }
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString()
    {
        return Address;
    }


}