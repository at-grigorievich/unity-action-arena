using System;
using System.Threading;
using ATG.Animator;
using ATG.Animator.Event_Dispatcher;
using ATG.Attack;
using ATG.Observable;
using ATG.Stamina;
using Cysharp.Threading.Tasks;

namespace Characters.Observers
{
    public class AttackByMBTObserver: IDisposable
    {
        private readonly IAttackService _attack;
        private readonly IAnimatorWrapper _animator;
        private readonly IStaminaService _stamina;

        private readonly TimeSpan _attackDurationSec;

        private CancellationTokenSource _cts;
        
        public readonly IObservableVar<bool> IsAttacking;
        
        public AttackByMBTObserver(IAttackService attack, IAnimatorWrapper animator, 
            IStaminaService stamina)
        {
            _animator = animator;
            _stamina = stamina;
            _attack = attack;

            IsAttacking = new ObservableVar<bool>(false);
            _attackDurationSec = TimeSpan.FromSeconds(_animator.GetStateLength(AnimatorTag.Attack));
        }
        
        public void SetActive(bool isActive)
        {
            if(_animator.EventDispatcher == null)
                throw new Exception("Animator event dispatcher is null");

            Kill();
            
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
            
            _cts = new CancellationTokenSource();
            AttackAsync(_cts.Token).Forget();
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
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            IsAttacking?.Dispose();
        }
        
        private void Kill()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            _attack.Stop();
            IsAttacking.Value = false;
        }
        
        private async UniTask AttackAsync(CancellationToken token)
        {
            IsAttacking.Value = true;
            _animator.SelectState(AnimatorTag.Attack);
            
            await UniTask.Delay(_attackDurationSec, cancellationToken: token);

            Kill();
        }
    }
}