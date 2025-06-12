using System;
using ATG.Observable;
using UnityEngine;

namespace ATG.Health
{
    public sealed class HealthService: IHealthService<int>
    {
        private readonly IObservableVar<int> _heath;

        private int _default;

        public IReadOnlyObservableVar<int> Current => _heath;
        
        public float Rate => Mathf.Clamp01((float)Current.Value / _default);

        public event Action OnHealthIsOver;

        public HealthService(IObservableVar<int> health)
        {
            _heath = health;
        }
        
        public void Initialize()
        {
            _default = Current.Value;
        }

        public void Reduce(int reduceValue)
        {
            if(Current.Value <= 0) return;
            
            int nextHealth = Mathf.Clamp(Current.Value - reduceValue, 0, _default);
            _heath.Value = nextHealth;

            if (Current.Value <= 0)
            {
                OnHealthIsOver?.Invoke();
            }
        }

        public void Reset()
        {
            _heath.Value = _default;
        }
    }
}