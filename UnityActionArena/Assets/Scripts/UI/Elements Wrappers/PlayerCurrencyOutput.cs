using System;
using ATG.Observable;

namespace ATG.UI
{
    public class PlayerCurrencyOutput: IDisposable
    {
        private readonly CounterOutput _counterOutput;
        private ObserveDisposable _dis;
        
        public PlayerCurrencyOutput(CounterOutput counterOutput)
        {
            _counterOutput = counterOutput;
        }

        public void Show(IReadOnlyObservableVar<int> currency)
        {
            _dis = currency.Subscribe(OnCurrencyChanged);
            OnCurrencyChanged(currency.Value);
        }

        public void Hide()
        {
            Dispose();
            _counterOutput.Hide();
        }

        public void Dispose()
        {
            _dis?.Dispose();
            _dis = null;
        }

        private void OnCurrencyChanged(int value)
        {
            _counterOutput.Show(this, value.ToString());
        }
    }
}