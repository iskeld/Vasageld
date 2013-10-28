using EldSharp.Vasageld.Common;
using EldSharp.Vasageld.Core;

namespace EldSharp.Vasageld.Testing.Sandbox
{
    public class Sandbox
    {
        public static void Main(string[] args)
        {
            string path = @"h:\AssemblyInfo.cs";
            FileIncrementor incrementor = new FileIncrementor(path);
            incrementor.Increment(new SimpleProvider());
        }
    }

    public class SimpleProvider : IVersionsProvider
    {
        public Versions SupportedVersions {
            get { return Versions.All; }
        }
        public string GetAssemblyVersion(string current)
        {
            return "jeden";
        }

        public string GetAssemblyFileVersion(string current)
        {
            return "dwa";
        }

        public string GetAssemblyInformationalVersion(string current)
        {
            return "trzy";
        }
    }
}