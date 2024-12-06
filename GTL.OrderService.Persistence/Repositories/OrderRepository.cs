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

    public async Task DeleteAsync(Guid id)
    {
        await _dbContext.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
       return await _dbContext.Orders.Include(x=> x.OrderItems).FirstOrDefaultAsync(x=> x.Id == id);
    }

    public async Task UpdateAsync(Order entity)
    {
        _dbContext.Orders.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}