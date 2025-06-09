using System;
using ATG.Observable;
using UnityEngine;

namespace ATG.Stamina
{
    [Serializable]
    public sealed class AutoResetStaminaServiceCreator
    {
        [SerializeField] private StaminaConfig config;

        public IStaminaService Create(IObservableVar<int> stamina)
        {
            return new AutoResetStaminaService(stamina, config);
        }
    }
    
    public sealed class AutoResetStaminaService: IStaminaService
    {
        private readonly IObservableVar<int> _stamina;
        
        private int _default;
        private readonly int _defaultReduceAmount;
        private readonly int _defaultResetAmount;
        private readonly float _delayAfterReduce;
        
        private float _curReduceDelay;
        
        public int Current => Mathf.Clamp(_stamina.Value, 0, int.MaxValue);

        public bool IsEnough => Current >= _defaultReduceAmount;
        
        public float Rate => Mathf.Clamp01((float)Current / _default);
        
        public int DefaultReduceAmount => _defaultReduceAmount;
        
        
        public AutoResetStaminaService(IObservableVar<int> stamina, StaminaConfig config)
        {
            _stamina = stamina;
            _defaultReduceAmount = config.DefaultReduceAmount;
            _defaultResetAmount = config.DefaultResetAmount;
            _delayAfterReduce = config.DelayAfterReduce;
        }

        public void Initialize()
        {
            _default = Current;
        }

        public void Tick()
        {
            if (_curReduceDelay < _delayAfterReduce)
            {
                _curReduceDelay += Time.deltaTime;
                return;
            }
            int nextValue = Mathf.Clamp(Current + _defaultResetAmount, 0, _default);
            _stamina.Value = nextValue;
        }

        public void Reduce(int reduceAmount)
        {
            _stamina.Value = Mathf.Clamp(_stamina.Value - reduceAmount, 0, int.MaxValue);
            _curReduceDelay = 0f;
        }

        public void Reset()
        {
            _stamina.Value = _default;
            _curReduceDelay = 0f;
        }
    }
}