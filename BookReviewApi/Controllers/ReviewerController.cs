using AutoMapper;
using BookReviewApi.Dto;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            return Ok(reviewers);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int id)
        {
            if (!_reviewerRepository.ReviewerExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(id));
            return Ok(reviewer);
        }

        [HttpGet("id/reviews")]
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetAllReviewByReviewers (int id)
        {
            if (!_reviewerRepository.ReviewerExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var reviews = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetAllReviewsByReviewer(id));
            return Ok(reviews);
        }
    }
}
