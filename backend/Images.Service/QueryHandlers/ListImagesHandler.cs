using System.Linq;
using System.Threading.Tasks;
using Common;
using Images.Contracts.Queries;
using Images.Service.Entities;
using MediatR;

namespace Images.Service.QueryHandlers
{
    public class ListImagesHandler : IAsyncRequestHandler<ListImages, ListImage[]>
    {
        private readonly IStore<Image> _imageStore;

        public ListImagesHandler(IStore<Image> imageStore)
        {
            _imageStore = imageStore;
        }

        public Task<ListImage[]> Handle(ListImages message)
        {
            return Task.FromResult(_imageStore.Items
                .Skip(message.Page*message.PageSize)
                .Take(message.PageSize)
                .Select(i =>
                    new ListImage
                    {
                        Id = i.Id,
                    })
                .ToArray());
        }
    }
}
