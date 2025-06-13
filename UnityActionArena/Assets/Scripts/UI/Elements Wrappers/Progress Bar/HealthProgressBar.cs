using System;
using ATG.Health;
using ATG.Observable;

namespace ATG.UI
{
    public class HealthProgressBar: IDisposable
    {
        private readonly bool _withAnimation;
        private readonly ProgressBar _bar;

        private IHealthRate<int> _healthRate;
        private ObserveDisposable _dis;

        public HealthProgressBar(ProgressBar bar, bool withAnimation)
        {
            _bar = bar;
            _withAnimation = withAnimation;
        }

        public void Show(IHealthRate<int> healthRate)
        {
            _healthRate = healthRate;
            
            _bar.Show(this, new ProgressBarRate(_healthRate.Rate, _withAnimation));
            _dis = healthRate.Current.Subscribe(OnHealthChanged);
        }

        public void Dispose()
        {
            _dis?.Dispose();
            _dis = null;
            
            _healthRate = null;
        }
        
        private void OnHealthChanged(int _)
        {
            _bar.SetData(new ProgressBarRate(_healthRate.Rate, _withAnimation));
        }
    }
}