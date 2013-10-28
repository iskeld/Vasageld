using System;
using System.Collections.Generic;
using System.Reflection;
using EldSharp.Vasageld.Core.Interfaces;

namespace EldSharp.Vasageld.Core.Utils
{
    public class AttributeTypeVerifier : IAttributeTypeVerifier
    {
        private static readonly ITypeWrapper AssemblyVersionAttributeTypeWrapper = new ReflectionTypeWrapper(typeof(AssemblyVersionAttribute));
        private static readonly ITypeWrapper AssemblyInformationalVersionAttributeTypeWrapper = new ReflectionTypeWrapper(typeof(AssemblyInformationalVersionAttribute));
        private static readonly ITypeWrapper AssemblyFileVersionAttributeTypeWrapper = new ReflectionTypeWrapper(typeof(AssemblyFileVersionAttribute));

        private readonly IEqualityComparer<ITypeWrapper> _comparer;

        public AttributeTypeVerifier(IEqualityComparer<ITypeWrapper> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            _comparer = comparer;
        }

        public AssemblyVersionAttributeType? GetKnownAttributeType(ITypeWrapper wrapper)
        {
            if (_comparer.Equals(wrapper, AssemblyVersionAttributeTypeWrapper))
            {
                return AssemblyVersionAttributeType.AssemblyVersion;
            }
            if (_comparer.Equals(wrapper, AssemblyFileVersionAttributeTypeWrapper))
            {
                return AssemblyVersionAttributeType.AssemblyFileVersion;
            }
            if (_comparer.Equals(wrapper, AssemblyInformationalVersionAttributeTypeWrapper))
            {
                return AssemblyVersionAttributeType.AssemblyInformationalVersion;
            }
            return null;
        }
    }
}