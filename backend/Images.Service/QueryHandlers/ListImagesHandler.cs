using System.Linq;
using System.Threading.Tasks;
using Common;
using Images.Contracts.Queries;
using Images.Service.Entities;
using MediatR;

namespace Images.Service.QueryHandlers
{
    public class ListImagesHandler : IAsyncRequestHandler<ListImages, ListImages.Image[]>
    {
        private readonly IStore<Image> _imageStore;
        private readonly IStore<Comment> _commentsStore;

        public ListImagesHandler(IStore<Image> imageStore, IStore<Comment> commentsStore)
        {
            _imageStore = imageStore;
            _commentsStore = commentsStore;
        }

        public Task<ListImages.Image[]> Handle(ListImages message)
        {
            return Task.FromResult(_imageStore.Items
                .Skip(message.Page * message.PageSize)
                .Take(message.PageSize)
                .OrderBy(i => i.Date)
                .Select(i => new ListImages.Image
                {
                    Id = i.Id,
                    UserId = i.UserId,
                    Date = i.Date,
                })
                .Do(image =>
                {
                    image.Comments = _commentsStore
                        .Items
                        .Where(c => c.ImageGuid == image.Id)
                        .OrderBy(c => c.Date)
                        .Select(
                            s => new ListImages.Image.Comment
                            {
                                Id = s.Id,
                                CommentText = s.CommentText,
                                Date = s.Date,
                                UserId = s.UserId,
                            }).ToArray();
                })
                .ToArray());
        }
    }
}
