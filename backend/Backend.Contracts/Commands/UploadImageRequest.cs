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
}
