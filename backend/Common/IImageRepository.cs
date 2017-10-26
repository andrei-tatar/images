using System;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    public interface IImageRepository
    {
        Task Save(Guid id, Stream image);
        Task<Stream> Load(Guid id);
    }
}
