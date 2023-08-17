using BookReviewApi.Models;

namespace BookReviewApi.Dto
{
    public record ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }
    }
    public record CreateReviewDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
    public record UpdateReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
    }
}
