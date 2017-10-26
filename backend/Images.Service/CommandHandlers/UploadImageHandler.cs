using System.Threading.Tasks;
using Common;
using Images.Contracts.Commands;
using Images.Service.Entities;

namespace Images.Service.CommandHandlers
{
    public class UploadImageHandler : IHandle<UploadImage>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IStore<Image> _imageStore;

        public UploadImageHandler(IImageRepository imageRepository,
            IStore<Image> imageStore)
        {
            _imageRepository = imageRepository;
            _imageStore = imageStore;
        }

        public async Task Handle(UploadImage command)
        {
            await _imageRepository.Save(command.ImageGuid, command.Image);
            await _imageStore.Add(new Image
            {
                Id = command.ImageGuid,
                Location = command.Location,
                Tags = command.Tags,
                Date = command.Date,
                Description = command.Description,
                UserId = command.UserId,
            });
        }
    }
}
