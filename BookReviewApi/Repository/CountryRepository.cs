using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _dataContext;

        public CountryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CountryExists(int CountryId)
        {
            return _dataContext.Countries.Any(c=>c.Id == CountryId);
        }

        public bool CreateCountry(Country country)
        {
            _dataContext.Countries.Add(country);
            return Save();
        }

        public bool DeleteCountry(Country country)
        {
           _dataContext.Countries.Remove(country);
            return Save();
        }

        public Country GetAuthorCountry(int AuthorId)
        {
            return _dataContext.Authors.Where(a => a.Id == AuthorId).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Country> GetCountries()
        {
            return _dataContext.Countries.OrderBy(c=>c.Id).ToList();
        }

        public Country GetCountry(int CountryId)
        {
            return _dataContext.Countries.Where(c => c.Id == CountryId).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _dataContext.Countries.Update(country);
            return Save();
        }

        ICollection<Author> ICountryRepository.GetAuthorsByCountry(int CountryId)
        {
            return _dataContext.Authors.Where(c => c.Country.Id == CountryId).ToList();
        }
    }
}
