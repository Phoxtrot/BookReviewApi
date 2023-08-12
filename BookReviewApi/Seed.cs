using BookReviewApi.Data;
using BookReviewApi.Models;

namespace BookReviewApi
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.BookAuthors.Any())
            {
                var BookAuthors = new List<BookAuthor>()
                {
                    new BookAuthor()
                    {
                        Book = new Book()
                        {
                            Name = "Harry Potter",
                            DateCreated = new DateTime(1903,1,1),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Fiction"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Harry Potter",Text = "Harry Potter is the best book, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Harry Potter", Text = "Harry Potter is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Harry Potter",Text = "Harry, Harry, Poter", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Author = new Author()
                        {
                            Name = "JK. Rowlings",
                            Institution = "Havard",
                            Country = new Country()
                            {
                                Name = "England"
                            }
                        }
                    },
                    new BookAuthor()
                    {
                        Book = new Book()
                        {
                            Name = "Squirtle",
                            DateCreated = new DateTime(1903,1,1),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Science"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 4,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Author = new Author()
                        {
                            Name = "Sydney Sheldon",
                            Institution = "MIT",
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    },
                                    new BookAuthor()
                    {
                        Book = new Book()
                        {
                            Name = "Venasuar",
                            DateCreated = new DateTime(1903,1,1),
                            BookCategories = new List<BookCategory>()
                            {
                                new BookCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 3,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Author = new Author()
                        {
                            Name = "Ash",
                            
                            Institution = "Durhams",
                            Country = new Country()
                            {
                                Name = "USA"
                            }
                        }
                    }
                };
                dataContext.BookAuthors.AddRange(BookAuthors);
                dataContext.SaveChanges();
            }
        }
    }
}
