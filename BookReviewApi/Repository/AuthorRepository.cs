using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DataContext _dataContext;

        public AuthorRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool AuthorExists(int id)
        {
            return _dataContext.Authors.Any(a => a.Id == id);
        }

        public Author GetAuthor(int id)
        {
            return _dataContext.Authors.Where(a => a.Id == id).FirstOrDefault(); 
        }

        public ICollection<Author> GetAuthors()
        {
            return _dataContext.Authors.OrderBy(a=>a.Id).ToList();
        }
        public ICollection<Book> GetAuthorBooks(int id)
        {
            return _dataContext.BookAuthors.Where(a=>a.Author.Id == id).Select(b=>b.Book).ToList();
        }

        public Author GetBookAuthor(int id)
        {
            return _dataContext.BookAuthors.Where(b => b.Book.Id == id).Select(b => b.Author).FirstOrDefault();
        }

        public bool CreateAuthor(Author author)
        {
            _dataContext.Authors.Add(author);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAuthor(Author author)
        {
            _dataContext.Update(author);
            return Save();
        }

        public bool DeleteAuthor(Author author)
        {
            _dataContext.Remove(author);
            return Save();
        }
    }
}
