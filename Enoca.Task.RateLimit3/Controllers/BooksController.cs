using Enoca.Task.RateLimit3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Enoca.Task.RateLimit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
    {
        new Book { Id = 1, Title = "Book 1", Author = "Author 1", Pages = 100 },
        new Book { Id = 2, Title = "Book 2", Author = "Author 2", Pages = 200 },
        new Book { Id = 3, Title = "Book 3", Author = "Author 3", Pages = 300 }
    };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Pages = book.Pages;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            books.Remove(existingBook);
            return NoContent();
        }
    }
}
