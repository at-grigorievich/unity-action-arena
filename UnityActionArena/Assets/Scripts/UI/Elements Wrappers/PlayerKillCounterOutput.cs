using ATG.KillCounter;

namespace ATG.UI
{
    public sealed class PlayerKillCounterOutput
    {
        private readonly string _playerName;
        private readonly CounterOutput _counterOutput;
        private readonly IKillCounter _killCounter;

        private int _lastCount = -1;
        
        public PlayerKillCounterOutput(string playerName, CounterOutput counterOutput, IKillCounter killCounter)
        {
            _playerName = playerName;
            _counterOutput = counterOutput;
            _killCounter = killCounter;
        }

        public void Show()
        {
            _killCounter.OnTableChanged += OnKillTableChanged;
            OnKillTableChanged();
        }

        public void Dispose()
        {
            _killCounter.OnTableChanged -= OnKillTableChanged;
            _counterOutput.Hide();
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