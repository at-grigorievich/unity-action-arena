using System;
using System.Threading;
using ATG.Animator;
using ATG.Attack;
using ATG.Health;
using ATG.Move;
using ATG.Observable;
using Cysharp.Threading.Tasks;

namespace Characters.Observers
{
    public sealed class GetDamageObserver: IDisposable
    {
        private readonly IAnimatorWrapper _animator;
        private readonly IMoveableService _move;
        private readonly IHealthService<int> _health;

        private readonly TimeSpan _damagedDurationSec;

        private CancellationTokenSource _cts;
        
        public readonly IObservableVar<bool> IsDamaged;
        
        public AttackDamageData? LastReceivedDamage { get; private set; }
        
        public GetDamageObserver(IAnimatorWrapper animator, IMoveableService move, IHealthService<int> healthService)
        {
            _animator = animator;
            _health = healthService;
            _move = move;
            
            IsDamaged = new ObservableVar<bool>(false);

            _damagedDurationSec = TimeSpan.FromSeconds(_animator.GetStateLength(AnimatorTag.GetDamage));
        }
        
        public void SetActive(bool isActive)
        {
            LastReceivedDamage = null;
            IsDamaged.Value = false;
            
            Kill();
        }

        public void ReceiveDamage(AttackDamageData damageData)
        {
            _health.Reduce(damageData.Damage);
            LastReceivedDamage = damageData;
            
            Kill();
            
            _cts = new CancellationTokenSource();
            ReceiveDamageAsync(_cts.Token).Forget();
        }
        
        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            IsDamaged?.Dispose();
        }
        
        private void Kill()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            
            IsDamaged.Value = false;
        }

        private async UniTask ReceiveDamageAsync(CancellationToken token)
        {
            IsDamaged.Value = true;
            
            _animator.SelectState(AnimatorTag.GetDamage);
            
            await UniTask.Delay(_damagedDurationSec, cancellationToken: token);

            Kill();
        }
    }
}