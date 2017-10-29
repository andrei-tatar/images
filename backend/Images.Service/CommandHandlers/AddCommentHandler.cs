using System;
using System.Threading.Tasks;
using Common;
using Images.Contracts.Commands;
using Images.Service.Entities;

namespace Images.Service.CommandHandlers
{
    public class AddCommentHandler : IHandle<AddComment>
    {
        private readonly IStore<Comment, Guid> _commentsStore;

        public AddCommentHandler(IStore<Comment, Guid> commentsStore)
        {
            _commentsStore = commentsStore;
        }

        public async Task Handle(AddComment command)
        {
            await _commentsStore.Add(new Comment
            {
                Id = command.Id,
                ImageGuid = command.ImageId,
                UserId = command.UserId,
                Date = command.Date,
                CommentText = command.Comment,
            });
        }
    }
}
