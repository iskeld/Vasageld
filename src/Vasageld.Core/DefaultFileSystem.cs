using System.IO;
using EldSharp.Vasageld.Core.Interfaces;

namespace EldSharp.Vasageld.Core
{
    internal class DefaultFileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public string GetFileName(string file)
        {
            return Path.GetFileName(file);
        }

        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
