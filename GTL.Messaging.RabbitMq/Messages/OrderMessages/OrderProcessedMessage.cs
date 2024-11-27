namespace GTL.Messaging.RabbitMq.Messages.OrderMessages;

public sealed class OrderProcessedMessage : BaseMessage
{
    public float Price { get; set; }
    public string OrderId { get; set; }

    public OrderProcessedMessage(float price, string orderId, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        Price = price;
        OrderId = orderId;
    }
}