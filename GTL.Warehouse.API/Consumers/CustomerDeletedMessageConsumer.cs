using GTL.Messaging.RabbitMq.Messages.BookMessages;
using GTL.Messaging.RabbitMq.Messages.CustomerMessages;
using GTL.Messaging.RabbitMq.Producer;
using GTL.Warehouse.Persistence.Entities.Book;
using MassTransit;

namespace Georgia.Tech.Library.Consumers
{
    public class CustomerDeletedMessageConsumer : IConsumer<CustomerDeletedMessage>
    {
        private readonly IProducer<BookQuantityChangedMessage> _producer;

        public Task Consume(ConsumeContext<CustomerDeletedMessage> context)
        {
            throw new NotImplementedException();
        }



        /* public Task Consume(ConsumeContext<CustomerDeletedMessage> context)
         {
             var message = context.Message;

             _producer.PublishMessageAsync(new BookQuantityChangedMessage());
         }
         // TODO: make consumer
     } */
    }
}
