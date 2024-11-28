using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GTL.Warehouse.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities.Book;


namespace GTL.Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] Book newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            newBook.Id = Guid.NewGuid();
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookById), new {id = newBook.Id}, newBook);
        }
    }
}
