using Microsoft.EntityFrameworkCore;

namespace GTL.Customer.Persistence.Context;

public class CustomerDbContext : DbContext
{
    public DbSet<Domain.Aggregates.Customer> Customers { get; private set; }
    
    public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
    }
}