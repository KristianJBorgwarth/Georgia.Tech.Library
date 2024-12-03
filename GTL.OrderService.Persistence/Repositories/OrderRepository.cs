using GTL.OrderService.Persistence.Context;
using GTL.OrderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GTL.OrderService.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderServiceDbContext _dbContext;

    public OrderRepository(OrderServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task CreateAsync(Order entity)
    {
        await _dbContext.Orders.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Orders.FindAsync(id);
    }

    public async Task UpdateAsync(Order entity)
    {
        await _dbContext.SaveChangesAsync();
    }
}