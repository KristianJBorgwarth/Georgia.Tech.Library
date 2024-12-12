// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength
namespace GTL.Customer.Persistence.Outboxing;

public sealed class OutboxMessage
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Type { get; init; } = null!;
    public string Content { get; init; } = null!;
    public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;
    public DateTime? ProcessedOn { get; private set; }
    public string? Error { get; private set; }

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