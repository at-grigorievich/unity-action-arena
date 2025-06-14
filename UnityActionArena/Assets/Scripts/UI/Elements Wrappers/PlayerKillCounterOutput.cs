using System;
using ATG.KillCounter;

namespace ATG.UI
{
    public sealed class PlayerKillCounterOutput: IDisposable
    {
        private readonly CounterOutput _counterOutput;
        
        private string _playerName;
        private IKillCounter _killCounter;
        
        private int _lastCount = -1;
        
        public PlayerKillCounterOutput(CounterOutput counterOutput)
        {
            _counterOutput = counterOutput;
        }

        public void Show(string playerName, IKillCounter killCounter)
        {
            _playerName = playerName;
            _killCounter = killCounter;
            
            _killCounter.OnTableChanged += OnKillTableChanged;
            OnKillTableChanged();
        }

        public void Hide()
        {
            Dispose();
            _counterOutput.Hide();
            _playerName = string.Empty;
        }
        
        public void Dispose()
        {
            if (_killCounter != null)
            {
                _killCounter.OnTableChanged -= OnKillTableChanged;
            }
            
            _killCounter = null;
        }
        
        private void OnKillTableChanged()
        {
            int count = _killCounter.GetKillCountByName(_playerName);
            if(_lastCount == count) return;
            
            string result = count == 0 ? "0" : $"+{count}";
            _counterOutput.Show(this, result);
            
            _lastCount = count;
        }
    }
}