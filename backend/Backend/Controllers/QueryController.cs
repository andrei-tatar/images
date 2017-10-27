﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Contracts.Queries;
using Images.Contracts.Queries;

namespace Backend.Controllers
{
    public class QueryController : BaseController
    {
        [HttpGet]
        public async Task<ListImagesResponse> ListImages(int page, int pageSize)
        {
            var result = await Mediator.Send(new ListImages(page, pageSize));
            return new ListImagesResponse
            {
                Images = result.Select(s => new ListImagesResponse.ListImage
                {
                    Id = s.Id,
                    Link = $"image/{s.Id}",
                    UserId = s.UserId,
                    Date = s.Date,
                }),
            };
        }
    }
}