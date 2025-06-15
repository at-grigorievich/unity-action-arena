using System;
using ATG.Bank;
using ATG.Observable;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct CurrencyButtonData
    {
        public readonly IBank<int> Bank;
        public readonly int Price;

        public CurrencyButtonData(IBank<int> bank, int price)
        {
            Bank = bank;
            Price = price;
        }
    }
    
    public class CurrencyButton: ActivateButton<CurrencyButtonData>, IDisposable
    {
        [SerializeField] private CounterOutput counter;

        private int _price;
        private ObserveDisposable _dis;
        
        public override void Show(object sender, CurrencyButtonData data)
        {
            Activate();
            base.Show(sender, data);
            Dispose();

            _dis = data.Bank.Currency.Subscribe(OnBankCurrencyChanged);

            _price = data.Price;
            
            OnBankCurrencyChanged(data.Bank.Currency.Value);
            counter.Show(this, data.Price.ToString());
        }

        public override void Hide()
        {
            _dis?.Dispose();
            
            Deactivate();
            base.Hide();
        }

        public void Dispose()
        {
            _dis?.Dispose();
            counter?.Dispose();
        }

        private void OnBankCurrencyChanged(int newBank)
        {
            if (newBank >= _price)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
    }
}