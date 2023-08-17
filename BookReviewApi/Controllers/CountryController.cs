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
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(IEnumerable<Country>))]
        [ProducesResponseType(400)]
        public IActionResult GetCountries()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());
            return Ok(countries);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetCountry(int id)
        {
            if (!_countryRepository.CountryExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(id));
            return Ok(country);
        }


        [HttpGet("author/{id}")]
        [ProducesResponseType(200, Type = typeof(Country))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorCountry(int id)
        {
            if (!_countryRepository.CountryExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var country = _mapper.Map<CountryDto>(_countryRepository.GetAuthorCountry(id));
            return Ok(country);
        }

        [HttpGet("{id}/authors")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Author>))]
        [ProducesResponseType(400)]
        public IActionResult GetAuthorsByCountry(int id)
        {
            if (!_countryRepository.CountryExists(id))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var authors = _mapper.Map<List<AuthorDto>>(_countryRepository.GetAuthorsByCountry(id));
            return Ok(authors);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult CreateCountry(CreateCountryDto createCountry)
        {
            if (createCountry==null)
            {
                return BadRequest(ModelState);
            }
            var country = _countryRepository.GetCountries().Where(c=>c.Name.Trim().ToUpper()==createCountry.Name.TrimEnd().ToUpper()).FirstOrDefault();
            if (country!=null)
            {
                ModelState.AddModelError("", "Country already exist");
                return StatusCode(422, ModelState);
            }
            var countryMap = _mapper.Map<Country>(createCountry);
            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Couldn't create Country");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesful");
        }

        [HttpPut("{CountryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry(int CountryId, [FromBody]UpdateCountryDto updatedCountry)
        {
            if (updatedCountry == null)
            {
                return BadRequest(ModelState);
            }
            if (updatedCountry.Id != CountryId)
            {
                return BadRequest(ModelState);
            }
            if (!_countryRepository.CountryExists(updatedCountry.Id))
            {
                return NotFound();
            }
            var countryMap = _mapper.Map<Country>(updatedCountry);
            if (!_countryRepository.UpdateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{CountryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBook(int CountryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_countryRepository.CountryExists(CountryId))
            {
                return NotFound();
            }
            var deletedBook = _countryRepository.GetCountry(CountryId);
            if (!_countryRepository.DeleteCountry(deletedBook))
            {
                ModelState.AddModelError("", "Soemthing went wrong...");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
