using Georgia.Tech.Library.Messages;
using GTL.Warehouse.Persistence.Context;
using MassTransit;

public class ProcessOrderFailedConsumer : IConsumer<ProcessOrderFailedMessage>
{
    private readonly WarehouseDbContext _context;

    public ProcessOrderFailedConsumer(WarehouseDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<ProcessOrderFailedMessage> context)
    {
        var message = context.Message;

        for (int i = 0; i < message.BookIds.Count; i++)
        {
            var book = await _context.Books.FindAsync(message.BookIds[i]);
            if (book != null)
            {
                book.Quantity += message.Quantities[i]; // Restore stock
            }
        }

        await _context.SaveChangesAsync();
    }
}
