using System.Threading.Tasks;
using Common;
using Images.Contracts.Commands;

namespace Images.Service.CommandHandlers
{
    public class UploadImageHandler : IHandle<UploadImage>
    {
        public async Task Handle(UploadImage command)
        {
        }
    }
}
