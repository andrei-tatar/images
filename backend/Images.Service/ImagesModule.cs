using Common;
using Images.Contracts.Commands;
using Images.Contracts.Queries;
using Images.Service.CommandHandlers;
using Images.Service.QueryHandlers;
using Unity;

namespace Images.Service
{
    public static class ImagesModule
    {
        public static void Register(IUnityContainer container)
        {
            container.RegisterCommandHandler<UploadImage, UploadImageHandler, UploadImageValidator>();
            container.RegisterCommandHandler<AddComment, AddCommentHandler, AddCommentValidator>();
            container.RegisterCommandHandler<RateImage, RateImageHandler, RateImageValidator>();
            container.RegisterQueryHandler<ListImages, ListImages.Image[], ListImagesHandler>();
            container.RegisterQueryHandler<ListImageComments, ListImageComments.Comment[], ListImageCommentsHandler>();
        }
    }
}
