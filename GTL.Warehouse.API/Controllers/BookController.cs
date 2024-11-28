using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GTL.Warehouse.Data;
using GTL.Warehouse.Models.Book;
using Microsoft.EntityFrameworkCore;


namespace GTL.Warehouse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly WarehouseDbContext _context;

        public BooksController(WarehouseDbContext context)
        {
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return Ok(books);
        }
        // GET: specific book
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }
            return Ok(book);
        }
    }
}
