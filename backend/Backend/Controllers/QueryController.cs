using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Contracts.Queries;
using Images.Contracts.Queries;

namespace Backend.Controllers
{
    public class QueryController : BaseController
    {
        [HttpGet]
        public async Task<ListImagesResponse> ListImages(int page, int pageSize)
        {
            var result = await Mediator.Send(new ListImages(page, pageSize, User.Identity.Name));
            return new ListImagesResponse
            {
                Images = result.Select(listImage => new ListImagesResponse.ListImage
                {
                    Id = listImage.Id,
                    Link = $"image/{listImage.Id}",
                    UserId = listImage.UserId,
                    Date = listImage.Date,
                    AverageRating = listImage.AverageRating,
                    UserRating =  listImage.UserRating,
                    Location = listImage.Location,
                    Comments = listImage.Comments.Select(listComment => new ListImagesResponse.ListImage.ListImageComment
                    {
                        Id = listComment.Id,
                        CommentText = listComment.CommentText,
                        Date = listComment.Date,
                        UserId = listComment.UserId,
                    })
                }),
            };
        }

        [HttpGet]
        public async Task<ListImageCommentsResponse> ListImageComments(Guid imageId)
        {
            var result = await Mediator.Send(new ListImageComments(imageId));
            return new ListImageCommentsResponse
            {
                Comments = result.Select(listComment => new ListImageCommentsResponse.Comment
                {
                    Id = listComment.Id,
                    CommentText = listComment.CommentText,
                    Date = listComment.Date,
                    UserId = listComment.UserId,
                })
            };
        }

        [HttpGet]
        public async Task<double?> GetImageAverageRating(Guid imageId)
        {
            return await Mediator.Send(new GetImageAverageRating(imageId));
        }
    }
}