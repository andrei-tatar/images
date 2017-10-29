using System.Threading.Tasks;
using Common;
using Images.Contracts.Commands;
using Images.Service.Entities;

namespace Images.Service.CommandHandlers
{
    public class RateImageHandler : IHandle<RateImage>
    {
        private readonly IStore<ImageRating, ImageRatingId> _imageRatingStore;

        public RateImageHandler(IStore<ImageRating, ImageRatingId> imageRatingStore)
        {
            _imageRatingStore = imageRatingStore;
        }

        public async Task Handle(RateImage command)
        {
            await _imageRatingStore.Add(new ImageRating
            {
                Id = new ImageRatingId
                {
                    ImageId = command.ImageId,
                    UserId = command.UserId,
                },
                Rate = command.Rate,
            });
        }
    }
}
