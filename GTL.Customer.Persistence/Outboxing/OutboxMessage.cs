// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace GTL.Customer.Persistence.Outboxing;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string Type { get; init; }
    public string Content { get; init; }
    public DateTime OccurredOn { get; init; }
    public DateTime? ProcessedOn { get; private set; }
    public string? Error { get; private set; } = string.Empty;

    public OutboxMessage(string type, string content)
    {
        Id = Guid.NewGuid();
        Type = type;
        Content = content;
        OccurredOn = DateTime.UtcNow;
    }

    public void MarkAsProcessed()
    {
        ProcessedOn = DateTime.UtcNow;
    }

    public void MarkAsFailed(string error)
    {
        ProcessedOn = DateTime.UtcNow;
        Error = error;
    }
}