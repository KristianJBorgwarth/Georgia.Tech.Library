using GTL.Messaging.RabbitMq.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GTL.Messaging.RabbitMq.Producer;

public class Producer<TMessage> : IProducer<TMessage> where TMessage : BaseMessage
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<Producer<TMessage>> _logger;

    public Producer(IPublishEndpoint publishEndpoint, ILogger<Producer<TMessage>> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishMessageAsync(TMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            await _publishEndpoint.Publish(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Exception occured in {Producer} for message with id: {Id}",
                nameof(Producer<TMessage>),
                message.Id);
        }
    }
}