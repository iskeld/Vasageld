using System;
using EldSharp.Vasageld.Common;
using EldSharp.Vasageld.Core.Interfaces;

namespace EldSharp.Vasageld.Core
{
    public class ProjectIncrementor : IIncrementor
    {
        private string _projectFile;

        public ProjectIncrementor(string projectFile)
            : this(new DefaultFileSystem(), projectFile)
        {

        }

        public ProjectIncrementor(IFileSystem fileSystem, string projectFile)
        {
            if (fileSystem == null)
            {
                throw new ArgumentNullException("fileSystem");
            }
            if (string.IsNullOrEmpty(projectFile))
            {
                throw new ArgumentNullException("projectFile");
            }
            if (!fileSystem.FileExists(projectFile))
            {
                throw new ArgumentException(string.Format(Resources.ProjectIncrementor_ProjectFileDoesNotExist, projectFile));
            }

            _projectFile = projectFile;
        }

        public IncrementationResult Increment(IVersionsProvider versionsProvider, Versions versionsToIncrement)
        {
            throw new NotImplementedException();
        }

        public IncrementationResult Increment(IVersionsProvider versionsProvider)
        {
            if (versionsProvider == null)
            {
                throw new ArgumentNullException("versionsProvider");
            }
            if (!CheckIfProviderSupportsAnyVersion(versionsProvider))
            {
                throw new ArgumentException(Resources.VersionsProviderDoesNotSupportVersions);
            }
            throw new NotImplementedException();
        }

        private static bool CheckIfProviderSupportsAnyVersion(IVersionsProvider versionsProvider)
        {
            var versions = (int) (versionsProvider.SupportedVersions & Versions.All);
            return versions != 0;
        }
    }
}