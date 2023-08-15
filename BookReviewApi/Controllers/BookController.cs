using AutoMapper;
using BookReviewApi.Dto;
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
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult getBooks()
        {
            var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());
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
            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(id));
           
            if (!ModelState.IsValid)
            {return BadRequest(ModelState);
            }
            return Ok(book);
        }

        [HttpGet("Name")]
        [ProducesResponseType(200, Type=typeof(Book))]
        [ProducesResponseType(400)]
        public IActionResult getBookName(string name)
        {  
            var book = _mapper.Map<BookDto>(_bookRepository.GetBook(name));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult CreateBook([FromQuery] int CategoryId, [FromQuery] int AuthorId, [FromBody]CreateBookDto createBook)
        {
            if (createBook == null || CategoryId == null || AuthorId == null)
            {
                return BadRequest(ModelState);
            } 
            var book = _bookRepository.GetBooks().Where(b=>b.Name.Trim().ToUpper() == createBook.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (book != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(422, ModelState);
            }
            var bookMap = _mapper.Map<Book>(createBook);
            if (!_bookRepository.Createbook(AuthorId,CategoryId,bookMap))
            {
                ModelState.AddModelError("", "Error occured while creating...");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessful");
        }
    }
}
