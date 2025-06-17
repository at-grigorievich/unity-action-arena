using System;
using ATG.KillCounter;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct PlayerStatisticUIData
    {
        public readonly string PlayerNick;
        public readonly IKillCounter KillCounter;
        public readonly IEarnPerKillCounter EarnPerKillCounter;

        public PlayerStatisticUIData(string nick, IKillCounter killCounter, IEarnPerKillCounter perKillCounter)
        {
            PlayerNick = nick;
            KillCounter = killCounter;
            EarnPerKillCounter = perKillCounter;
        }
    }
    
    public class PlayerStatisticView: UIElement<PlayerStatisticUIData>, IDisposable
    {
        [SerializeField] private CounterOutput killCountOutput;
        [SerializeField] private CounterOutput earnCountOutput;

        private PlayerKillCounterOutput _killCounter;
        private PlayerEarnCounterOutput _earnCounter;
        
        protected override void Awake()
        {
            base.Awake();
           _killCounter = new PlayerKillCounterOutput(killCountOutput);
           _earnCounter = new PlayerEarnCounterOutput(earnCountOutput);
        }
        
        public override void Show(object sender, PlayerStatisticUIData data)
        {
            _killCounter.Show(data.PlayerNick, data.KillCounter);
            _earnCounter.Show(data.EarnPerKillCounter);
        }

        public override void Hide()
        {
            Dispose();

            _killCounter.Hide();
            _earnCounter.Hide();
        }
        
        public void Dispose()
        {
            killCountOutput?.Dispose();
            earnCountOutput?.Dispose();
        }
    }
}