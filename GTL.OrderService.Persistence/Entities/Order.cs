
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

    public List<Guid> GetBookIds()
    {
        return _orderItems.Select(x => x.BookId).ToList();
    }

    public decimal CalculateTotalPrice()
    {
        return _orderItems.Sum(orderItem => orderItem.Price);
    }

    public void SetOrderStatus(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }

}