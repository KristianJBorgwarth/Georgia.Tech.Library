using GTL.Messaging.RabbitMq.Messages;

namespace GTL.Messaging.RabbitMq.Producer;

public interface IProducer<in TMessage> where TMessage : BaseMessage
{
    Task PublishMessageAsync(TMessage message, CancellationToken cancellationToken = default);
    Task SendMessageASync(TMessage message,string queueName, CancellationToken cancellationToken = default);
}