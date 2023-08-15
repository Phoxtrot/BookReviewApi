using AutoMapper;
using BookReviewApi.Dto;
using BookReviewApi.Models;

namespace BookReviewApi.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateBookDto, Book>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<Review, ReviewDto>();
        }
    }
}
