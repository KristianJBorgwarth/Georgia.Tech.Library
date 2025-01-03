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
        try
        {
            var booksToDelete = context.Message.BookIds.ToList();
            foreach (var bookId in booksToDelete)
            {

                var book = await _repository.GetBookByBookIdAsync(bookId);

                await _repository.DeleteBookWithBookIdAsync(bookId);

                var amount = await _repository.GetBookCountByIdAndTitleAsync(book.Title, book.BookDetailsId);

                var message = new BookQuantityChangedMessage(
                 bookId,
                 amount,
                 context.Message.CorrelationId
                 );

                    await _producer.PublishMessageAsync(message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "En error occured while processing the OrderProcessSuccesfulMessage");
            throw;
        }
    }
}