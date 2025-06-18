using System;
using System.Collections.Generic;
using ATG.Character;
using ATG.Observable;
using ATG.User;

namespace ATG.KillCounter
{
    public class PlayerEarnsPerKillDecorator: IKillCounter, IEarnPerKillCounter
    {
        private MurderRatesConfig _config;
        
        private readonly IKillCounter _decorated;
        private readonly PlayerPresenter _player;
        private readonly UserPresenter _user;

        private readonly IObservableVar<int> _earnsPerKill;
        private int _lastEarn;
        
        public IReadOnlyObservableVar<int> EarnsPerKill => _earnsPerKill;
        public IReadOnlyDictionary<string, int> KillByName => _decorated.KillByName;

        private ObserveDisposable _dis;
        
        public event Action OnTableChanged
        {
            add => _decorated.OnTableChanged += value;
            remove => _decorated.OnTableChanged -= value;
        }

        public PlayerEarnsPerKillDecorator(PlayerPresenter presenter, UserPresenter user,
            MurderRatesConfig murderRatesConfig, 
            IEnumerable<IDieCountable> allDieCountables)
        {
            _config = murderRatesConfig;
            _player = presenter;
            _user = user;

            _earnsPerKill = new ObservableVar<int>(0);
            _dis = _earnsPerKill.Subscribe(OnEarnsPerKillsChanged);
            
            _decorated = new TableKillCounter(allDieCountables);
            _decorated.OnTableChanged += OnTableChangedHandler;
        }

        public int GetKillCountByName(string name) => _decorated.GetKillCountByName(name);

        public void Dispose()
        {
            _decorated.Dispose();
            _decorated.OnTableChanged -= OnTableChangedHandler;
            
            _dis?.Dispose();
            _dis = null;
        }

        private void OnTableChangedHandler()
        {
            int killCount = GetKillCountByName(_player.Nick);
            
            int totalEarn = killCount * _config.KillCost;
            
            _earnsPerKill.Value = totalEarn;
        }
        
        private void OnEarnsPerKillsChanged(int newEarn)
        {
            int delta = newEarn - _lastEarn;
            if (delta > 0)
            {
                _user.AddCurrency(delta);
            }
            _lastEarn = newEarn;
        }
    }
}