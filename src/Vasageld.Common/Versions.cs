using System;

namespace EldSharp.Vasageld.Common
{
    [Flags]
    public enum Versions
    {
        AssemblyVersion = 1,
        AssemblyFileVersion = 2,
        AssemblyInformationalVersion = 4,

        All = 7
    }
}
