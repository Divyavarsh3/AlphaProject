using Alpha.Common.Models;
using Alpha.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET ALL BOOKS
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var result = await _bookService.GetBooks();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET BOOK BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var result = await _bookService.GetBookById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // INSERT BOOK
        [HttpPost]
        public async Task<IActionResult> InsertBook(Books book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _bookService.InsertBook(book);

                return Ok("Book Inserted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE BOOK
        [HttpPut]
        public async Task<IActionResult> UpdateBook(Books book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _bookService.UpdateBook(book);

                return Ok("Book Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE BOOK
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var result = await _bookService.DeleteBook(id);

                return Ok("Book Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}