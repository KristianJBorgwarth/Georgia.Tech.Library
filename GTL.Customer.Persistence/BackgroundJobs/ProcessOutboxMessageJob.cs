using GTL.Application.Contracts;
using GTL.Customer.Persistence.Outboxing;
using GTL.Customer.Persistence.Repositories;
using GTL.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace GTL.Customer.Persistence.BackgroundJobs;

sealed internal class ProcessOutboxMessageJob(
    IOutboxRepository outboxRepository,
    IPublisher publisher,
    ILogger<ProcessOutboxMessageJob> logger,
    IUnitOfWork unitOfWork)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var messages = await outboxRepository.GetUnprocessedAsync(20);
            if (messages.Length is 0) return;

            foreach (var message in messages)
            {
                var domainEvent = DeserializeDomainEvent(message);
                if (domainEvent == null) continue;

                await publisher.Publish(domainEvent);
                await outboxRepository.MarkAsProcessedAsync(message);
            }

            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing outbox message");
        }
    }

    /// <summary>
    /// Deserializes an outbox message into a domain event.
    /// </summary>
    /// <param name="outboxMessage">OutboxMessage to be deserialized</param>
    /// <returns>IDomainEvent Representing the deserialized <see cref="OutboxMessage"/></returns>
    private IDomainEvent? DeserializeDomainEvent(OutboxMessage outboxMessage)
    {
        var eventType = Type.GetType(outboxMessage.Type);

        if (eventType == null)
        {
            logger.LogError("Error getting type for message with id: {Id}", outboxMessage.Id);
            return null;
        }

        var domainEvent = (IDomainEvent?)JsonConvert.DeserializeObject(outboxMessage.Content, eventType);

        if (domainEvent == null)
        {
            logger.LogError("Error deserializing message with id: {Id}", outboxMessage.Id);
            return null;
        }

        return domainEvent;
    }
}