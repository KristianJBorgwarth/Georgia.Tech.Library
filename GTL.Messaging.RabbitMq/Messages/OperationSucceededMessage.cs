namespace GTL.Messaging.RabbitMq.Messages;

public enum OperationType
{
    PaymentRequest,
}
public class OperationSucceededMessage(OperationType operationType, Guid correlationId, Guid? causationId = null) : BaseMessage(correlationId, causationId)
{
    public OperationType OperationType { get; private set; } = operationType;
}