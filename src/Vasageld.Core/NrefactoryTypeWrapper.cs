using System;
using System.Reflection;
using EldSharp.Vasageld.Core.Interfaces;
using ICSharpCode.NRefactory.TypeSystem;

namespace EldSharp.Vasageld.Core
{
    public class NrefactoryTypeWrapper : ITypeWrapper
    {
        private readonly IType _type;
        private ITypeDefinition _typeDefinition;

        private ITypeDefinition TypeDefinition
        {
            get { return _typeDefinition ?? (_typeDefinition = _type.GetDefinition()); }
        }

        public string FullName
        {
            get { return _type.FullName; }
        }

        public NrefactoryTypeWrapper(IType type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            _type = type;
        }

        public string GetAssemblyName()
        {
            string result = TypeDefinition.ParentAssembly.AssemblyName;
            return result;
        }

        public string GetFullAssemblyName()
        {
            string result = TypeDefinition.ParentAssembly.FullAssemblyName;
            return result;
        }

        public string GetAssemblyQualifiedName()
        {
            string result = Assembly.CreateQualifiedName(GetFullAssemblyName(), FullName);
            return result;
        }
    }
}