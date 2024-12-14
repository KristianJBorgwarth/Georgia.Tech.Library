using GTL.Messaging.RabbitMq.Messages.BookMessages;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Messaging.RabbitMq.Producer;
using GTL.Warehouse.Persistence.Repositories;
using MassTransit;

namespace Georgia.Tech.Library.Consumers;

public class OrderProcessSuccessfulMessageConsumer : IConsumer<OrderProcessSuccessfulMessage>
{
    private readonly IBookRepository _repository;
    private readonly ILogger<OrderProcessSuccessfulMessageConsumer> _logger;
    private readonly IProducer<BookQuantityChangedMessage> _producer;

    public OrderProcessSuccessfulMessageConsumer(IProducer<BookQuantityChangedMessage> producer, ILogger<OrderProcessSuccessfulMessageConsumer> logger, IBookRepository repository)
    {
        _producer = producer;
        _logger = logger;
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<OrderProcessSuccessfulMessage> context)
    {
        throw new NotImplementedException();
    }
}