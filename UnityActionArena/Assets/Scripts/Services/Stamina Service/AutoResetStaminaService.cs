using System;
using ATG.Observable;
using UnityEngine;

namespace ATG.Stamina
{
    [Serializable]
    public sealed class AutoResetStaminaServiceCreator
    {
        [SerializeField] private StaminaConfig config;

        public IStaminaService Create(IObservableVar<float> stamina)
        {
            return new AutoResetStaminaService(stamina, config);
        }
    }
    
    public sealed class AutoResetStaminaService: IStaminaService
    {
        private readonly IObservableVar<float> _stamina;
        
        private readonly int _defaultReduceAmount;
        private readonly int _defaultResetAmount;
        private readonly float _delayAfterReduce;
        
        private float _default;
        private float _curReduceDelay;
        
        public IReadOnlyObservableVar<float> Current => _stamina;

        public bool IsEnough => Current.Value >= _defaultReduceAmount;
        
        public float Rate => Mathf.Clamp01(Current.Value / _default);
        
        public int DefaultReduceAmount => _defaultReduceAmount;
        
        public AutoResetStaminaService(IObservableVar<float> stamina, StaminaConfig config)
        {
            _stamina = stamina;
            _defaultReduceAmount = config.DefaultReduceAmount;
            _defaultResetAmount = config.DefaultResetAmount;
            _delayAfterReduce = config.DelayAfterReduce;
        }

        public void Initialize()
        {
            _default = Current.Value;
        }

        public void Tick()
        {
            if (_curReduceDelay < (IsEnough == true ? _delayAfterReduce : 2 * _delayAfterReduce))
            {
                _curReduceDelay += Time.deltaTime;
                return;
            }
            
            float nextValue = Mathf.Clamp(Current.Value + _defaultResetAmount * Time.deltaTime, 0, _default);
            _stamina.Value = nextValue;
        }

        public void Reduce(int reduceAmount)
        {
            _stamina.Value = Mathf.Clamp(_stamina.Value - reduceAmount, 0, float.MaxValue);
            _curReduceDelay = 0f;
        }

        public void Reset()
        {
            _stamina.Value = _default;
            _curReduceDelay = 0f;
        }
    }
}