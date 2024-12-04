using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using MassTransit;

namespace Georgia.Tech.Library.Consumers;

public class OrderProcessedConsumer : IConsumer<OrderProcessedMessage>
{
    public async Task Consume(ConsumeContext<OrderProcessedMessage> context)
    {
        Console.WriteLine("Order Processed: {0}", context.Message.OrderId);
        Console.WriteLine("Price: {0}", context.Message.Price);
    }
}