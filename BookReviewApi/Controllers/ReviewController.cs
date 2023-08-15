using AutoMapper;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookReviews(int id)
        {
            if (!_bookRepository.BookExists(id))
            {
                return NotFound();
            }
            var reviews = _mapper.Map<List<Review>>(_reviewRepository.GetBookReviews(id));
            return Ok(reviews);
        }
        
    }
}
