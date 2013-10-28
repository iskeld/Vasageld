namespace EldSharp.Vasageld.Core.Interfaces
{
    public interface IAttributeTypeVerifier
    {
        AssemblyVersionAttributeType? GetKnownAttributeType(ITypeWrapper wrapper);
    }
}