using System;
using System.Collections.Generic;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Input;
using ATG.Stamina;

namespace Characters.Observers
{
    public sealed class AttackByInputObserver
    {
        private readonly IInputable _input;
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;
        private readonly IStaminaService _stamina;

        private bool _isAttacking;
        
        public event Action<IReadOnlyCollection<IAttackable>> OnAttackCompleted; 
        
        public AttackByInputObserver(IInputable input, IAttackService attack, IAnimatorWrapper animator, 
            IStaminaService stamina)
        {
            _input = input;
            _attack = attack;
            _animator = animator;
            _stamina = stamina;
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");
            
            if (isActive == true)
            {
                _animator.EventDispatcher.Subscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Subscribe(AnimatorEventType.END_SWING, OnEndSwing);
                _input.OnLMBClicked += OnLMBClicked;
            }
            else
            {
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.END_SWING, OnEndSwing);
                
                _input.OnLMBClicked -= OnLMBClicked;
            }
        }
        
        private void OnLMBClicked(bool obj)
        {
            if(obj == false) return;
            if(_attack.IsAvailable == false) return;
            if(_stamina.IsEnough == false) return;
            if(_isAttacking == true) return;
            
            _animator.SelectState(AnimatorTag.Attack);
        }
        
        private void OnStartSwing()
        {
            //Debug.Log("start swing");
            
            _isAttacking = true;
            
            _stamina.Reduce(_stamina.DefaultReduceAmount);
            _attack.TakeSwing();
        }
        
        private void OnEndSwing()
        {
            //Debug.Log("end swing");
            
            var result = _attack.EndSwing();
            
            OnAttackCompleted?.Invoke(result);
            
            _isAttacking = false;
        }
    }
}