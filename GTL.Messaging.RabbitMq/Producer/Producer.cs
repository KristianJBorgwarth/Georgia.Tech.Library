using GTL.Messaging.RabbitMq.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GTL.Messaging.RabbitMq.Producer;

public class Producer<TMessage> : IProducer<TMessage> where TMessage : BaseMessage
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<Producer<TMessage>> _logger;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public Producer(IPublishEndpoint publishEndpoint, ILogger<Producer<TMessage>> logger, ISendEndpoint sendEndpoint, ISendEndpointProvider sendEndpointProvider)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _sendEndpointProvider = sendEndpointProvider;
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

    public async Task SendMessageASync(TMessage message, string queueName, CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endpoint.Send(message, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Exception occurred in {Producer} while sending message with id: {Id} to queue: {Queue}",
                nameof(Producer<TMessage>),
                message.Id,
                queueName);
        }
    }
}