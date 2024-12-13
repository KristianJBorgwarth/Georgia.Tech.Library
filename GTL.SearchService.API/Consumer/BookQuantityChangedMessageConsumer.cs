using GTL.Messaging.RabbitMq.Messages.BookMessages;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using MassTransit;

namespace GTL.SearchService.API.Consumer
{
    public class BookQuantityChangedMessageConsumer : IConsumer<BookQuantityChangedMessage>
    {
        public async Task Consume(ConsumeContext<BookQuantityChangedMessage> context)
        {
            //Implement logic to adjust cache
            Console.WriteLine("BookId: {0}", context.Message.BookId);
            Console.WriteLine("Quantity: {0}", context.Message.Quantity);
        }
    }
}
