using System;
using MediatR;

namespace Images.Contracts.Commands
{
    public class RateImage : IRequest
    {
        public Guid ImageId { get; }
        public int Rate { get; }
        public string UserId { get; }

        public RateImage(Guid imageId, int rate, string userId)
        {
            ImageId = imageId;
            Rate = rate;
            UserId = userId;
        }
    }
}