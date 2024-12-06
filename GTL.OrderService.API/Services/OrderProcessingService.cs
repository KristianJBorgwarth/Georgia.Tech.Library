﻿using GTL.Domain.Common;
using GTL.OrderService.API.Requests;
using GTL.OrderService.Persistence.Repositories;

namespace GTL.OrderService.API.Services;

public interface IOrderProcessingService
{
    Result ProcessOrder(OrderProcessingRequest request);
}
public class OrderProcessingService : IOrderProcessingService
{
    private readonly IOrderRepository _orderRepository;
    public OrderProcessingService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    public Result ProcessOrder(OrderProcessingRequest request)
    {
        Console.WriteLine("this is working");
        return Result.Ok();
    }
}