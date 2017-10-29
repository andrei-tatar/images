using System;
using Common;

namespace Images.Service.Entities
{
    public class Image : IEntity<Guid>
    {
        public string Location { get; set; }
        public string[] Tags { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
