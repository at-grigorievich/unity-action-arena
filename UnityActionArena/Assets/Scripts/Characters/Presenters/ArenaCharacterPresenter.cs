using System.Collections.Generic;
using ATG.Animator;
using ATG.Attack;
using ATG.Health;
using ATG.Items;
using ATG.Move;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Character
{
    public abstract class ArenaCharacterPresenter: CharacterPresenter, ITickable
    {
        protected readonly IAttackService _attack;
        protected readonly IHealthService<int> _health;
        
        protected ArenaCharacterPresenter(CharacterView view, CharacterModel model, 
            IAnimatorWrapper animator, IMoveableService move, IAttackService attack) 
            : base(view, model, animator, move)
        {
            _attack = attack;
            _health = new HealthService(model.Health);
        }

        public override void Initialize()
        {
            base.Initialize();
            _attack.InitOwner(_view);
            
            _view.OnAttacked += RequestToGetDamageHandle;
            _attack.OnRequestToDealDamage += RequestToDealDamageHandle;
            _health.OnHealthIsOver += OnHealthIsOverHandle;
        }


        public override void Dispose()
        {
            base.Dispose();
            
            _view.OnAttacked -= RequestToGetDamageHandle;
            _attack.OnRequestToDealDamage -= RequestToDealDamageHandle;
            _health.OnHealthIsOver -= OnHealthIsOverHandle;
        }

        public virtual void Tick()
        {
            if(_isActive == false) return;
            
            _attack.Tick();
        }

        public override void Spawn(Vector3 spawnPosition, Quaternion spawnRotation)
        {
            base.Spawn(spawnPosition, spawnRotation);
            _health.Reset();
        }

        public override void TakeOnEquipments(IEnumerable<Item> items)
        {
            base.TakeOnEquipments(items);
            _health.Initialize();
        }

        private void RequestToDealDamageHandle(IAttackable obj)
        {
            obj.TakeHitByAttacker(new AttackDamageData(_characterModel.Damage.Value));
        }
        
        private void RequestToGetDamageHandle(AttackDamageData damageData)
        {
            _animator.SelectState(AnimatorTag.GetDamage);
            _health.Reduce(damageData.Damage);
        }
        
        private void OnHealthIsOverHandle()
        {
            //_health.Reset();
        }
    }
}