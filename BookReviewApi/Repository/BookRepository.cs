using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public bool BookExists(int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public Book GetBook(string name)
        {
            return _context.Books.Where(b => b.Name == name).FirstOrDefault();
        }

        public decimal GetBookRating(int id)
        {
            var review = _context.Reviews.Where(b=>b.Book.Id == id);
            if (review.Count() <=0)
            {
                return 0;
            }
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection <Book> GetBooks()
        {
            return _context.Books.OrderBy(b=>b.Id).ToList();
        }
    }
}
