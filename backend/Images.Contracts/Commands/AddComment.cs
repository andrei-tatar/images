using System;
using MediatR;

namespace Images.Contracts.Commands
{
    public class AddComment : IRequest
    {
        public AddComment(Guid imageId, string comment, string userId, DateTime date)
        {
            Id = Guid.NewGuid();
            ImageId = imageId;
            Comment = comment;
            UserId = userId;
            Date = date;
        }

        public Guid Id { get; }
        public Guid ImageId { get; }
        public string Comment { get; }
        public string UserId { get; }
        public DateTime Date { get; }
    }
}
