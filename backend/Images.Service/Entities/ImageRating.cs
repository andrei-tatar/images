using Common;

namespace Images.Service.Entities
{
    public class ImageRating : IEntity<ImageRatingId>
    {
        public ImageRatingId Id { get; set; }
        public int Rate { get; set; }
    }
}
