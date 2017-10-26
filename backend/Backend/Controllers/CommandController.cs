using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Contracts.Commands;
using Backend.Exceptions;
using Images.Contracts.Commands;
using Newtonsoft.Json;

namespace Backend.Controllers
{
    public class CommandController : BaseController
    {
        [HttpPost]
        public async Task<Guid> UploadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new BadRequestException();

            var provider = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
            var requestString = await provider.Contents
                .Single(s => s.Headers.ContentDisposition.Name == "\"request\"")
                .ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<UploadImageRequest>(requestString);

            var image = await (provider.Contents
                .Where(s => s.Headers.ContentDisposition.Name == "\"image\"")
                .Select(s => s.ReadAsStreamAsync())
                .SingleOrDefault() ?? Task.FromResult<Stream>(null));

            var command = new UploadImage(User.Identity.Name, image, request.Tags, request.Description, request.Date, request.Location);
            await Mediator.Send(command);
            return command.ImageGuid;
        }
    }
}