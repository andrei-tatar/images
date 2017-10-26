using System;
using System.IO;
using MediatR;

namespace Images.Contracts.Commands
{
    public class UploadImage : IRequest
    {
        public UploadImage(Stream image, string[] tags, string description, DateTime date, string location)
        {
            Image = image;
            Tags = tags;
            Description = description;
            Date = date;
            Location = location;
            ImageGuid = Guid.NewGuid();
        }

        public Guid ImageGuid { get; }
        public Stream Image { get; }
        public string[] Tags { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public string Location { get; }
    }
}
