using Georgia.Tech.Library.Context;
using Georgia.Tech.Library.Messages;
using MassTransit;

public class CustomerDeletedConsumer : IConsumer<CustomerDeletedMessage>
{
	private readonly WarehouseDbContext _context;

	public CustomerDeletedConsumer(WarehouseDbContext context)
	{
		_context = context;
	}

	public async Task Consume(ConsumeContext<CustomerDeletedMessage> context)
	{
		var customerId = context.Message.CustomerId;

		// Assuming Books has a field indicating ownership
		var books = _context.Books.Where(b => b.OwnerId == customerId);
		foreach (var book in books)
		{
			book.OwnerId = null; // Mark books as unowned
		}

		await _context.SaveChangesAsync();
	}
}
