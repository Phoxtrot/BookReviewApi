using AutoMapper;
using BookReviewApi.Dto;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;
using BookReviewApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IBookRepository bookRepository, IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int ReviewerId, [FromQuery] int BookId, [FromBody] CreateReviewDto createReview)
        {
            if (createReview == null)
            {
                return BadRequest(ModelState);
            }
            var reviewMap = _mapper.Map<Review>(createReview);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(ReviewerId);
            reviewMap.Book = _bookRepository.GetBook(BookId);
            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessful");

        }
        [HttpPut("{ReviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int ReviewId, [FromBody] UpdateReviewDto updateReview)
        {
            if (updateReview == null)
            {
                return BadRequest(ModelState);
            }
            if (updateReview.Id != ReviewId)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(updateReview.Id))
            {
                return NotFound();
            }
            var reviewMap = _mapper.Map<Review>(updateReview);
            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ReviewId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int ReviewId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_reviewRepository.ReviewExists(ReviewId))
            {
                return NotFound();
            }
            var deletedBook = _reviewRepository.GetReview(ReviewId);
            if (!_reviewRepository.DeleteReview(deletedBook))
            {
                ModelState.AddModelError("", "Soemthing went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
