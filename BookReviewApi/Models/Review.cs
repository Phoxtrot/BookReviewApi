﻿namespace BookReviewApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Book Book { get; set; }
        public Reviewer Reviewer { get; set; }
    }
}
