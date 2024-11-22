using GTL.OrderService.Persistence.Entities;
using GTL.OrderService.Persistence.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GTL.OrderService.Persistence.Context;

public class OrderServiceDbContext : DbContext
{
    public DbSet<Order?> Orders { get; private set; }

    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new OrderModelConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}