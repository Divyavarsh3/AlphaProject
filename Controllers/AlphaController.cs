using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alpha.Common.Models;

namespace Alpha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlphaController : ControllerBase
    {
        // Dummy Data
        private static List<Books> books = new List<Books>()
        {
            new Books
            {
                Id = 1,
                BookName = "C# Basics",
                Author = "Divya",
                Price = 500
            },

            new Books
            {
                Id = 2,
                BookName = "ASP.NET Core",
                Author = "Kumar",
                Price = 700
            }
        };

        // GET ALL BOOKS
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(books);
        }

        // GET BOOK BY ID
        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound("Book Not Found");
            }

            return Ok(book);
        }

        // ADD BOOK
        [HttpPost]
        public IActionResult AddBook(Books book)
        {
            books.Add(book);

            return Ok("Book Added Successfully");
        }

        // UPDATE BOOK BY ID
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Books updatedBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound("Book Not Found");
            }

            book.BookName = updatedBook.BookName;
            book.Author = updatedBook.Author;
            book.Price = updatedBook.Price;

            return Ok("Book Updated Successfully");
        }

        // DELETE BOOK BY ID
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound("Book Not Found");
            }

            books.Remove(book);

            return Ok("Book Deleted Successfully");
        }
    }
}