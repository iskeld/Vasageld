namespace EldSharp.Vasageld.Common
{
    public interface IVersionsProvider
    {
        Versions SupportedVersions { get; }
        string GetAssemblyVersion(string current);
        string GetAssemblyFileVersion(string current);
        string GetAssemblyInformationalVersion(string current);
    }
}
