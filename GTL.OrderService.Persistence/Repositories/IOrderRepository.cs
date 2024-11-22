using GTL.OrderService.Persistence.Entities;

namespace GTL.OrderService.Persistence.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order? order);
    Task<Order?> GetByIdAsync(Guid id);
}