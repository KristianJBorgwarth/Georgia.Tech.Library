namespace GTL.WareHouse.Context
{
    using GTL.WareHouse.Models;
    using Microsoft.EntityFrameworkCore;

    public class WarehouseDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options)
        { 
        }
    }
}
