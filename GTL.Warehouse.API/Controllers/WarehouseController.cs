using Georgia.Tech.Library.Context;
using Georgia.Tech.Library.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint


    public WarehouseController(WarehouseDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }



    [HttpGet("items")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }

    [HttpGet("items/{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var item = await _context.Books.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return item;
    }

    [HttpPost("items")]
    public async Task<ActionResult<Book>> AddStockItem(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new AddedBookMessage
        {
            BookId = book.id,
            Title = book.title,
            Quantity = book.quantity

        });

        return CreatedAtAction(nameof(GetBook), new { id = book.id }, book);
    }

    [HttpPut("items/{id}")]
    public async Task<IActionResult> UpdateBook(Guid id, Book book)
    {
        if (id != book.id) return BadRequest();

        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("items/{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        // Retrieve the book from the database
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound(); 
        }

        // Remove the book
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }

};
