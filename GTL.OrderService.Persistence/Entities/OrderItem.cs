
using GTL.OrderService.Persistence.Abstractions;
using System.ComponentModel;

namespace GTL.OrderService.Persistence.Entities;

public class OrderItem : Entity
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid BookId { get; private set; }
    public string BookTitle { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem(Guid bookId, string bookTitle, decimal price, int quantity)
    {
        BookId = bookId;
        BookTitle = bookTitle;
        Price = price;
        Quantity = quantity;
    }

}