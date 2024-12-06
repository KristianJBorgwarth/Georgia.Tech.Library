namespace GTL.Messaging.RabbitMq.Messages.BookMessages;

public class BookQuantityChangedMessage : BaseMessage
{
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }

    public BookQuantityChangedMessage(Guid bookId, int quantity, bool isIncrement, Guid correlationId,
        Guid? causationId = null) : base(correlationId, causationId)
    {
        BookId = bookId;
        Quantity = quantity;
    }
}