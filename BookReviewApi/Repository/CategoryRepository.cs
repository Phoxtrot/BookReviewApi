using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private  DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public bool CategoryExists(int id)
        {
            return _dataContext.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);
            return Save();
        }

        public ICollection<Book> GetBooksByCategory(int categoryId)
        {
            return _dataContext.BookCategories.Where(c=>c.CategoryId == categoryId).Select(b=>b.Book).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.OrderBy(c=>c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
