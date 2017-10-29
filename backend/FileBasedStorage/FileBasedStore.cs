using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace FileBasedStorage
{
    public class FileBasedStore<T, TId> : IStore<T, TId> where T : class, IEntity<TId>
    {
        private readonly string _path;

        public FileBasedStore(string path)
        {
            _path = Path.Combine(path, typeof(T).Name + "-" + typeof(TId).Name);
            Utils.CreateDirectoryRecursive(_path);
        }

        public IQueryable<T> Items
        {
            get
            {
                return Directory.EnumerateFiles(_path, "*.json").Select(filePath =>
                {
                    try
                    {
                        var content = File.ReadAllText(filePath);
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }).Where(s => s != null).AsQueryable();
            }
        }

        public async Task Add(T item)
        {
            var fileName = GetFileName(item.Id);
            using (var writer = new StreamWriter(fileName))
            {
                var serialized = JsonConvert.SerializeObject(item);
                await writer.WriteAsync(serialized);
            }
        }

        public Task Delete(TId id)
        {
            var fileName = GetFileName(id);
            File.Delete(fileName);
            return Task.FromResult(true);
        }

        public Task Update(T item)
        {
            return Add(item);
        }

        private string GetFileName(TId id)
        {
            return Path.ChangeExtension(Path.Combine(_path, id.ToString()), "json");
        }
    }
}
