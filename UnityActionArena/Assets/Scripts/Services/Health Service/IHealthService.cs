using System;
using ATG.Observable;

namespace ATG.Health
{
    public interface IHealthRate<T>
    {
        IReadOnlyObservableVar<T> Current { get; }
        float Rate { get; }
    }
    
    public interface IHealthService<T>: IHealthRate<T>
    {
        event Action OnHealthIsOver;
        
        void Initialize();
        void Reduce(T reduceValue);
        void Reset();
    }
}