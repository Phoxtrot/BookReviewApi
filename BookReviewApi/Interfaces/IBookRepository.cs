using BookReviewApi.Models;

namespace BookReviewApi.Interfaces
{
    public interface IBookRepository
    {
        ICollection<Book> GetBooks();
        Book GetBook(int id);
        Book GetBook(string name);
        decimal GetBookRating (int id);
        bool BookExists(int id);
        bool Createbook(int authorId, int CategoryId, Book book);
        bool Save();

    }
}
