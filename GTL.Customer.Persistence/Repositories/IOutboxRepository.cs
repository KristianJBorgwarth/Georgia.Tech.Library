using GTL.Customer.Persistence.Outboxing;

namespace GTL.Customer.Persistence.Repositories;

public interface IOutboxRepository
{
    Task<OutboxMessage[]> GetUnprocessedAsync(int batchSize);
    Task MarkAsProcessedAsync(OutboxMessage message);
}

public class OutboxRepository : IOutboxRepository
{
    public Task<OutboxMessage[]> GetUnprocessedAsync(int batchSize)
    {
        throw new NotImplementedException();
    }

    public Task MarkAsProcessedAsync(OutboxMessage message)
    {
        throw new NotImplementedException();
    }
}