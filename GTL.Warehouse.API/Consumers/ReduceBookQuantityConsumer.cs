using Georgia.Tech.Library.Context;
using Georgia.Tech.Library.Messages;
using MassTransit;

public class ReduceBookQuantityConsumer : IConsumer<ReduceBookQuantityMessage>
{
    private readonly WarehouseDbContext _context;

    public ReduceBookQuantityConsumer(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<ReduceBookQuantityMessage> context)
    {
        var message = context.Message;

        var book = await _context.Books.FindAsync(message.BookId);
        if (book == null || book.quantity < message.Quantity)
        {
            throw new Exception("Insufficient stock or book not found");
        }

        book.quantity -= message.Quantity;
        await _context.SaveChangesAsync();
    }
}
