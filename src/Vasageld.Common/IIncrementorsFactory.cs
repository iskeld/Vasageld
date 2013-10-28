namespace EldSharp.Vasageld.Common
{
    public interface IIncrementorsFactory
    {
        IIncrementor ForProjectFile(string projectFile);
    }
}
