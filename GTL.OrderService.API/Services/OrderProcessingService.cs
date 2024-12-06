using GTL.Domain.Common;
using GTL.Messaging.RabbitMq.Messages.OrderMessages;
using GTL.Messaging.RabbitMq.Producer;
using GTL.OrderService.API.Requests;
using GTL.OrderService.Persistence.Entities;
using GTL.OrderService.Persistence.Repositories;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GTL.OrderService.API.Services;

public interface IOrderProcessingService
{
    Task<Result> ProcessOrder(OrderProcessingRequest request);
}
public class OrderProcessingService : IOrderProcessingService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProducer<ProcessOrderRequestMessage> _producer;
    private readonly ILogger<OrderProcessingService> _logger;

    public OrderProcessingService(IOrderRepository orderRepository, IProducer<ProcessOrderRequestMessage> producer, ILogger<OrderProcessingService> logger)
    {
        _orderRepository = orderRepository;
        _producer = producer;
        _logger = logger;
    }
    public async Task<Result> ProcessOrder(OrderProcessingRequest request)
    {
        try
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);

            if (order is null)
            {
                _logger.LogError("Order with id {OrderId} not found", request.OrderId);
                return Result.Fail(Errors.General.NotFound(request.OrderId));
            }

            var bookIds = order.GetBookIds();

            var price = order.CalculateTotalPrice();
            var intPrice = Convert.ToInt32(price);

            var message = new ProcessOrderRequestMessage(request.CustomerId,
                request.OrderId,
                bookIds,
                intPrice,
                request.CardNumber,
                request.ExpirationDate,
                request.CVC,
                Guid.NewGuid());

            order.SetOrderStatus(OrderStatus.Completed);

            await _orderRepository.UpdateAsync(order);

            await _producer.PublishMessageAsync(message);

            return Result.Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing order");
            return Result.Fail(Errors.General.UnspecifiedError("Exception occured while processing order"));
        }
    }
}