using System;

namespace EldSharp.Vasageld.Common
{
    public class IncrementationResult
    {
        private readonly IVersionsDictionary _resultVersions;

        public string AssemblyVersion
        {
            get { return _resultVersions[AssemblyVersionAttributeType.AssemblyVersion]; }
        }

        public string AssemblyFileVersion
        {
            get { return _resultVersions[AssemblyVersionAttributeType.AssemblyFileVersion]; }
        }

        public string AssemblyInformationalVersion
        {
            get { return _resultVersions[AssemblyVersionAttributeType.AssemblyInformationalVersion]; }
        }

        public IncrementationResult(IVersionsDictionary resultVersions)
        {
            if (resultVersions == null)
            {
                throw new ArgumentNullException("resultVersions");
            }
            _resultVersions = resultVersions;
        }
    }
}
