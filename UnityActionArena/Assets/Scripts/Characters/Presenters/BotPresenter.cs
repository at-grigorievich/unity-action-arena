using ATG.Animator;
using ATG.Attack;
using ATG.EnemyDetector;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using UnityEngine;

namespace ATG.Character
{
    public sealed class BotPresenter: ArenaCharacterPresenter
    {
        private readonly IEnemyDetector _enemyDetector;
        
        public readonly TargetNavigationPointSet NavigationPoints;

        public readonly IObservableVar<bool> IsGetDamage;
        public IObservableVar<int> Health => _characterModel.Health;
        
        public BotPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaService stamina,
            TargetNavigationPointSet navigationPoints) 
            : base(view, model, animator, move, attack, stamina)
        {
            NavigationPoints = navigationPoints;
            
            IsGetDamage = new ObservableVar<bool>(false);

            _enemyDetector = new RangeEnemyDetector(_characterModel.Range, _view);
        }

        public override void Tick()
        {
            base.Tick();
            _enemyDetector.Detect();
        }

        public void Stop()
        {
            _move.Stop();
        }
        
        public void MoveTo(Vector3 position)
        {
            _move.MoveTo(position);
            _animator.SelectState(AnimatorTag.Run);
        }

        protected override void RequestToGetDamageHandle(AttackDamageData damageData)
        {
            base.RequestToGetDamageHandle(damageData);
            IsGetDamage.Value = true;
        }
    }
}