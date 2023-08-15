using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Review> GetBookReviews(int bookId)
        {
            return _dataContext.Reviews.Where(b=>b.Book.Id==bookId).ToList();
        }

        public Review GetReview(int id)
        {
            return _dataContext.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(r => r.Id == id);  
        }
    }
}
