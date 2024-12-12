using GTL.Customer.Persistence.Context;
using GTL.Customer.Persistence.Outboxing;
using Microsoft.EntityFrameworkCore;

namespace GTL.Customer.Persistence.Repositories;

internal interface IOutboxRepository
{
    Task<OutboxMessage[]> GetUnprocessedAsync(int batchSize);
    Task MarkAsProcessedAsync(OutboxMessage message);
}

sealed internal class OutboxRepository(CustomerServiceDbContext context) : IOutboxRepository
{
    public async Task<OutboxMessage[]> GetUnprocessedAsync(int batchSize)
    {
        return await context.OutboxMessages.Where(x=> x.ProcessedOn == null).Take(batchSize).ToArrayAsync();
    }

    public Task MarkAsProcessedAsync(OutboxMessage message)
    {
        message.MarkAsProcessed();
        return Task.CompletedTask;
    }
}