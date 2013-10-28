namespace EldSharp.Vasageld.Common
{
    public interface IIncrementor
    {
        IncrementationResult Increment(IVersionsProvider versionsProvider, Versions versionsToIncrement);
        IncrementationResult Increment(IVersionsProvider versionsProvider);
    }
}
