namespace GTL.Messaging.RabbitMq.Messages.CustomerMessages;

public class CustomerDeletedMessage : BaseMessage
{
    public Guid CustomerId { get; init; }
    public string CustomerEmail { get; init; }
    public string CustomerName { get; init; }

    public CustomerDeletedMessage(Guid customerId, string customerEmail, string customerName, Guid correlationId,
        Guid? causationId = null) : base(correlationId, causationId)
    {
        CustomerEmail = customerEmail;
        CustomerName = customerName;
        CustomerId = customerId;
    }
}