namespace GTL.Messaging.RabbitMq.Messages.OrderMessages;

public sealed class OrderProcessingRequestMessage : BaseMessage
{
    public Guid CustomerId { get; init; }
    public Guid OrderId { get; init; }
    public List<Guid> BookIds { get; init; }
    public int Price { get; init; }
    public string CardNumber { get; init; }
    public string ExpirationDate { get; init; }
    public int CVC { get; init; }

    public OrderProcessingRequestMessage(Guid customerId, Guid orderId, List<Guid> bookIds, int price, string cardNumber, string expirationDate, int cvc, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        CustomerId = customerId;
        OrderId = orderId;
        BookIds = bookIds;
        Price = price;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CVC = cvc;
    }
}