using AutoMapper;
using BookReviewApi.Dto;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, ICountryRepository countryRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthors()
        {
            var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthors());
            return Ok(authors);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Author))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthor(int id)
        {
            if (!_authorRepository.AuthorExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(id));
            return Ok(author);
        }

        [HttpGet("id/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorBooks(int id)
        {
            if (!_authorRepository.AuthorExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = _mapper.Map<List<BookDto>>(_authorRepository.GetAuthorBooks(id));
            return Ok(books);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult CreateAuthor([FromQuery]int CountryId,[FromBody]CreateAuthorDto CreateAuthor)
        {
            if(CreateAuthor == null)
            {
                return BadRequest(ModelState);
            }
            var authors = _authorRepository.GetAuthors().Where(a=>a.Name.Trim().ToUpper() == CreateAuthor.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (authors!=null)
            {
                ModelState.AddModelError("", "Author already exists");
                return StatusCode(422, ModelState);
            }
            var authorMapper = _mapper.Map<Author>(CreateAuthor);
            authorMapper.Country = _countryRepository.GetCountry(CountryId);
            if (!_authorRepository.CreateAuthor(authorMapper))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesful");
        }
    }
}
