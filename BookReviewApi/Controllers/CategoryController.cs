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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            return Ok(categories);
        }

        [HttpGet("Id")]
        [ProducesResponseType(200,Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(id));
            return Ok(category);
        }

        [HttpGet("id/books")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        [ProducesResponseType(400)]
        public IActionResult GetBooksByCategory(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var books = _mapper.Map<List<BookDto>>(_categoryRepository.GetBooksByCategory(id));
            return Ok(books);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult CreateCategory([FromBody] CreateCategoryDto categoryCreate)
        {
            if (categoryCreate ==null)
            {
                return BadRequest(ModelState);
            }
            var category = _categoryRepository.GetCategories().Where(c=>c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper()).FirstOrDefault();
            if (category!=null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }
            var categoryMap = _mapper.Map<Category>(categoryCreate);
            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Category cannot be created");
                return StatusCode(500,ModelState);
            }
            return Ok("Sucessful");
        }


        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int CategoryId, UpdateCategoryDto updateCategory)
        {
            if (updateCategory == null)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }
            if (updateCategory.Id != CategoryId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryMap = _mapper.Map<Category>(updateCategory);
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Someting went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully Updated");
        }


        [HttpDelete("{CategoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int CategoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_categoryRepository.CategoryExists(CategoryId))
            {
                return NotFound();
            }
            var deletedBook = _categoryRepository.GetCategory(CategoryId);
            if (!_categoryRepository.DeleteCategory(deletedBook))
            {
                ModelState.AddModelError("", "Soemthing went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
