using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.OrderService.Persistence.Entities;
using GTL.OrderService.Persistence.Repositories;
using MassTransit;

namespace GTL.OrderService.API.Consumers;

public class RollbackOrderProcessStatusMessageConsumer(
    ILogger<RollbackOrderProcessStatus> logger,
    IOrderRepository orderRepository)
    : IConsumer<RollbackOrderProcessStatus>
{
    public async Task Consume(ConsumeContext<RollbackOrderProcessStatus> context)
    {
        try
        {
            var message = context.Message;
            var order = await orderRepository.GetByIdAsync(message.OrderId);

            if (order is null)
            {
                logger.LogError("Order with id {OrderId} not found", message.OrderId);
                throw new Exception($"Order with id {message.OrderId} not found");
            }
            order.SetOrderStatus(OrderStatus.Failed);

            await orderRepository.UpdateAsync(order);

            logger.LogInformation("Order with id {OrderId} status set to {OrderStatus}", message.OrderId, OrderStatus.Failed);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error consuming message");
            throw;
        }
    }
}