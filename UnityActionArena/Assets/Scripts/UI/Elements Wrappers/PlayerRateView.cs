using System;
using ATG.Health;
using ATG.Stamina;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct PlayerRateUIData
    {
        public readonly IHealthRate<int> HealthRate;
        public readonly IStaminaRate StaminaRate;

        public PlayerRateUIData(IHealthRate<int> healthRate, IStaminaRate staminaRate)
        {
            HealthRate = healthRate;
            StaminaRate = staminaRate;
        }
    }
    
    public class PlayerRateView: UIElement<PlayerRateUIData>, IDisposable
    {
        [SerializeField] private ProgressBar heathBarView;
        [SerializeField] private ProgressBar staminaBarView;
        
        private HealthProgressBar _healthBar;
        private StaminaProgressBar _staminaBar;

        protected override void Awake()
        {
            base.Awake();

            _healthBar = new HealthProgressBar(heathBarView, true);
            _staminaBar = new StaminaProgressBar(staminaBarView, true);
        }

        public override void Show(object sender, PlayerRateUIData data)
        {
            _healthBar.Show(data.HealthRate);
            _staminaBar.Show(data.StaminaRate);
        }

        public override void Hide()
        {
            _healthBar.Dispose();
            _staminaBar.Dispose();
        }

        public void Dispose()
        {
            _healthBar?.Dispose();
            _staminaBar?.Dispose();
        }
    }
}