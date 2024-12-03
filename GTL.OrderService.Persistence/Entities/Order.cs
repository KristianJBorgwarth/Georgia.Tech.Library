
using GTL.OrderService.Persistence.Abstractions;
using System.ComponentModel;

namespace GTL.OrderService.Persistence.Entities;

public enum OrderStatus
{
    Pending,
    Completed,
    Failed,
    Cancelled
}

public class Order : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid OrderItemId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order(Guid userId, Guid orderItemId, OrderStatus orderStatus)
    {
        UserId = userId;
        OrderItemId = orderItemId;
        OrderStatus = orderStatus;
    }

    public void AddOrderLine(Guid bookId, string bookTitle, decimal price, int quantity)
    {
        _orderItems.Add(new OrderItem(bookId, bookTitle, price, quantity));
        TotalPrice = CalculateTotalPrice();
    }

    public decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var orderItem in _orderItems)
        {
            totalPrice += orderItem.Price * orderItem.Quantity;
        }
        return totalPrice;
    }

}