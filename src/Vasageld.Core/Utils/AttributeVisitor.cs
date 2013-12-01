using System;
using EldSharp.Vasageld.Common;
using EldSharp.Vasageld.Core.Interfaces;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;

namespace EldSharp.Vasageld.Core.Utils
{
    public class AttributeVisitor : DepthFirstAstVisitor
    {
        private readonly CSharpAstResolver _resolver;
        private readonly IAttributeTypeVerifier _attributeTypeVerifier;
        public AttributeVisitorResults Results { get; private set; }

        public AttributeVisitor(CSharpAstResolver resolver, IAttributeTypeVerifier attributeTypeVerifier)
        {
            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }
            if (attributeTypeVerifier == null)
            {
                throw new ArgumentNullException("attributeTypeVerifier");
            }
            _resolver = resolver;
            _attributeTypeVerifier = attributeTypeVerifier;

            Results = new AttributeVisitorResults();
        }

        public override void VisitAttribute(ICSharpCode.NRefactory.CSharp.Attribute attribute)
        {
            base.VisitAttribute(attribute);
            ResolveResult result = _resolver.Resolve(attribute.Type);
            IType attributeType = result.Type;
            ITypeWrapper wrapper = new NrefactoryTypeWrapper(attributeType);

            AssemblyVersionAttributeType? knownType = _attributeTypeVerifier.GetKnownAttributeType(wrapper);
            if (!knownType.HasValue)
            {
                return;
            }

            var ctorInvocation = (CSharpInvocationResolveResult)_resolver.Resolve(attribute);
            ResolveResult argumentResolveResult = ctorInvocation.Arguments[0];

            if (!argumentResolveResult.IsCompileTimeConstant)
            {
                throw new NotSupportedException();
            }
            string version = argumentResolveResult.ConstantValue.ToString();

            Results[knownType.Value] = new AttributeValuePair(attribute, version);
        }
    }
}