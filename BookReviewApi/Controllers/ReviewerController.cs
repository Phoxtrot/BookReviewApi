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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReviewer([FromBody] CreateReviewerDto createReviewer)
        {
            if (createReviewer == null)
            {
                return BadRequest(ModelState);
            }
            var reviewers = _reviewerRepository.GetReviewers().Where(r=>r.FirstName.Trim().ToUpper() == createReviewer.FirstName.Trim().ToUpper() && r.LastName.Trim().ToUpper()==createReviewer.LastName.Trim().ToUpper()).FirstOrDefault();
            if (reviewers != null)
            {
                ModelState.AddModelError("", "Reviewer Name already exists");
                return StatusCode(422, ModelState);
            }
            var reviewerMap = _mapper.Map<Reviewer>(createReviewer);
            if (!_reviewerRepository.CreateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesful");

        }


        [HttpPut("{ReviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int ReviewerId, [FromBody] UpdateReviewerDto updateReviewer)
        {
            if (updateReviewer == null)
            {
                return BadRequest(ModelState);
            }
            if (updateReviewer.Id != ReviewerId)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewerRepository.ReviewerExists(updateReviewer.Id))
            {
                return NotFound();
            }
            var reviewerMap = _mapper.Map<Reviewer>(updateReviewer);
            if (!_reviewerRepository.UpdateReviewer(reviewerMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ReviewerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int ReviewerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_reviewerRepository.ReviewerExists(ReviewerId))
            {
                return NotFound();
            }
            var deletedBook = _reviewerRepository.GetReviewer(ReviewerId);
            if (!_reviewerRepository.DeleteReviewer(deletedBook))
            {
                ModelState.AddModelError("", "Soemthing went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
