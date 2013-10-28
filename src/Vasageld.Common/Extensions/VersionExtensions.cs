using System;

namespace EldSharp.Vasageld.Common.Extensions
{
    public static class VersionExtensions
    {
        public static Version Increment(this Version version, Version other)
        {
            return other == null ? version : version.Increment(other.Major, other.Minor, other.Build, other.Revision);
        }

        public static Version Increment(this Version version,
            int major = 0, int minor = 0, int build = 0, int revision = 0)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            return new Version(version.Major + major, version.Minor + minor, version.Build + build,
                version.Revision + revision);
        }

        public static Version IncrementMajor(this Version version)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            return new Version(version.Major + 1, version.Minor, version.Build, version.Revision);
        }

        public static Version IncrementMinor(this Version version)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            return new Version(version.Major, version.Minor + 1, version.Build, version.Revision);
        }

        public static Version IncrementBuild(this Version version)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            return new Version(version.Major, version.Minor, version.Build + 1, version.Revision);
        }

        public static Version IncrementRevision(this Version version)
        {
            if (version == null)
            {
                throw new ArgumentNullException("version");
            }
            return new Version(version.Major, version.Minor, version.Build, version.Revision + 1);
        }
    }
}