using GTL.OrderService.Persistence.Entities;

namespace GTL.OrderService.Persistence.Repositories;

public interface IOrderRepository
{

    Task CreateAsync(Order entity);
    Task DeleteAsync(Guid id);
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAll();
    Task UpdateAsync(Order entity);
}