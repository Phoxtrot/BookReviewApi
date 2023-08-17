namespace BookReviewApi.Dto
{
    public record BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public record CreateBookDto
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public record UpdateBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
