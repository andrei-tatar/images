using System.Linq;
using System.Threading.Tasks;
using Common;
using Images.Contracts.Queries;
using Images.Service.Entities;
using MediatR;

namespace Images.Service.QueryHandlers
{
    public class GetImageAverageRatingHandler : IAsyncRequestHandler<GetImageAverageRating, double?>
    {
        private readonly IStore<ImageRating, ImageRatingId> _imageRatingsStore;

        public GetImageAverageRatingHandler(
           IStore<ImageRating, ImageRatingId> imageRatingsStore)
        {
            _imageRatingsStore = imageRatingsStore;
        }

        public Task<double?> Handle(GetImageAverageRating message)
        {
            var imageRatings = _imageRatingsStore.Items.Where(r => r.Id.ImageId == message.ImageId).ToArray();
            var averageRating = imageRatings.Length == 0
                ? (double?)null
                : imageRatings.Select(s => s.Rate).Average();

            return Task.FromResult(averageRating);
        }
    }
}
