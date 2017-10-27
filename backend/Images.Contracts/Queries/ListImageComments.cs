using System;
using MediatR;

namespace Images.Contracts.Queries
{
    public class ListImageComments : IRequest<ListImageComments.Comment[]>
    {
        public Guid ImageId { get; }

        public ListImageComments(Guid imageId)
        {
            ImageId = imageId;
        }

        public class Comment
        {
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public DateTime Date { get; set; }
            public string CommentText { get; set; }
        }
    }
}