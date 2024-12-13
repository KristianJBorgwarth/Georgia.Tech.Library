using GTL.Messaging.RabbitMq.Messages;

namespace GTL.Warehouse.API.Messages.BookCreatedMessage;

public class BookCreatedMessage : BaseMessage
{
    public string Title { get; set; }
  
    public string Price { get; set; }

    public BookCreatedMessage(string title, string price, Guid correlationId, Guid? causationId = null)
        : base(correlationId, causationId)
    {
        Title = title;
        
        Price = price;
    }
}
