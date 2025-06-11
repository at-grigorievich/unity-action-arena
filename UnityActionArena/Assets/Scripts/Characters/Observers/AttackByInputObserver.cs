using System;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Input;
using ATG.Observable;
using ATG.Stamina;

namespace Characters.Observers
{
    public sealed class AttackByInputObserver
    {
        private readonly IInputable _input;
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;
        private readonly IStaminaService _stamina;
        
        public readonly IObservableVar<bool> IsAttacking;
        
        public AttackByInputObserver(IInputable input, IAttackService attack, IAnimatorWrapper animator, 
            IStaminaService stamina)
        {
            _input = input;
            _attack = attack;
            _animator = animator;
            _stamina = stamina;
            
            IsAttacking = new ObservableVar<bool>(false);
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");
            
            _attack.Stop();
            IsAttacking.Value = false;
            
            if (isActive == true)
            {
                _animator.EventDispatcher.Subscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Subscribe(AnimatorEventType.END_SWING, OnEndSwing);
                _animator.EventDispatcher.Subscribe(AnimatorEventType.END_ATTACK, OnEndAttack);
                
                _input.OnLMBClicked += OnLMBClicked;
            }
            else
            {
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.START_SWING, OnStartSwing);
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.END_SWING, OnEndSwing);
                _animator.EventDispatcher.Unsubscribe(AnimatorEventType.END_ATTACK, OnEndAttack);
                
                _input.OnLMBClicked -= OnLMBClicked;
            }
        }
        
        private void OnLMBClicked(bool obj)
        {
            if(obj == false) return;
            if(_stamina.IsEnough == false) return;
            if(IsAttacking.Value == true) return;
            
            IsAttacking.Value = true;
            _animator.SelectState(AnimatorTag.Attack);
        }
        
        private void OnStartSwing()
        {
            //Debug.Log("start swing");

            _stamina.Reduce(_stamina.DefaultReduceAmount);
            _attack.TakeSwing();
        }
        
        private void OnEndSwing()
        {
            //Debug.Log("end swing");
            var result = _attack.EndSwing();
        }

        private void OnEndAttack()
        {
            IsAttacking.Value = false;
        }
    }
}