﻿using BookReviewApi.Models;
using Microsoft.EntityFrameworkCore;

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
                .HasForeignKey(b=>b.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(c => c.Category)
               .WithMany(bc => bc.BookCategories)
               .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });
            modelBuilder.Entity<BookAuthor>().HasOne(b=>b.Book)
                .WithMany(ba=>ba.BookAuthors)
                .HasForeignKey(b=>b.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(a=>a.Author)
                .WithMany(ba=>ba.BookAuthors)
                .HasForeignKey(a=>a.AuthorId);
        }

    }
}
