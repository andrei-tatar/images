using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Backend.Contracts.Commands;
using Images.Contracts.Commands;

namespace Backend.Controllers
{
    public class CommandController : BaseController
    {
        [HttpPost]
        public async Task<Guid> UploadImage(UploadImageRequest request)
        {
            //if (!Request.Content.IsMimeMultipartContent())
            //    throw new BadRequestException();

            //var provider = await Request.Content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider());
            //var requestString = await provider.Contents[0].ReadAsStringAsync();
            //var image = await provider.Contents[1].ReadAsStreamAsync();
            //var request = JsonConvert.DeserializeObject<UploadImageRequest>(requestString);

            var command = new UploadImage(null, request.Tags, request.Description, request.Date, request.Location);
            await Mediator.Send(command);
            return command.ImageGuid;
        }
    }
}