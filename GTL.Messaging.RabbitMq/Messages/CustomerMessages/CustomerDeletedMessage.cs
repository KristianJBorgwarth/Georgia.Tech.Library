namespace GTL.Messaging.RabbitMq.Messages.CustomerMessages;

public class CustomerDeletedMessage : BaseMessage
{
    public Guid CustomerId { get; init; }

    public CustomerDeletedMessage(Guid customerId, Guid correlationId,
        Guid? causationId = null) : base(correlationId, causationId)
    {
        CustomerId = customerId;
    }
}