namespace EldSharp.Vasageld.Core.Interfaces
{
    public interface IFileSystem
    {
        bool FileExists(string path);
        string ReadAllText(string path);
        string GetFileName(string file);
        void WriteAllText(string path, string contents);
    }
}
