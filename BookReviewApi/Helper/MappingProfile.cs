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
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CreateCountryDto, Country>();
            CreateMap<UpdateCountryDto, Country>();
            CreateMap<Author, AuthorDto>();
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<CreateReviewerDto, Reviewer>();
            CreateMap<UpdateReviewerDto, Reviewer>();
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
        }
    }
}
