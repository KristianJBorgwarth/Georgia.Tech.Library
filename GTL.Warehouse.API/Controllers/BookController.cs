using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using GTL.Warehouse.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using GTL.Warehouse.Persistence.Entities;
using MassTransit;
using GTL.Messaging.RabbitMq.Producer;
using GTL.Warehouse.API.Messages.BookCreatedMessage;
using GTL.Warehouse.Persistence.Repositories;
using System.Text.Json.Serialization;


namespace GTL.Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //private readonly IProducer<BookCreatedMessage> _producer;
        private readonly IBookRepository _repository;

        public BookController(/*IProducer<BookCreatedMessage> producer,*/ IBookRepository repository)
        {
           // _producer = producer;
            _repository = repository;
        }



        // GET: api/books
        [HttpGet("all")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _repository.GetAllBooks();
            return Ok(books);
        }
        // GET: specific book



        [HttpGet("bookid/{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
           var book = await _repository.GetBooksByUserIdAsync(id);
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
                return BadRequest(ModelState);
            }

            newBook.Id = Guid.NewGuid();
            await _repository.AddAsync(newBook);

           /* var corelationId = Guid.NewGuid();

            var message = new BookCreatedMessage(
                newBook.Title,
                newBook.Price,
                corelationId);

            await _producer.PublishMessageAsync(message); */

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        // TODO: New endpoint to GET on a single word, get top 10 finds



        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetBookByFullTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) { return BadRequest(new { Message = "Title must be provided" }); }

            var books = await _repository.GetBookByTitleAsync(title);
            if (books == null || books.Count == 0)
            {
                return NotFound(new { Message = $"No books found with the title conatining '{title}.'" });
            }
            return Ok(books);
                    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookOfUser(Guid id)
        {
            await _repository.DeleteBookWithUserIdAsync(id);
            return Ok();
        }
        [HttpGet("userId/{id}")]
        public async Task<IActionResult> GetBooksByUserId(Guid userId)
        {
            var books = await _repository.GetBooksByUserIdAsync(userId);
            return Ok(books);
        }
    }
}