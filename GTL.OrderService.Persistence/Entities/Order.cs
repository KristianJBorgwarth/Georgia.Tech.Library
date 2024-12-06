
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
    public decimal TotalPrice { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public Order(Guid userId, OrderStatus orderStatus)
    {
        UserId = userId;
        OrderStatus = orderStatus;
    }

    public void AddOrderLine(Guid bookId, string bookTitle, decimal price, int quantity)
    {
        _orderItems.Add(new OrderItem(Id, bookId, bookTitle, price, quantity));
        TotalPrice = CalculateTotalPrice();
    }

    public void DeleteOrderItem(Guid orderItemId)
    {
        _orderItems.RemoveAll(x => x.Id == orderItemId);
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