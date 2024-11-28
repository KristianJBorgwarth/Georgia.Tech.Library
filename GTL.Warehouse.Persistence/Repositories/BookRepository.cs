using GTL.Warehouse.Persistence.Context;
using GTL.Warehouse.Persistence.Entities.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Repositories
{
    public class BookRepository
    {
        private readonly WarehouseDbContext _dbContext;

        public BookRepository(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Book? book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Books.FindAsync(id);
        }
    }
}
