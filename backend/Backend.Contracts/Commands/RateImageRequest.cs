using System;

namespace Backend.Contracts.Commands
{
    public class RateImageRequest
    {
        public Guid ImageId { get; set; }
        public int Rate { get; set; }
    }
}