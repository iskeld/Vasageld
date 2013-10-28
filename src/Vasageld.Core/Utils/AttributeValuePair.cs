using ICSharpCode.NRefactory.CSharp;

namespace EldSharp.Vasageld.Core.Utils
{
    public sealed class AttributeValuePair
    {
        public Attribute Attribute { get; private set; }
        public string Version { get; private set; }

        public AttributeValuePair(Attribute attribute, string version)
        {
            Attribute = attribute;
            Version = version;
        }
    }
}
