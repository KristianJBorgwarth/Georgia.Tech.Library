using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities;
using GTL.Warehouse.Persistence.ModelConfigurations;



namespace GTL.Warehouse.Persistence.Context
{

    public class WarehouseDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookModelConfiguration());
            modelBuilder.ApplyConfiguration(new BookDetailsModelConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
