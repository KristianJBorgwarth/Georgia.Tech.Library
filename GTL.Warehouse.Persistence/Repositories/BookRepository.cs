using GTL.Warehouse.Persistence.Context;
using GTL.Warehouse.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Repositories
{
    public class BookRepository : IBookRepository

    {
        private readonly WarehouseDbContext _dbContext;

        public BookRepository(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task AddAsync(Book? book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            // Ensure the required relationships are satisfied
           /* if (book.SellerId == Guid.Empty)
                throw new InvalidOperationException("Book must be associated with a valid seller.");

            // Optional: Ensure related `BookDetails` is initialized
            if (book.BookDetails == null)
            {
                throw new InvalidOperationException("BookDetails must be provided.");
            }*/

            // Add the book to the context
            await _dbContext.Books.AddAsync(book);

            // Save changes
            await _dbContext.SaveChangesAsync();
        }




        public async Task<Book?> GetBookByBookIdAsync(Guid bookId)
        {
            return await _dbContext.Books
                .Include(b => b.BookDetails)
                .FirstOrDefaultAsync(b =>b.Id == bookId);
        }



        public async Task<List<Book?>> GetBookByTitleAsync(string title)
        {
            return await _dbContext.Books
          .Include(b => b.BookDetails) // Include BookDetails
          .Where(b => EF.Functions.Like(b.Title, $"%{title}%"))
          .ToListAsync();
        }


        public async Task<List<Book?>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }



       public async Task DeleteBookWithUserIdAsync(Guid userId)
        {
            var booksToDelete = await _dbContext.Books.Where(book => book.SellerId == userId).ToListAsync();

            if (booksToDelete.Any())
            {
                _dbContext.RemoveRange(booksToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<List<Book?>> GetBooksByUserIdAsync(Guid userId)
        {
            var books = await _dbContext.Books.Where(book => book.SellerId == userId).ToListAsync();
            return books;
        }
    }
}
