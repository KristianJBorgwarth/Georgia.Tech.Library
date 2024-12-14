namespace GTL.Messaging.RabbitMq.Messages.OrderMessages;

public class RollbackOrderProcessStatus : BaseMessage
{
    public Guid OrderId { get; init; }

    public RollbackOrderProcessStatus(Guid orderId, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        OrderId = orderId;
    }
}