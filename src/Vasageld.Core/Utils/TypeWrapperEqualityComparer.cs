using System.Collections.Generic;
using EldSharp.Vasageld.Core.Interfaces;

namespace EldSharp.Vasageld.Core.Utils
{
    public class TypeWrapperEqualityComparer : IEqualityComparer<ITypeWrapper>
    {
        public bool Equals(ITypeWrapper x, ITypeWrapper y)
        {
            if (x == null)
            {
                return y == null;
            }
            if (y == null)
            {
                return false;
            }
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x.FullName != y.FullName)
            {
                return false;
            }
            return x.GetAssemblyQualifiedName() == y.GetAssemblyQualifiedName();
        }

        public int GetHashCode(ITypeWrapper obj)
        {
            return obj.GetHashCode();
        }
    }
}