using BookReviewApi.Models;

namespace BookReviewApi.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        bool ReviewerExists(int id);
        ICollection<Review> GetAllReviewsByReviewer(int reviewerId);

    }
}
