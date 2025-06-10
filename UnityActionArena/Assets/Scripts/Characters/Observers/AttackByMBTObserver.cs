using System;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Observable;
using ATG.Stamina;
using UnityEngine;

namespace Characters.Observers
{
    public class AttackByMBTObserver
    {
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;
        private readonly IStaminaService _stamina;

        public readonly IObservableVar<bool> IsAttacking;

        public AttackByMBTObserver(IAttackService attack, IAnimatorWrapper animator, 
            IStaminaService stamina)
        {
            _animator = animator;
            _stamina = stamina;
            _attack = attack;

            IsAttacking = new ObservableVar<bool>(false);
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");
            
            if (isActive == true)
            {
                _animator.EventDispatcher.Subscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Subscribe(AnimatorEventType.END_SWING, OnEndSwing);
            }
            else
            {
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.END_SWING, OnEndSwing);
            }
        }
        
        public void OnAttackRequired()
        {
            if(_stamina.IsEnough == false) return;
            if(IsAttacking.Value == true) return;
            
            _animator.SelectState(AnimatorTag.Attack);
        }
        
        private void OnStartSwing()
        {
            Debug.Log("start swing");;
            IsAttacking.Value = true;
            
            _stamina.Reduce(_stamina.DefaultReduceAmount);
            _attack.TakeSwing();
        }
        
        private void OnEndSwing()
        {
            Debug.Log("end swing");
            
            var result = _attack.EndSwing();
            
            IsAttacking.Value = false;
        }
    }
}