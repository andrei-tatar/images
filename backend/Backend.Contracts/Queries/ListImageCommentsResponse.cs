using System;
using System.Collections.Generic;

namespace Backend.Contracts.Queries
{
    public class ListImageCommentsResponse
    {
        public IEnumerable<Comment> Comments { get; set; }

        public class Comment
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public DateTime Date { get; set; }
            public string CommentText { get; set; }
        }
    }
}