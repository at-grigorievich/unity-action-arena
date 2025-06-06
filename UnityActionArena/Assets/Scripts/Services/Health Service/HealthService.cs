using System;
using ATG.Observable;
using UnityEngine;

namespace ATG.Health
{
    public sealed class HealthService: IHealthService<int>
    {
        private readonly IObservableVar<int> _heath;

        private int _default;
        
        public int Current => Mathf.Clamp(_heath.Value, 0, int.MaxValue);
        
        public float Rate => Mathf.Abs(Current / _default);

        public event Action OnHealthIsOver;

        public HealthService(IObservableVar<int> health)
        {
            _heath = health;
        }
        
        public void Initialize()
        {
            _default = Current;
        }

        public void Reduce(int reduceValue)
        {
            if(Current <= 0) return;
            
            int nextHealth = Mathf.Clamp(Current - reduceValue, 0, _default);
            _heath.Value = nextHealth;

            if (Current <= 0)
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