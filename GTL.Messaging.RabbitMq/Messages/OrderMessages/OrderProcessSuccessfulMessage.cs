namespace GTL.Messaging.RabbitMq.Messages.OrderMessages;

public class OrderProcessSuccessfulMessage : BaseMessage
{
    public List<Guid> BookIds { get; init; }

    public OrderProcessSuccessfulMessage(Guid correlationId, Guid? causationId, List<Guid> bookIds) : base(correlationId, causationId)
    {
        BookIds = bookIds;
    }
}