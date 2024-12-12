using System.Text.Json.Serialization;

namespace GTL.Customer.API.Base;

/// <summary>
/// The responsibility for the envelope is to wrap any result to create a consistent resultset to the clients.
/// </summary>
public class Envelope : Envelope<string>
{
    [JsonConstructor]//This is needed for deserialization for tests
    protected internal Envelope(string errorMessage) : base(null, errorMessage)
    {
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }

    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    public static Envelope Error(string errormessage)
    {
        return new Envelope(errormessage);
    }
}

public class Envelope<T>
{
    public T Result { get; }
    public string ErrorMessage { get; }
    public DateTime TimeGenerated { get; set; }

    [JsonConstructor]//This is needed for deserialization for tests
    protected internal Envelope(T result, string errorMessage)
    {
        this.Result = result;
        this.ErrorMessage = errorMessage;
        this.TimeGenerated = DateTime.UtcNow;
    }
}