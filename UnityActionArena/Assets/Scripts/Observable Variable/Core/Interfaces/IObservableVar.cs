using System;

namespace ATG.Observable
{
    public interface IReadOnlyObservableVar<out T>: IDisposable
    {
        T Value { get; }
        T DefaultValue { get; }
        
        ObserveDisposable Subscribe(Action<T> callback);
    }
    
    public interface IObservableVar<T>: IReadOnlyObservableVar<T>
    {
        T Value { get; set; }
    }
}
