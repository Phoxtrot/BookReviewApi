namespace BookReviewApi.Dto
{
    public record CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public record CreateCountryDto
    {
        public string Name { get; set; }
    }
    public record UpdateCountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
