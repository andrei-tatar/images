using System;
using System.Collections.Generic;
using MediatR;

namespace Images.Contracts.Queries
{
    public class ListImages : IRequest<ListImages.Image[]>
    {
        public ListImages(int page, int pageSize, string userId, ImageSortBy sortBy)
        {
            Page = page;
            PageSize = pageSize;
            UserId = userId;
            SortBy = sortBy;
        }

        public int Page { get; }
        public int PageSize { get; }
        public string UserId { get; }
        public ImageSortBy SortBy { get; }

        public enum ImageSortBy
        {
            Date,
            Location,
        }

        public class Image
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public DateTime Date { get; set; }
            public IEnumerable<Comment> Comments { get; set; }
            public double? AverageRating { get; set; }
            public int? UserRating { get; set; }
            public string Location { get; set; }
            public string[] Tags { get; set; }

            public class Comment
            {
                public Guid Id { get; set; }
                public string UserId { get; set; }
                public DateTime Date { get; set; }
                public string CommentText { get; set; }
            }
        }
    }
}
