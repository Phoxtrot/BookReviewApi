﻿using BookReviewApi.Data;
using BookReviewApi.Interfaces;
using BookReviewApi.Models;

namespace BookReviewApi.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _dataContext;

        public ReviewerRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<Review> GetAllReviewsByReviewer(int reviewerId)
        {
            return _dataContext.Reviews.Where(r=>r.Reviewer.Id == reviewerId).ToList();
        }

        public Reviewer GetReviewer(int id)
        {
            return _dataContext.Reviewers.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _dataContext.Reviewers.OrderBy(r=>r.Id).ToList();    
        }

        public bool ReviewerExists(int id)
        {
            return _dataContext.Reviewers.Any(r => r.Id == id);
        }
    }
}