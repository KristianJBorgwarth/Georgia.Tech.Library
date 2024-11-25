namespace GTL.Messaging.RabbitMq.Messages;

public abstract class BaseMessage(Guid correlationId, Guid? causationId = null)
{
    /// <summary>
    /// The time that the message was created.
    /// </summary>
    public DateTime OccurredOn { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// The unique identifier for this message.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Should be the same for all messages that are part of the same logical operation.
    /// Set by the originating message.
    /// </summary>
    public Guid CorrelationId { get; private set; } = correlationId;

    /// <summary>
    /// Should match the ID of the message that caused this message to be created.
    /// Will be null for the first message in a chain.
    /// </summary>
    public Guid? CausationId { get; private set; } = causationId;
}