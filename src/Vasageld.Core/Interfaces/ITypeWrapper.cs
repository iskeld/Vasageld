namespace EldSharp.Vasageld.Core.Interfaces
{
    public interface ITypeWrapper
    {
        string FullName { get; }
        string GetAssemblyName();
        string GetFullAssemblyName();
        string GetAssemblyQualifiedName(); 
    }
}