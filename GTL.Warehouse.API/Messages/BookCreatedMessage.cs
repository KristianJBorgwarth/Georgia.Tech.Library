using GTL.Messaging.RabbitMq.Messages;

namespace GTL.Warehouse.API.Messages.BookCreatedMessage;

public class BookCreatedMessage : BaseMessage
{
    public string Title { get; set; }
    public int Quantity { get; set; }
    public string Price { get; set; }

    public BookCreatedMessage(string title, int quantity, string price, Guid correlationId, Guid? causationId = null)
        : base(correlationId, causationId)
    {
        Title = title;
        Quantity = quantity;
        Price = price;
    }
}
