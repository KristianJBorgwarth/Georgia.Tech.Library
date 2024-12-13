using GTL.Warehouse.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTL.Warehouse.Persistence.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book? book);
        Task<Book?> GetBookByBookIdAsync(Guid bookId);
        Task<List<Book?>> GetBookByTitleAsync(string title);
        Task<List<Book?>> GetAllBooks();
        Task DeleteBookWithUserIdAsync(Guid userID);
        Task<List<Book?>> GetBooksByUserIdAsync(Guid id);
    }
}
