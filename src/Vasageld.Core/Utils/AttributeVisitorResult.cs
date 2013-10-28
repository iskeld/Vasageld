using System.Collections.Generic;

namespace EldSharp.Vasageld.Core.Utils
{
    public class AttributeVisitorResults
    {
        private readonly Dictionary<AssemblyVersionAttributeType, AttributeValuePair> _attributes =
            new Dictionary<AssemblyVersionAttributeType, AttributeValuePair>();

        public AttributeValuePair this[AssemblyVersionAttributeType type]
        {
            get
            {
                AttributeValuePair result;
                return _attributes.TryGetValue(type, out result) ? result : null;
            }
            set { _attributes[type] = value; }
        }
    }
}