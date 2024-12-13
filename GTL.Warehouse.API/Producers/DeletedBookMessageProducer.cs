using Georgia.Tech.Library.Messages;
using MassTransit;

public class DeletedBookMessageProducer
{
    private readonly IPublishEndpoint _publishEndpoint;

    public DeletedBookMessageProducer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishDeletedBook(Guid bookId)
    {
        await _publishEndpoint.Publish(new DeletedBookMessage
        {
            BookId = bookId
        });
    }
}
