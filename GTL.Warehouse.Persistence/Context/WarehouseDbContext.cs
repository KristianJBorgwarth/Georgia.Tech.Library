using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities.Book;



namespace GTL.Warehouse.Persistence.Context
{

    public class WarehouseDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id); // Primary Key
                entity.Property(b => b.Title)
                      .IsRequired(); // Required
                entity.Property(b => b.Quantity)
                      .IsRequired(); // Required
                entity.Property(b => b.Price)
                      .IsRequired(); // Required
            });
        }
    }
}
