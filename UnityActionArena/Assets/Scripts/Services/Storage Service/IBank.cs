using ATG.Observable;

namespace ATG.Bank
{
    public interface IBank<T>
    {
        public IReadOnlyObservableVar<T> Currency { get; }

        void AddCurrency(T amount);
        bool TrySpendCurrency(T amount);
    }
}