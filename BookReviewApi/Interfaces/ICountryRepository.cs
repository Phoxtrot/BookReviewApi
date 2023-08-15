using BookReviewApi.Models;

namespace BookReviewApi.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int CountryId);
        bool CountryExists(int CountryId);
        ICollection<Author> GetAuthorsByCountry(int CountryId);
        Country GetAuthorCountry(int AuthorId);
        bool CreateCountry(Country country);
        bool Save();

    }
}
