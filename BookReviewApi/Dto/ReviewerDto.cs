using BookReviewApi.Models;

namespace BookReviewApi.Dto
{
    public record ReviewerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
    public record CreateReviewerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public record UpdateReviewerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
