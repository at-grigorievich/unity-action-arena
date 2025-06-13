using System;
using ATG.Health;
using UnityEngine;

namespace ATG.UI
{
    public readonly struct ArenaBotUIData
    {
        public readonly IHealthRate<int> HealthRate;
        
        public ArenaBotUIData(IHealthRate<int> healthRate)
        {
            HealthRate = healthRate;
        }
    }
    
    public sealed class ArenaBotUIView: LookAtCameraCanvas<ArenaBotUIData>, IDisposable
    {
        [SerializeField] private ProgressBar healthBarView;

        private HealthProgressBar _health;

        protected override void Awake()
        {
            base.Awake();
            _health = new HealthProgressBar(healthBarView, true);
        }

        public override void Show(object sender, ArenaBotUIData data)
        {
            Hide();
            
            _health.Show(data.HealthRate);
            base.Show(sender, data);
        }

        public override void Hide()
        {
            _health.Dispose();
            base.Hide();
        }

        public void Dispose()
        {
            healthBarView?.Dispose();
            _health?.Dispose();
        }
    }
}