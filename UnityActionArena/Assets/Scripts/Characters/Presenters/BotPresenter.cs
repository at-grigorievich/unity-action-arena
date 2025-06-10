using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.EnemyDetector;
using ATG.Move;
using ATG.Observable;
using ATG.Stamina;
using Characters.Observers;
using UnityEngine;

namespace ATG.Character
{
    public sealed class BotPresenter: ArenaCharacterPresenter
    {
        private readonly IEnemyDetector _enemyDetector;
        private readonly AttackByMBTObserver _attackObserver;
        
        public readonly TargetNavigationPointSet NavigationPoints;
        
        public readonly IObservableVar<bool> HasDetectedEnemies;
        
        public IObservableVar<int> Health => _characterModel.Health;
        public IObservableVar<bool> IsGetDamage => _getDamageObserver.IsDamaged;
        public IObservableVar<bool> IsAttacking => _attackObserver.IsAttacking;
        
        public IReadOnlyCollection<IDetectable> DetectedEnemies { get; private set; }

        public bool EnoughStamina => _stamina.IsEnough;
        
        public BotPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaService stamina,
            TargetNavigationPointSet navigationPoints) 
            : base(view, model, animator, move, attack, stamina)
        {
            NavigationPoints = navigationPoints;
            _enemyDetector = new RangeEnemyDetector(_characterModel.Range, _view);
            _attackObserver = new AttackByMBTObserver(_attack, _animator, _stamina);
            
            HasDetectedEnemies = new ObservableVar<bool>(false);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            _attackObserver.SetActive(isActive);
        }

        public override void Tick()
        {
            base.Tick();
            DetectedEnemies = _enemyDetector.Detect();
            HasDetectedEnemies.Value = DetectedEnemies.Count > 0;
        }

        public void Stop()
        {
            _animator.SelectState(AnimatorTag.Idle);
            _move.Stop();
        }

        public void Attack()
        {
            _attackObserver.OnAttackRequired();
        }
        
        public void MoveTo(Vector3 position)
        {
            _move.MoveTo(position);
            _animator.SelectState(AnimatorTag.Run);
        }
    }
}