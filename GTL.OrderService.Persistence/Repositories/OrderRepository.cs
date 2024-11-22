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

    public async Task AddAsync(Order? order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Orders.FindAsync(id);
    }
}