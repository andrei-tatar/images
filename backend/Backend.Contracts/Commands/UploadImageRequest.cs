using System;

namespace Backend.Contracts.Commands
{
    public class UploadImageRequest
    {
        public string[] Tags { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }

    public class AddCommentRequest
    {
        public Guid ImageId { get; set; }
        public string Comment { get; set; }
    }
}
