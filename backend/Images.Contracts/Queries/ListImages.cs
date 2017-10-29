using System;
using System.Collections.Generic;
using MediatR;

namespace Images.Contracts.Queries
{
    public class ListImages : IRequest<ListImages.Image[]>
    {
        public ListImages(int page, int pageSize, string userId)
        {
            Page = page;
            PageSize = pageSize;
            UserId = userId;
        }

        public int Page { get; }
        public int PageSize { get; }
        public string UserId { get; }

        public class Image
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public DateTime Date { get; set; }
            public IEnumerable<Comment> Comments { get; set; }
            public double? AverageRating { get; set; }
            public int? UserRating { get; set; }

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
