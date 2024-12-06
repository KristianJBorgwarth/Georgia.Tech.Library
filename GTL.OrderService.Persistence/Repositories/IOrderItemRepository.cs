using GTL.OrderService.Persistence.Entities;

namespace GTL.OrderService.Persistence.Repositories;

public interface IOrderItemRepository
{

    Task AddAsync(OrderItem item);
    Task<OrderItem> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderItem>> GetAll();
    Task UpdateAsync(OrderItem item);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<OrderItem>> GetAllItemsFromOrder(Guid Id);
}