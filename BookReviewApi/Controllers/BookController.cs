using BookReviewApi.Interfaces;
using BookReviewApi.Models;
using BookReviewApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace BookReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult getBooks()
        {
            var books = _bookRepository.GetBooks();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(books);

        }
        [HttpGet("Id")]
        [ProducesResponseType(200, Type =typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult getBook(int id)
        {
            if (!_bookRepository.BookExists(id))
            {
                return NotFound();
            }
            var book = _bookRepository.GetBook(id);
            if (!ModelState.IsValid)
            {return BadRequest(ModelState);
            }
            return Ok(book);
        }
        [HttpGet("Id/Rating")]
        [ProducesResponseType(200, Type=typeof(Decimal))]
        public IActionResult getBookRating(int id)
        {
            if (!_bookRepository.BookExists(id))
            {
                return NotFound();
            }
            var bookRating = _bookRepository.GetBookRating(id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(bookRating);

        }
    }
}
