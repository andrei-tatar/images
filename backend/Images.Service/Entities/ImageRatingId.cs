using System;

namespace Images.Service.Entities
{
    public class ImageRatingId
    {
        public Guid ImageId { get; set; }
        public string UserId { get; set; }

        public override string ToString()
        {
            return UserId + '-' + ImageId;
        }
    }
}