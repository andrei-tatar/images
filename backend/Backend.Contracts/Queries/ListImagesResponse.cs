using System;
using System.Collections.Generic;

namespace Backend.Contracts.Queries
{
    public class ListImagesResponse
    {
        public IEnumerable<ListImage> Images { get; set; }

        public class ListImage
        {
            public Guid Id { get; set; }
            public string Link { get; set; }
        }
    }
}
