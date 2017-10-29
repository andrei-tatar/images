using System;
using System.Collections.Generic;

namespace Backend.Contracts.Queries
{
    public class ListImagesResponse
    {
        public IEnumerable<ListImage> Images { get; set; }

        public class ListImage
        {
            public Guid Id { get; set; }
            public string Link { get; set; }
            public string UserId { get; set; }
            public DateTime Date { get; set; }
            public IEnumerable<ListImageComment> Comments { get; set; }
            public double? AverageRating { get; set; }
            public int? UserRating { get; set; }
            public string Location { get; set; }

            public class ListImageComment
            {
                public Guid Id { get; set; }
                public string UserId { get; set; }
                public DateTime Date { get; set; }
                public string CommentText { get; set; }
            }
        }
    }
}
