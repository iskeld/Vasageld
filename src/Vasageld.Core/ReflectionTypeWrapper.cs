using System;
using EldSharp.Vasageld.Core.Interfaces;

namespace EldSharp.Vasageld.Core
{
    public class ReflectionTypeWrapper : ITypeWrapper
    {
        private readonly Type _type;

        public string FullName
        {
            get { return _type.FullName; }
        }

        public ReflectionTypeWrapper(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            _type = type;
        }

        public string GetAssemblyName()
        {
            return _type.Assembly.GetName().Name;
        }

        public string GetFullAssemblyName()
        {
            return _type.Assembly.FullName;
        }

        public string GetAssemblyQualifiedName()
        {
            return _type.AssemblyQualifiedName;
        }
    }
}