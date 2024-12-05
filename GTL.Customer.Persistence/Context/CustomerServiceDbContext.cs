using Microsoft.EntityFrameworkCore;

namespace GTL.Customer.Persistence.Context;

public class CustomerServiceDbContext : DbContext
{
    public DbSet<Domain.Aggregates.Customer> Customers { get; private set; }

    public CustomerServiceDbContext(DbContextOptions<CustomerServiceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerServiceDbContext).Assembly);
    }
}