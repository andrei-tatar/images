using Common;
using Images.Contracts.Commands;
using Images.Contracts.Queries;
using Images.Service.CommandHandlers;
using Images.Service.QueryHandlers;
using MediatR;
using Unity;

namespace Images.Service
{
    public static class ImagesModule
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterCommandHandler<UploadImage, UploadImageHandler, UploadImageValidator>();
            container.RegisterQueryHandler<ListImages, ListImage[], ListImagesHandler>();
        }
    }
}
