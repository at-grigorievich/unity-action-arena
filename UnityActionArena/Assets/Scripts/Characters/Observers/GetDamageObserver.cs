using System;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Health;
using ATG.Move;
using ATG.Observable;
using UnityEngine;

namespace Characters.Observers
{
    public sealed class GetDamageObserver
    {
        private readonly IAnimatorWrapper _animator;
        private readonly IMoveableService _move;
        private readonly IHealthService<int> _health;
        
        public readonly IObservableVar<bool> IsDamaged;
        
        public GetDamageObserver(IAnimatorWrapper animator, IMoveableService move, IHealthService<int> healthService)
        {
            _animator = animator;
            _health = healthService;
            _move = move;
            
            IsDamaged = new ObservableVar<bool>(false);
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");
            
            if (isActive == true)
            {
                _animator.EventDispatcher.Subscribe(AnimatorEventType.START_DAMAGE, OnStartDamage);
                _animator.EventDispatcher.Subscribe(AnimatorEventType.STOP_DAMAGE, OnEndDamage);

                IsDamaged.Value = false;
            }
            else
            {
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_DAMAGE, OnStartDamage);
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.STOP_DAMAGE, OnEndDamage);
            }
        }

        public void ReceiveDamage(AttackDamageData damageData)
        {
            IsDamaged.Value = true;
            _move.Stop();
            _animator.SelectState(AnimatorTag.GetDamage);
            _health.Reduce(damageData.Damage);
        }
        
        private void OnStartDamage()
        {
            
        }

        private void OnEndDamage()
        {
            IsDamaged.Value = false;
        }
        
        private void Reset() => IsDamaged.Value = false;
    }
}