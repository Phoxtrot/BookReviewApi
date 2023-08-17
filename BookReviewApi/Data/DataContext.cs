using BookReviewApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BookReviewApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookCategory>().HasOne(b => b.Book)
                .WithMany(bc=>bc.BookCategories)
                .HasForeignKey(b=>b.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookCategory>().HasOne(c => c.Category)
               .WithMany(bc => bc.BookCategories)
               .HasForeignKey(c => c.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(b=>b.Book)
                .WithMany(ba=>ba.BookAuthors)
                .HasForeignKey(b=>b.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<BookAuthor>().HasOne(a=>a.Author)
                .WithMany(ba=>ba.BookAuthors)
                .HasForeignKey(a=>a.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Country>().HasMany(c => c.Authors).WithOne(a => a.Country)
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reviewer>().HasMany(r => r.Reviews).WithOne(r=>r.Reviewer)
            .HasForeignKey(r=>r.ReviewerId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>().HasMany(r => r.Reviews).WithOne(r => r.Book)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
