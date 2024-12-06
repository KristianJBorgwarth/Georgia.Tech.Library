namespace GTL.Messaging.RabbitMq.Messages.OrderMessages;

public sealed class PaymentRequestMessage : BaseMessage
{
    public int Price { get; init; }
    public string CardNumber { get; init; }
    public string ExpirationDate { get; init; }
    public int CVC { get; init; }

    public PaymentRequestMessage(int price, string cardNumber, string expirationDate, int cvc, Guid correlationId, Guid? causationId = null) : base(correlationId, causationId)
    {
        Price = price;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        CVC = cvc;
    }
}