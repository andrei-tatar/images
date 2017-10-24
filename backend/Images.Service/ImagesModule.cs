using Common;
using Images.Contracts.Commands;
using Images.Service.CommandHandlers;
using Unity;

namespace Images.Service
{
    public static class ImagesModule
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterCommandHandler<UploadImage, UploadImageHandler, UploadImageValidator>();
        }
    }
}
