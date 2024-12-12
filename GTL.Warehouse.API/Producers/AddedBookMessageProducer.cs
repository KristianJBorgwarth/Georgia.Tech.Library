using Georgia.Tech.Library.Messages;
using MassTransit;

public class AddedBookMessageProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public AddedBookMessageProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAddedBook(Guid bookId, string title, int quantity)
    {
        await _publishEndpoint.Publish(new AddedBookMessage
        {
            BookId = bookId,
            Title = title,
            Quantity = quantity
        });
    }
}
