using BookReviewApi.Models;

namespace BookReviewApi.Interfaces
{
    public interface IReviewRepository
    {
        Review GetReview(int id);
        bool ReviewExists(int id);
        ICollection<Review> GetBookReviews(int bookId);
    }
}
