using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GTL.Warehouse.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities.Book;
using MassTransit;
using GTL.Messaging.RabbitMq.Producer;
using GTL.Warehouse.API.Messages.BookCreatedMessage;


namespace GTL.Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly WarehouseDbContext _context;
        private readonly IProducer<BookCreatedMessage> _producer;

        public BookController(WarehouseDbContext context, IProducer<BookCreatedMessage> producer)
        {
            _context = context;
            _producer = producer;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBook([FromBody] Book newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            newBook.Id = Guid.NewGuid();
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            var corelationId = Guid.NewGuid();

            var message = new BookCreatedMessage(
                newBook.Title,
                newBook.Quantity,
                newBook.Price,
                corelationId);

            await _producer.PublishMessageAsync(message);

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        // TODO: New endpoint to GET on a single word, get top 10 finds

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetBookByTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) { return BadRequest(new { Message = "Title must be provided" }); }

            var books = await _context.Books.
                Where(b => EF.Functions.Like(b.Title, $"%{title}")).ToListAsync();
            if (books == null || books.Count == 0)
            {
                return NotFound(new { Message = $"No Books found with the title containing '{title}'." });
            }
            return Ok(books);
        }
    }
}