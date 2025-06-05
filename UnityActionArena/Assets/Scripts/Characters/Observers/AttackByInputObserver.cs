using System;
using System.Collections.Generic;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Input;

namespace Characters.Observers
{
    public sealed class AttackByInputObserver
    {
        private readonly IInputable _input;
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;

        public event Action<IReadOnlyCollection<IAttackable>> OnAttackCompleted; 
        
        public AttackByInputObserver(IInputable input, IAttackService attack, IAnimatorWrapper animator)
        {
            _input = input;
            _attack = attack;
            _animator = animator;
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");
            
            if (isActive == true)
            {
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
            
            _animator.EventDispatcher.Subscribe(AnimatorEventType.START_SWING, OnStartSwing);
            
            _animator.SelectState(AnimatorTag.Attack);
        }
        
        private void OnStartSwing()
        {
            _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_SWING, OnStartSwing);
            _animator.EventDispatcher.Subscribe(AnimatorEventType.END_SWING, OnEndSwing);
            
            //Debug.Log("start swing");
            
            _attack.TakeSwing();
        }
        
        private void OnEndSwing()
        {
            _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_SWING, OnStartSwing);
            _animator.EventDispatcher.Unsubscribe(AnimatorEventType.END_SWING, OnEndSwing);
            
            //Debug.Log("end swing");
            
            var result = _attack.EndSwing();

            OnAttackCompleted?.Invoke(result);
        }
    }
}