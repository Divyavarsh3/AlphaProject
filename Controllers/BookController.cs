
using Alpha.Common.Models;

using Alpha.Service.Interfaces;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Alpha.API.Controllers
{
    [Authorize]

    [Route("api/[controller]")]

    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET ALL BOOKS WITH PAGINATION
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetBooks(
        int pageNumber = 1,
        int pageSize = 5)
        {
            try
            {
                var result =
                await _bookService.GetBooks(
                    pageNumber,
                    pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET BOOK BY ID
        [Authorize(Roles = "Admin")]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var result =
                await _bookService.GetBookById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // INSERT BOOK - ADMIN ONLY
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> InsertBook(Books book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _bookService.InsertBook(book);

                return Ok("Book Inserted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE BOOK - ADMIN ONLY
        [Authorize(Roles = "Admin")]

        [HttpPut]
        public async Task<IActionResult> UpdateBook(Books book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _bookService.UpdateBook(book);

                return Ok("Book Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE BOOK - ADMIN ONLY
        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _bookService.DeleteBook(id);

                return Ok("Book Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

