using System;
using MediatR;

namespace Images.Contracts.Queries
{
    public class ListImages : IRequest<ListImage[]>
    {
        public ListImages(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page { get; }
        public int PageSize { get; }
    }

    public class ListImage
    {
        public Guid Id { get; set; }
    }
}
