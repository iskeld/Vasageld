using System;
using EldSharp.Vasageld.Common;

namespace EldSharp.Vasageld.Core
{
    public class IncrementorsFactory : IIncrementorsFactory
    {
        public static IIncrementorsFactory Default;

        public IIncrementor ForProjectFile(string projectFile)
        {
            throw new NotImplementedException();
        }
    }
}
