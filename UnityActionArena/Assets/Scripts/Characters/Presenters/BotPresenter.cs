using System.Collections.Generic;
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
        public readonly IObservableVar<bool> HasDetectedEnemies;
        
        public IObservableVar<int> Health => _characterModel.Health;
        
        public IReadOnlyCollection<IDetectable> DetectedEnemies { get; private set; }

        public bool CanAttack => _stamina.IsEnough == true && _attack.IsAvailable == true;
        
        public BotPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, 
            IAttackService attack, IStaminaService stamina,
            TargetNavigationPointSet navigationPoints) 
            : base(view, model, animator, move, attack, stamina)
        {
            NavigationPoints = navigationPoints;
            _enemyDetector = new RangeEnemyDetector(_characterModel.Range, _view);
            
            IsGetDamage = new ObservableVar<bool>(false);
            HasDetectedEnemies = new ObservableVar<bool>(false);
        }

        public override void Tick()
        {
            base.Tick();
            DetectedEnemies = _enemyDetector.Detect();
            HasDetectedEnemies.Value = DetectedEnemies.Count > 0;
        }

        public void StayInShock()
        {
            _move.Stop();
        }

        public void Attack()
        {
            
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