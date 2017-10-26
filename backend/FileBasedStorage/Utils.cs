using System.IO;

namespace FileBasedStorage
{
    internal static class Utils
    {
        public static void CreateDirectoryRecursive(string path)
        {
            if (Directory.Exists(path)) return;
            CreateDirectoryRecursive(Path.GetDirectoryName(path));
            Directory.CreateDirectory(path);
        }
    }
}
