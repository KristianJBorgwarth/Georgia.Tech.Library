
using System.ComponentModel;

namespace GTL.OrderService.Persistence.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public int Quantity { get; private set; }
}