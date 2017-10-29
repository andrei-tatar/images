using System;
using MediatR;

namespace Images.Contracts.Queries
{
    public class GetImageAverageRating : IRequest<double?>
    {
        public Guid ImageId { get; }

        public GetImageAverageRating(Guid imageId)
        {
            ImageId = imageId;
        }
    }
}