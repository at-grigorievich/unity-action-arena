using System;
using ATG.KillCounter;
using ATG.Observable;

namespace ATG.UI
{
    public sealed class PlayerEarnCounterOutput: IDisposable
    {
        private readonly CounterOutput _counterOutput;
        
        private IEarnPerKillCounter _earnPerKillCounter;
        private ObserveDisposable _dis;
        
        public PlayerEarnCounterOutput(CounterOutput counterOutput)
        {
            _counterOutput = counterOutput;
        }
        
        public void Show(IEarnPerKillCounter earnPerKillCounter)
        {
            _earnPerKillCounter = earnPerKillCounter;
            
            _dis = _earnPerKillCounter.EarnsPerKill.Subscribe(OnEarnChanged);
            OnEarnChanged(_earnPerKillCounter.EarnsPerKill.Value);
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
            _counterOutput.Dispose();

            _earnPerKillCounter = null;
        }

        private void OnEarnChanged(int totalEarn)
        {
            string result = totalEarn == 0 ? "0" : $"+{totalEarn}";
            _counterOutput.Show(this, result);
        }
    }
}