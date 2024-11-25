namespace Georgia.Tech.Library.Context
{
    using Georgia.Tech.Library.Models;
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
