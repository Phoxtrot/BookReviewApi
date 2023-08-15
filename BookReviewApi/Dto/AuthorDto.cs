namespace BookReviewApi.Dto
{
    public record AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Institution { get; set; }
    }
    public record CreateAuthorDto
    {
        public string Name { get; set; }
        public string Institution { get; set; }
    }
}
