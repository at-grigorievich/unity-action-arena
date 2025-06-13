using ATG.Observable;

namespace ATG.KillCounter
{
    public interface IEarnPerKillCounter
    {
        IReadOnlyObservableVar<int> EarnsPerKill { get; }
    }
}