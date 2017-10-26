using System;
using System.IO;
using System.Threading.Tasks;
using Common;

namespace FileBasedStorage
{
    public class FileBasedImageRepository : IImageRepository
    {
        private readonly string _path;

        public FileBasedImageRepository(string path)
        {
            _path = path;
            Utils.CreateDirectoryRecursive(_path);
        }

        public async Task Save(Guid id, Stream image)
        {
            var fileName = GetFileName(id);
            using (var fileStream = File.Create(fileName))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        public Task<Stream> Load(Guid id)
        {
            var fileName = GetFileName(id);
            return Task.FromResult<Stream>(File.OpenRead(fileName));
        }

        private string GetFileName(Guid id)
        {
            return Path.Combine(_path, id.ToString());
        }
    }
}