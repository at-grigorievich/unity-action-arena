using System;

namespace ATG.Health
{
    public interface IHealthService<T>
    {
        T Current { get; }
        float Rate { get; }
        
        event Action OnHealthIsOver;
        
        void Initialize();
        void Reduce(T reduceValue);
        void Reset();
    }
}