using GTL.OrderService.Persistence.Abstractions;
using GTL.OrderService.Persistence.Context;
using GTL.OrderService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace GTL.OrderService.Persistence.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly OrderServiceDbContext _dbContext;

    public OrderItemRepository(OrderServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(OrderItem item)
    {
        await _dbContext.OrderItems.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _dbContext.OrderItems.Where(o => o.Id == id).ExecuteDeleteAsync();

    }

    public async Task<IEnumerable<OrderItem>> GetAll()
    {
        return await _dbContext.OrderItems.ToListAsync();
    }

    public async Task<IEnumerable<OrderItem>> GetAllItemsFromOrder(Guid id)
    {
        return await _dbContext.OrderItems.Where(o => o.OrderId == id).ToListAsync();

    }

    public async Task<OrderItem> GetByIdAsync(Guid id)
    {
        return await _dbContext.OrderItems.FindAsync(id);

    }

    public async Task UpdateAsync(OrderItem item)
    {
        _dbContext.OrderItems.Update(item);
        await _dbContext.SaveChangesAsync();
    }
}