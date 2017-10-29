using System;
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
        private readonly IStore<Image, Guid> _imageStore;
        private readonly IStore<Comment, Guid> _commentsStore;
        private readonly IStore<ImageRating, ImageRatingId> _imageRatingsStore;

        public ListImagesHandler(
            IStore<Image, Guid> imageStore,
            IStore<Comment, Guid> commentsStore,
            IStore<ImageRating, ImageRatingId> imageRatingsStoreStore)
        {
            _imageStore = imageStore;
            _commentsStore = commentsStore;
            _imageRatingsStore = imageRatingsStoreStore;
        }

        public Task<ListImages.Image[]> Handle(ListImages message)
        {
            var query = _imageStore.Items;
            switch (message.SortBy)
            {
                case ListImages.ImageSortBy.Date:
                    query = query.OrderBy(i => i.Date);
                    break;
                case ListImages.ImageSortBy.Location:
                    query = query.OrderBy(i => i.Location);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Task.FromResult(query
                .Skip(message.Page * message.PageSize)
                .Take(message.PageSize)
                .Select(image => new ListImages.Image
                {
                    Id = image.Id,
                    UserId = image.UserId,
                    Date = image.Date,
                    Location = image.Location,
                    Comments = _commentsStore
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
                            }).ToArray(),
                })
                .Do(image =>
                {
                    var imageRatings = _imageRatingsStore.Items.Where(r => r.Id.ImageId == image.Id).ToArray();
                    var userRating = imageRatings.SingleOrDefault(r => r.Id.UserId == message.UserId);

                    image.UserRating = userRating?.Rate;
                    image.AverageRating = imageRatings.Length == 0
                        ? (double?) null
                        : imageRatings.Select(s => s.Rate).Average();
                })
                .ToArray());
        }
    }
}
