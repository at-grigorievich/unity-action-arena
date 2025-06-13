using System;
using System.Collections.Generic;
using ATG.Character;
using ATG.Observable;

namespace ATG.KillCounter
{
    public class PlayerEarnsPerKillDecorator: IKillCounter, IEarnPerKillCounter
    {
        private MurderRatesConfig _config;
        
        private readonly IKillCounter _decorated;
        private readonly PlayerPresenter _player;

        private readonly IObservableVar<int> _earnsPerKill;
        
        public IReadOnlyObservableVar<int> EarnsPerKill => _earnsPerKill;
        public IReadOnlyDictionary<string, int> KillByName => _decorated.KillByName;
        
        public event Action OnTableChanged
        {
            add => _decorated.OnTableChanged += value;
            remove => _decorated.OnTableChanged -= value;
        }

        public PlayerEarnsPerKillDecorator(PlayerPresenter presenter, MurderRatesConfig murderRatesConfig, 
            IEnumerable<IDieCountable> allDieCountables)
        {
            _config = murderRatesConfig;
            _player = presenter;

            _earnsPerKill = new ObservableVar<int>(0);
            
            _decorated = new TableKillCounter(allDieCountables);
            _decorated.OnTableChanged += OnTableChangedHandler;
        }

        public int GetKillCountByName(string name) => _decorated.GetKillCountByName(name);

        public void Dispose()
        {
            _decorated.Dispose();
            _decorated.OnTableChanged -= OnTableChangedHandler;
        }

        private void OnTableChangedHandler()
        {
            int killCount = GetKillCountByName(_player.Nick);

            int totalEarn = killCount * _config.KillCost;
            
            _earnsPerKill.Value = totalEarn;
        }
    }
}