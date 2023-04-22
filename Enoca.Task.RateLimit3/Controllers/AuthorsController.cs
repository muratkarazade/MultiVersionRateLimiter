using Enoca.Task.RateLimit3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Enoca.Task.RateLimit3;
using Microsoft.AspNetCore.Cors;

namespace Enoca.Task.RateLimit3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class AuthorsController : ControllerBase
    {
        private static List<Author> authors = new List<Author>
    {
        new Author { Id = 1, Name = "Author 1", Email = "author1@example.com" },
        new Author { Id = 2, Name = "Author 2", Email = "author2@example.com" },
        new Author { Id = 3, Name = "Author 3", Email = "author3@example.com" }
    };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]        
        public IActionResult Post(Author author)
        {
            author.Id = authors.Max(a => a.Id) + 1;
            authors.Add(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Author author)
        {
            var existingAuthor = authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            existingAuthor.Name = author.Name;
            existingAuthor.Email = author.Email;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingAuthor = authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            authors.Remove(existingAuthor);
            return NoContent();
        }
    }
}
