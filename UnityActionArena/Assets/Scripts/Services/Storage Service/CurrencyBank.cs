using System;
using ATG.Observable;

namespace ATG.Bank
{
    public class CurrencyBank: IBank<int>
    {
        private IObservableVar<int> _currency;

        public IReadOnlyObservableVar<int> Currency => _currency;

        public CurrencyBank(IObservableVar<int> currency)
        {
            _currency = currency;
        }
        
        public void AddCurrency(int amount)
        {
            if(amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            _currency.Value += amount;
        }

        public bool TrySpendCurrency(int amount)
        {
            if (_currency.Value <= amount) return false;

            _currency.Value -= amount;
            
            return true;
        }
    }
}