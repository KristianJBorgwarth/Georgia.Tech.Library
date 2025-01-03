using GTL.Messaging.RabbitMq.Messages.BookMessages;
using GTL.Messaging.RabbitMq.Messages.CustomerMessages;
using GTL.Messaging.RabbitMq.Producer;
using GTL.Warehouse.Persistence.Entities;
using GTL.Warehouse.Persistence.Repositories;
using MassTransit;

namespace Georgia.Tech.Library.Consumers
{
    public class CustomerDeletedMessageConsumer //: IConsumer<CustomerDeletedMessage>
    {
        private readonly IBookRepository _repository;
        private readonly IProducer<BookQuantityChangedMessage> _producer;


        public CustomerDeletedMessageConsumer(IBookRepository repository, IProducer<BookQuantityChangedMessage> producer)
        {
            _repository = repository;
            _producer = producer;
        }

        public async Task Consume(ConsumeContext<CustomerDeletedMessage> context)
        {


            /*var message = context.Message;

            var booksToDelete = await _repository.GetBooksByUserIdAsync(message.Id);

            // Trying to go through each book we want to delete and then produce a message that informs a service about the book we deleted and the quantity.

            foreach (var book in booksToDelete)
            {
                var quantityChangedMessage = new BookQuantityChangedMessage
                {
                    BookId = book.Id,
                    Quantity = book.Quantity -1,
                    

                };
            }

            await _repository.DeleteBookWithUserIdAsync(message.Id);
           
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
}
