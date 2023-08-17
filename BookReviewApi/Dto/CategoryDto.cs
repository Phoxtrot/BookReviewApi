namespace BookReviewApi.Dto
{
    public record CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public record CreateCategoryDto
    {
        public string Name { get; set; }
    }
    public record UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
