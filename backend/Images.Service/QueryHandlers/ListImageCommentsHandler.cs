using System.Linq;
using System.Threading.Tasks;
using Common;
using Images.Contracts.Queries;
using Images.Service.Entities;
using MediatR;

namespace Images.Service.QueryHandlers
{
    public class ListImageCommentsHandler : IAsyncRequestHandler<ListImageComments, ListImageComments.Comment[]>
    {
        private readonly IStore<Comment> _commentsStore;

        public ListImageCommentsHandler(IStore<Comment> commentsStore)
        {
            _commentsStore = commentsStore;
        }

        public Task<ListImageComments.Comment[]> Handle(ListImageComments message)
        {
            return Task.FromResult(_commentsStore.Items
                .Where(c => c.ImageGuid == message.ImageId)
                .OrderBy(c => c.Date)
                .Select(
                    s => new ListImageComments.Comment
                    {
                        Id = s.Id,
                        CommentText = s.CommentText,
                        Date = s.Date,
                        UserId = s.UserId,
                    }).ToArray());
        }
    }
}
