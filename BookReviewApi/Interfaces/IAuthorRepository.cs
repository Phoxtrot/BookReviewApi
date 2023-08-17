using BookReviewApi.Models;

namespace BookReviewApi.Interfaces
{
    public interface IAuthorRepository
    {
        ICollection<Author> GetAuthors();
        Author GetAuthor(int id);
        ICollection<Book> GetAuthorBooks(int id);
        bool AuthorExists(int id);
        Author GetBookAuthor(int id);
        bool CreateAuthor(Author author);
        bool UpdateAuthor(Author author);
        bool DeleteAuthor(Author author);
        bool Save();
    }
}
